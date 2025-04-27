using Unity.VisualScripting;
using UnityEngine;

public class LineCon : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    public ResultManager resultManager;
    public string namea;
    private int hp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = 2;
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Debug.Log("LineのHPがない。ゲームオーバーじゃ");
            resultManager.Result(namea);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item_Bad"))
        {
            hp -= 1;
            Destroy(collision.gameObject);
            Debug.Log("ダメージくらった");
            audioManager.PlaySE("25Sakyou - Bomb 0.0.0_103939");
        }
        if (collision.CompareTag("Item_Good"))
        {
            hp += 1;
            Destroy(collision.gameObject);
            Debug.Log("回復じゃ");
            audioManager.PlaySE("25Sakyou - Ping_113834");
        }
    }
}
