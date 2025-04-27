using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    [SerializeField] private List<BoxCollider2D> spawnList = new List<BoxCollider2D>();
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnInterval;

    private static readonly Vector2[] Directions = { Vector2.down, Vector2.right, Vector2.left, Vector2.up };

    void Start()
    {
        spawnTime = 0f;
    }

    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnInterval < spawnTime)
        {
            int num = Random.Range(0, spawnList.Count);
            int itemNum = Random.Range(0, items.Count);
            BoxCollider2D spawnRange = spawnList[num];
            Vector3 randomRange = RandomRange(spawnRange.bounds);

            GameObject item = Instantiate(items[itemNum], randomRange, Quaternion.identity);
            SetItemDirection(item, itemNum, Directions[num]);
            spawnTime = 0f;
        }
    }

    Vector3 RandomRange(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(randomX, randomY, 0);
    }

    private void SetItemDirection(GameObject item, int itemNum, Vector2 direction)
    {
        // itemNum: 0=Thorn, 1=Apple, 2/3=Bomb
        if (itemNum == 0)
        {
            // Thorn
            var thorn = item.GetComponent<ThornCon>();
            if (thorn != null) thorn.SetThorn(direction);
        }
        else if (itemNum == 1)
        {
            // Apple
            var apple = item.GetComponent<AppleCon>();
            if (apple != null) apple.SetApple(direction);
        }
        else if (itemNum == 2 || itemNum == 3)
        {
            // Bomb
            var bomb = item.GetComponent<BombCon>();
            if (bomb != null) bomb.SetThorn(direction);
        }
    }
}
