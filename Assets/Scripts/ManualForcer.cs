using UnityEngine;

public class ManualForcer : MonoBehaviour
{
    private Rigidbody2D rb2d; // Rigidbody2Dコンポーネントを格納する変数
    [SerializeField] private float force = 500f; // 力の大きさ
    Vector2 direction; // 次に加える力の方向を格納する変数
    private int marginAngle;
    
    [Header("Key Configuration")]
    [SerializeField] private KeyCode actionKey = KeyCode.Space; // デフォルトのアクションキー
    
    // キー押下時間の追跡
    private float keyPressTime = 0f;
    private bool isKeyPressed = false;
    [SerializeField] private float shortPressDuration = 0.5f; // 短押し判定時間（秒）

    [SerializeField] private Transform arrowImage;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        // 初回の方向をセット
        marginAngle = Random.Range(0, 45);
        direction = new Vector2(Mathf.Cos(marginAngle * Mathf.Deg2Rad), Mathf.Sin(marginAngle * Mathf.Deg2Rad)).normalized;
    }

    void Update()
    {
        // キーが押されたとき
        if (Input.GetKeyDown(actionKey))
        {
            isKeyPressed = true;
            keyPressTime = 0f;
        }
        
        // キーが押されている間
        if (isKeyPressed)
        {
            keyPressTime += Time.deltaTime;
        }
        
        // キーが離されたとき
        if (Input.GetKeyUp(actionKey))
        {
            isKeyPressed = false;
            
            // 0.5秒未満の短押しなら方向を90度変える
            if (keyPressTime < shortPressDuration)
            {
                ChangeDirection();
            }
            // 0.5秒以上の長押しなら力を加える
            else
            {
                ApplyForce();
            }
            
            keyPressTime = 0f;
        }
    }

    // 方向を90度変更
    private void ChangeDirection()
    {

        // 現在の方向から90度回転した新しい方向を計算
        direction = new Vector2(-direction.y, direction.x);

        // 矢印の向きを更新（矢印を表示する）
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        arrowImage.rotation = Quaternion.Euler(0, 0, angle);
    }

    // 力を適用
    private void ApplyForce()
    {
        rb2d.AddForce(direction * force);
    }
}
