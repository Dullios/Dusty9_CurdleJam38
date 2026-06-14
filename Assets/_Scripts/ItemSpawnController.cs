using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnController : MonoBehaviour
{
    [Header("Item Values")]
    public GameObject[] itemList;
    public GameObject displayItem;
    public float spawnTimeMin;
    public float spawnTimeMax;

    public float restockTimeMin;
    public float restockTimeMax;

    [Header("Spawner Values")]
    public float angularSpeed;
    bool hasSpawned;

    Coroutine spawnCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnItem(0.1f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, angularSpeed, 0);

        if(hasSpawned)
        {
            spawnCoroutine = StartCoroutine(SpawnItem(Random.Range(spawnTimeMin, spawnTimeMax)));
            hasSpawned = false;
        }
    }

    IEnumerator SpawnItem(float time)
    {
        yield return new WaitForSeconds(time);

        RemoveItem();
        displayItem = Instantiate(itemList[Random.Range(0, itemList.Length)], transform);
        displayItem.GetComponent<ItemProperties>().itemSpawner = this;
        hasSpawned = true;
    }

    public void ItemClicked()
    {
        StopCoroutine(spawnCoroutine);
        RemoveItem();

        StartCoroutine(SpawnItem(Random.Range(restockTimeMin, restockTimeMax)));
    }

    void RemoveItem()
    {
        if (displayItem != null)
        {
            Destroy(displayItem);
            displayItem = null;
        }
    }
}
