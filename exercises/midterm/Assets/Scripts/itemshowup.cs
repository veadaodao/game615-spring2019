using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemshowup : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public float spawnTime = 2.5f;
    public GameObject[] Items;

    void Start()
    {
        InvokeRepeating("SpawnItems", spawnTime, spawnTime);
    }

    void Update()
    {
        
    }
    void SpawnItems()
    {
        int spawnIndex = Random.Range(0, SpawnPoints.Length);
        int ItemIndex = Random.Range(0, Items.Length);
        Instantiate(Items[ItemIndex], SpawnPoints[spawnIndex].position, SpawnPoints[spawnIndex].rotation);
    }
}
