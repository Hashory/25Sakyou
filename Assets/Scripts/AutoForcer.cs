using UnityEngine;

public class AutoForcer : MonoBehaviour
{
    private Rigidbody2D rb2d; // Rigidbody2Dコンポーネントを格納する変数
    [SerializeField] private float force = 500f; // 力の大きさ
    [SerializeField] private float forceDuration = 2f; // 力を加える時間の間隔
    [SerializeField] private Transform arrowImage;

    private Vector2 nextForceDirection; // 次に加える力の方向を格納する変数
    private float timer = 0f; // 経過時間を追跡するタイマー

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        // 初回の方向をセット
        RandomizeNextDirection();
        // 初期状態では矢印を非表示
        DisableArrow();
        // タイマーを0に設定して開始
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        // forceDuration が 2 の場合のタイミング
        // 0秒: 力を適用し、矢印を非表示
        if (timer < Time.deltaTime) // 最初のフレーム
        {
            ApplyForce();
            DisableArrow();
        }
        // 1秒: 次の方向を決定し、矢印を表示・更新
        else if (timer >= forceDuration / 2 && timer < (forceDuration / 2) + Time.deltaTime)
        {
            RandomizeNextDirection();
            UpdateArrowDirection();
        }
        // 2秒: 力を適用し、タイマーをリセット
        else if (timer >= forceDuration && timer < forceDuration + Time.deltaTime)
        {
            ApplyForce();
            timer = 0f; // タイマーリセット
        }
    }
    
    // 次の方向をランダムに設定
    private void RandomizeNextDirection()
    {
        nextForceDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    
    // 矢印の向きを更新（矢印を表示する）
    private void UpdateArrowDirection()
    {
        arrowImage.gameObject.SetActive(true);
        float angle = Mathf.Atan2(nextForceDirection.y, nextForceDirection.x) * Mathf.Rad2Deg +180;
        arrowImage.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    // 矢印を非表示にする
    private void DisableArrow()
    {
        arrowImage.gameObject.SetActive(false);
    }
    
    // 力を適用
    private void ApplyForce()
    {
        rb2d.AddForce(nextForceDirection * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item_Bad"))
        {
            Debug.Log("ゲームオーバーじゃ");
        }
    }
}
