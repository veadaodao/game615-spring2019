using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleshipmove : MonoBehaviour
{
    public float speed = 8;
    void Start()
    {

    }


    void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name =="stop")
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            speed = 0;

        }
    }
}
