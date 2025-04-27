using UnityEngine;

public class SpriteFadeByUpdate : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private float fadeDelay = 0.0f; // フェード開始までの遅延時間
    private SpriteRenderer spriteRenderer;
    private float timer = 0f;
    private bool isFading = false;
    private Color startColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 指定した遅延後にフェード開始
        if (fadeDelay > 0f)
            Invoke(nameof(StartFade), fadeDelay);
        else
            StartFade();
    }

    private void Update()
    {
        if (!isFading) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / fadeDuration);
        Color c = startColor;
        c.a = Mathf.Lerp(startColor.a, 0f, t);
        spriteRenderer.color = c;

        if (t >= 1f)
        {
            isFading = false;
            Destroy(gameObject); // フェード終了後に自動で削除
        }
    }

    public void StartFade()
    {
        startColor = spriteRenderer.color;
        timer = 0f;
        isFading = true;
    }
}
