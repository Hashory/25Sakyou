using UnityEngine;

public class BombCon : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 dir;
    private bool canDestroy = false;
    [SerializeField] private float safeTime = 1.0f; // 無敵時間

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(EnableDestroy), safeTime);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += (Vector3)dir * speed * Time.deltaTime;
    }

    private void EnableDestroy()
    {
        canDestroy = true;
    }

    public void SetThorn(Vector2 dir)
    {
        this.dir = dir;

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
