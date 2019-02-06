using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour
{

    private Rigidbody rb;
    private Vector3 startPosition;
    private Quaternion startRotation;
    float speed = 200;
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        float horizontal = 0;
        float vertical = 0;
        if (Input.GetKey(KeyCode.W))
        {
            print("w");
            vertical = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vertical = -1;
            print("s");
        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1;
            print("a");
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1;
            print("d");
        }

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "trap")
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.transform.position = startPosition;
            gameObject.transform.rotation = startRotation;

        }
        if (other.gameObject.CompareTag("clock"))
        {
            other.gameObject.SetActive(false);
        }
        
    }
}
