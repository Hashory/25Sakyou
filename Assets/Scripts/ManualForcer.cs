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
        marginAngle = Random.Range(0, 45);
        direction = new Vector2(Mathf.Cos(marginAngle * Mathf.Deg2Rad), Mathf.Sin(marginAngle * Mathf.Deg2Rad)).normalized;

        UpdateArrow();
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
        if (Input.GetKey(actionKey))
        {
            keyPressTime += Time.deltaTime;
            if(keyPressTime > shortPressDuration)
            {
                ChangeDirection();
                keyPressTime = 0f;
            }
        }
        
        // キーが離されたとき
        if (Input.GetKeyUp(actionKey))
        {
            isKeyPressed = false;
            
            ApplyForce();
            
            keyPressTime = 0f;
        }

        // 毎フレーム矢印を更新
        UpdateArrow();
    }

    // 方向を90度変更
    private void ChangeDirection()
    {
        float angle = 45 * Mathf.Deg2Rad; // ラジアンに変換
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);

        // 時計回りにする
        float newX = direction.x * cos + direction.y * sin;
        float newY = -direction.x * sin + direction.y * cos;

        direction = new Vector2(newX, newY);
        UpdateArrow();
    }

    // 矢印の向きをdirectionに合わせてグローバル回転で更新
    private void UpdateArrow()
    {
        if (arrowImage != null)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            arrowImage.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // 力を適用
    private void ApplyForce()
    {
        rb2d.AddForce(direction * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item_Bad"))
        {
            Destroy(collision.gameObject);
            Debug.Log("ゲームオーバーじゃ");
        }
    }
}
