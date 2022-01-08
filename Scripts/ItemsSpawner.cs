using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] List<Item> items;

    [SerializeField] Transform spawn1, spawn2;
    [SerializeField] int secondsBetweenSpawn;

    private void Start()
    {
        StartCoroutine(Spawn()); 
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(secondsBetweenSpawn);
            Vector3 randomPos = new Vector3(Random.Range(spawn1.position.x, spawn2.position.x), Random.Range(spawn1.position.y, spawn2.position.y), 0);
            Instantiate(items[Random.Range(0, items.Count)], randomPos, Quaternion.identity);
        }
    }
}
