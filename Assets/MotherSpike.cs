using UnityEngine;

public class MotherSpike : MonoBehaviour
{
    public GameObject prefab;

    
    void Start()
    {
        for(int n=0; n<10; n++){
            Instantiate(prefab, new Vector3(
                Random.Range(-100, 100),
                Random.Range(-100, 100), 
                0),
                Quaternion.identity);
        }
        Debug.Log("Prefab instantiated at random position.");
        Debug.Log("Prefab instantiated at random position.");

    }
}
