using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saber : MonoBehaviour
{
    public float speed = 30f;
    GameObject player;
    private Rigidbody rb;
    void Start()
    {

        player = GameObject.Find("player");


        float Scale = 1 * 0.05f;
        transform.localScale = new Vector3(Scale, Scale, Scale);
        transform.rotation = player.transform.rotation;

    }


    void Update()
    {

        transform.position = transform.position + transform.forward * speed * Time.deltaTime;

    }
}
