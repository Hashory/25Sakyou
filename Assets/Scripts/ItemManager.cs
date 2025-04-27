using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    [SerializeField] private List<BoxCollider2D> spawnList = new List<BoxCollider2D>();
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnInterval;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnInterval < spawnTime)
        {
            //上下左右どこに生成されるか
            int num = Random.Range(0, spawnList.Count);
            //どのアイテムが生成されるか
            int itemNum = Random.Range(0, items.Count);
            BoxCollider2D spawnRange = spawnList[num];

            Vector3 randomRange = RandomRange(spawnRange.bounds);

            GameObject item = Instantiate(items[itemNum], randomRange, Quaternion.identity);
            if (num == 0)
            {
                //上に生成
                if (itemNum == 0)
                {
                    //とげ
                    item.GetComponent<ThornCon>().SetThorn(Vector2.down);
                }
                else if (itemNum == 1)
                {
                    //リンご
                    item.GetComponent<AppleCon>().SetApple(Vector2.down);
                }
                else if (itemNum == 2 || itemNum == 3)
                {
                    //爆弾
                    item.GetComponent<BombCon>().SetThorn(Vector2.down);
                }
            }

            if (num == 1)
            {
                //左に生成
                if (itemNum == 0)
                {
                    //とげ
                    item.GetComponent<ThornCon>().SetThorn(Vector2.right);
                }
                else if (itemNum == 1)
                {
                    //リンご
                    item.GetComponent<AppleCon>().SetApple(Vector2.right);
                }
                else if (itemNum == 2 || itemNum == 3)
                {
                    //爆弾
                    item.GetComponent<BombCon>().SetThorn(Vector2.right);
                }

            }

            if (num == 2)
            {
                //→に生成
                if (itemNum == 0)
                {
                    //とげ
                    item.GetComponent<ThornCon>().SetThorn(Vector2.left);
                }
                else if (itemNum == 1)
                {
                    //リンご
                    item.GetComponent<AppleCon>().SetApple(Vector2.left);
                }
                else if (itemNum == 2 || itemNum == 3)
                {
                    //爆弾
                    item.GetComponent<BombCon>().SetThorn(Vector2.left);
                }

            }

            if (num == 3)
            {
                //↓に生成
                if (itemNum == 0)
                {
                    //とげ
                    item.GetComponent<ThornCon>().SetThorn(Vector2.up);
                }
                else if (itemNum == 1)
                {
                    //リンご
                    item.GetComponent<AppleCon>().SetApple(Vector2.up);
                }
                else if (itemNum == 2 || itemNum == 3)
                {
                    //爆弾
                    item.GetComponent<BombCon>().SetThorn(Vector2.up);
                }

            }
            spawnTime = 0f;
        }
    }

    Vector3 RandomRange(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(randomX, randomY, 0);
    }
}
