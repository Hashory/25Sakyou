using UnityEngine;

public class AppleCon : MonoBehaviour
{
    private Vector2 dir;
    [SerializeField]private float speed;
    [SerializeField] private float rotateSpeed = 180f; // 角度回転スピード
    [SerializeField] private float radius = 0.5f; // 回転の半径

    private float angle = 0f; // 現在の回転角度

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 角度を進める
        angle += rotateSpeed * Time.deltaTime;

        // 回転によるオフセットを作る
        Vector2 offset = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad)
        ) * radius;

        // 進行方向に進みながら、回転オフセットを追加
        transform.position += ((Vector3)dir * speed * Time.deltaTime) + (Vector3)(offset * Time.deltaTime);
    }

    public void SetApple(Vector2 dir)
    {
        this.dir = dir;
    }
}
