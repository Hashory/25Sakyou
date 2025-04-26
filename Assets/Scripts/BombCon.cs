using UnityEngine;

public class BombCon : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 dir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    void Update()
    {
        this.transform.position += (Vector3)dir * speed * Time.deltaTime;
    }

    public void SetThorn(Vector2 dir)
    {
        this.dir = dir;

    }
}
