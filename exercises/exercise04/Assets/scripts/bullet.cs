using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 5f;
    GameObject player;
    private Rigidbody rb;
    void Start()
    {

        player = GameObject.Find("player1");


        float Scale = 10 * 1.5f;
        transform.localScale = new Vector3(Scale, Scale, Scale);
        transform.rotation = player.transform.rotation;

    }

 
    void Update()
    {

        transform.position = transform.position + transform.forward * speed * Time.deltaTime;

    }


}

