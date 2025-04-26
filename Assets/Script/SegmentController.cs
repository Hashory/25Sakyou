using UnityEngine;

// Rigidbodyが必須であることを示す属性
[RequireComponent(typeof(Rigidbody))]
public class SegmentController : MonoBehaviour
{
    public Rigidbody Rb { get; private set; }

    void Awake()
    {
        // このGameObjectにアタッチされているRigidbodyを取得して保持
        Rb = GetComponent<Rigidbody>();
        if (Rb == null)
        {
            Debug.LogError("SegmentController requires a Rigidbody component!", this.gameObject);
        }
    }

    /// <summary>
    /// このセグメントに力を加えるメソッド
    /// </summary>
    /// <param name="force">加える力のベクトル</param>
    /// <param name="mode">力の加え方（デフォルトはImpulse: 瞬間的な力）</param>
    public void ApplyForce(Vector3 force, ForceMode mode = ForceMode.Impulse)
    {
        if (Rb != null)
        {
            Rb.AddForce(force, mode);
            // Debug.Log($"Applied force {force} to {gameObject.name}"); // デバッグ用
        }
        else
        {
            Debug.LogWarning("Cannot apply force: Rigidbody not found on segment!", this.gameObject);
        }
    }

    // 必要に応じて他のメソッド（例：特定の位置に力を加えるAddForceAtPositionなど）を追加可能
}