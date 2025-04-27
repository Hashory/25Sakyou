using UnityEngine;

public class ThornCon : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 dir;
    private bool canDestroy = false;
    [SerializeField] private float safeTime = 1.5f; // 無敵時間
    [SerializeField] private float stopTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(EnableDestroy), safeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(stopTime > 1.0f)
        {
            this.transform.position += (Vector3)dir * speed * Time.deltaTime;
        }
        else
        {
            this.transform.position += Vector3.zero;
            stopTime += Time.deltaTime;
        }
        
    }

    private void EnableDestroy()
    {
        canDestroy = true;
    }

    public void SetThorn(Vector2 dir)
    {
        this.dir = dir;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle-90f); // 必要に応じて
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canDestroy) return; // 無敵中は無視

        if (collision.CompareTag("OutWall"))
        {
            Destroy(this.gameObject);
        }
    }
}
