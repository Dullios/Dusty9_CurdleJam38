using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnController : MonoBehaviour
{
    [Header("Item Values")]
    public GameObject[] itemList;
    public float spawnTimeMin;
    public float spawnTimeMax;

    public float restockTimeMin;
    public float restockTimeMax;

    [Header("Spawner Values")]
    public float angularSpeed;
    bool hasSpawned;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnItem(0.1f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, angularSpeed, 0);

        if(hasSpawned)
        {
            StartCoroutine(SpawnItem(Random.Range(spawnTimeMin, spawnTimeMax)));
            hasSpawned = false;
        }
    }

    IEnumerator SpawnItem(float time)
    {
        yield return new WaitForSeconds(time);

        RemoveChild();
        Instantiate(itemList[Random.Range(0, itemList.Length)], transform);
        hasSpawned = true;
    }

    void RemoveChild()
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
    }
}
