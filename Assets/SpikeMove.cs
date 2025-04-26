using UnityEngine;

public class SpikeMove : MonoBehaviour
{
    public float speed = 2.0f; // 移動速度（単位：ユニット/秒）
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 毎フレーム、右方向（X軸正方向）に移動
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
