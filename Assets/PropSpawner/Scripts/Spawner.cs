using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float minTimeToSpawn = 0.2f;
    [SerializeField]
    private float maxTimeToSpawn = 2.0f;
    private float timeToSpawn = 0.0f;
    private float timeSinceSpawn;
    private PropPool objectPool;
    [SerializeField]
    private GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        objectPool = FindObjectOfType<PropPool>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= timeToSpawn)
        {
            timeToSpawn = Random.Range(minTimeToSpawn, maxTimeToSpawn);
            GameObject newProp = objectPool.GetObject(prefab);
            newProp.transform.position = this.transform.position + new Vector3(Random.Range(-50.0f, 50.0f), 0, 0);
            timeSinceSpawn = 0f;
        }
    }
}
