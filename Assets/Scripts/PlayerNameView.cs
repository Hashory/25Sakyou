using UnityEngine;

[System.Serializable]
public struct targetSet {
        public Transform target; // ターゲットのTransform
        public string keyName;
}

public class PlayerNameView : MonoBehaviour
{
    [SerializeField] private GameObject playerNamePrefab; // プレイヤー名のプレハブ

    [SerializeField] private targetSet[] targets; // プレイヤーのターゲットセット

    [SerializeField] private Vector3 offset = Vector3.zero; // プレイヤー名のオフセット

    private GameObject[] playerNameObjects; // プレイヤー名のインスタンス

    void Start()
    {
        playerNameObjects = new GameObject[targets.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            playerNameObjects[i] = Instantiate(playerNamePrefab, targets[i].target.position + offset, Quaternion.identity);
            // 親を設定しないことでワールド座標に従う
        }
    }

    void Update()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].target != null)
            {
                playerNameObjects[i].transform.position = targets[i].target.position + offset;
            }
        }
    }
}
