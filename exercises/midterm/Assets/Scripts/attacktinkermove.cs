using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attacktinkermove : MonoBehaviour
{
    public float speed = 1f;
    GameObject Barbarian;
    private Rigidbody rb;

    void Start()
    {

        Barbarian = GameObject.Find("Barbarian");
        transform.position = Barbarian.transform.position;
        transform.rotation = Barbarian.transform.rotation;

    }


    void Update()
    {

        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        float terrainHeightwhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);
        if (terrainHeightwhereWeAre > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x,
                                             terrainHeightwhereWeAre,
                                             transform.position.z);
        }
    }
}
