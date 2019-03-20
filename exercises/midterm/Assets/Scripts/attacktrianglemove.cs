using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attacktrianglemove : MonoBehaviour
{
    public float speed = 30f;
    GameObject wolfrider;
    private Rigidbody rb;

    void Start()
    {

        wolfrider = GameObject.Find("wolfrider");
        transform.position = wolfrider.transform.position;
        transform.rotation = wolfrider.transform.rotation;

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
