using UnityEngine;
using System.Collections.Generic;

public class ArticulatedCreatureGenerator : MonoBehaviour
{
    [Header("Creature Settings")]
    [SerializeField, Min(2), Tooltip("生成するセグメント（◯）の数")]
    private int numberOfSegments = 3;

    [SerializeField, Min(0.1f), Tooltip("セグメントの中心間の距離")]
    private float segmentSpacing = 1.5f;

    [SerializeField, Min(0.1f), Tooltip("セグメント（球）の半径")]
    private float segmentRadius = 0.5f;

    [SerializeField, Min(0.01f), Tooltip("コネクタ（棒）の太さ")]
    private float connectorThickness = 0.1f;

    [Header("Component Settings")]
    [SerializeField, Tooltip("セグメントに使用するマテリアル（任意）")]
    private Material segmentMaterial;

    [SerializeField, Tooltip("コネクタに使用するマテリアル（任意）")]
    private Material connectorMaterial;

    [SerializeField, Tooltip("セグメントの物理特性を設定するPhysicMaterial（任意）")]
    private PhysicsMaterial segmentPhysicsMaterial;

    [Header("Physics Settings")]
    [SerializeField, Tooltip("セグメントの質量")]
    private float segmentMass = 1.0f;
    [SerializeField, Tooltip("セグメントの抵抗")]
    private float segmentDrag = 0.5f;
    [SerializeField, Tooltip("セグメントの回転抵抗")]
    private float segmentAngularDrag = 0.5f;

    // 生成されたセグメントのリスト（外部からアクセス可能）
    [Header("Generated Segments")]
    public List<SegmentController> segments = new List<SegmentController>();

    void Awake()
    {
        GenerateCreature();
    }

    [ContextMenu("Generate Creature")] // Inspectorから実行できるようにする
    void GenerateCreature()
    {
        // 既存の生成物を削除（エディタでの再生成用）
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        segments.Clear();

        if (numberOfSegments < 2)
        {
            Debug.LogError("NumberOfSegments must be at least 2.");
            return;
        }

        Rigidbody previousRigidbody = null;

        for (int i = 0; i < numberOfSegments; i++)
        {
            // --- セグメント (Sphere) の生成 ---
            GameObject segmentGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            segmentGO.name = $"Segment_{i}";
            // ジェネレーターオブジェクトの子にする
            segmentGO.transform.SetParent(this.transform);

            // 位置設定 (X軸方向に生成)
            Vector3 segmentPosition = transform.position + Vector3.right * i * segmentSpacing;
            segmentGO.transform.position = segmentPosition;

            // スケール設定（半径に合わせる）
            segmentGO.transform.localScale = Vector3.one * segmentRadius * 2f;

            // Colliderの調整（デフォルトのSphereColliderを使用）
            SphereCollider sphereCollider = segmentGO.GetComponent<SphereCollider>();
            if (sphereCollider != null)
            {
                 sphereCollider.radius = 0.5f; // Primitiveのデフォルト半径は0.5
                 if(segmentPhysicsMaterial != null)
                 {
                    sphereCollider.material = segmentPhysicsMaterial;
                 }
            }

            // Rigidbodyの設定
            Rigidbody currentRigidbody = segmentGO.AddComponent<Rigidbody>();
            currentRigidbody.mass = segmentMass;
            currentRigidbody.drag = segmentDrag;
            currentRigidbody.angularDrag = segmentAngularDrag;
            currentRigidbody.useGravity = true; // 重力の影響を受ける

            // SegmentControllerの追加とリストへの登録
            SegmentController segmentController = segmentGO.AddComponent<SegmentController>();
            segments.Add(segmentController);

            // マテリアルの設定
            Renderer segmentRenderer = segmentGO.GetComponent<Renderer>();
            if (segmentRenderer != null && segmentMaterial != null)
            {
                segmentRenderer.material = segmentMaterial;
            }

            // --- Joint と コネクタ (-) の生成 (最初のセグメント以外) ---
            if (i > 0 && previousRigidbody != null)
            {
                var joint = segmentGO.AddComponent<ConfigurableJoint>();
                joint.connectedBody = previousRigidbody;
                joint.autoConfigureConnectedAnchor = false;

                // これまで固定だった 0.5 → segmentSpacing/2f に
                float half = segmentSpacing * 0.5f;
                joint.anchor = new Vector3(-half, 0, 0);
                joint.connectedAnchor = new Vector3(half, 0, 0);

                // 距離をしっかり固定
                joint.xMotion = ConfigurableJointMotion.Locked;
                joint.yMotion = ConfigurableJointMotion.Locked;
                joint.zMotion = ConfigurableJointMotion.Locked;

                // 回転は自由（または制限付きにしても良い）
                joint.angularXMotion = ConfigurableJointMotion.Free; // 例: Z軸周りのみ回転させたい場合は Locked or Limited
                joint.angularYMotion = ConfigurableJointMotion.Free; // 例: Z軸周りのみ回転させたい場合は Locked or Limited
                joint.angularZMotion = ConfigurableJointMotion.Free; // 例: 蝶番のように一軸回転にしたい場合など

                // --- コネクタ (Cube) の生成 ---
                GameObject connectorGO = GameObject.CreatePrimitive(PrimitiveType.Cube);
                connectorGO.name = $"Connector_{i-1}_to_{i}";
                connectorGO.transform.SetParent(this.transform); // ジェネレーターの子にする

                // コネクタのColliderは不要なら削除
                DestroyImmediate(connectorGO.GetComponent<BoxCollider>()); // 生成直後なのでImmediate

                // 位置設定 (2つのセグメントの中間)
                connectorGO.transform.position = (previousRigidbody.position + currentRigidbody.position) / 2f;

                // スケール設定 (太さと長さ)
                // 長さはセグメント間の距離 segmentSpacing
                connectorGO.transform.localScale = new Vector3(connectorThickness, connectorThickness, segmentSpacing);

                // 向き設定 (前のセグメントから現在のセグメントへ)
                connectorGO.transform.LookAt(currentRigidbody.position);

                // マテリアルの設定
                Renderer connectorRenderer = connectorGO.GetComponent<Renderer>();
                if (connectorRenderer != null && connectorMaterial != null)
                {
                    connectorRenderer.material = connectorMaterial;
                }
            }

            // 次のループのために現在のRigidbodyを保持
            previousRigidbody = currentRigidbody;
        }
        Debug.Log($"Generated creature with {numberOfSegments} segments.");
    }
}