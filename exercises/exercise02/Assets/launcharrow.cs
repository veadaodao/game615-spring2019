using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launcharrow : MonoBehaviour
{
    float launchForce = 0;
    Vector3 startPosition;
    Quaternion startRotation;
    bool playerWon = false;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.AddForce(transform.forward * launchForce);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            launchForce = launchForce + 3f;
        }
        else
        {
            if (launchForce > 0)
            {
                launchForce = launchForce - 3f;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            print("w");
            transform.Translate(Vector3.up * Time.deltaTime * 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            print("s");
            transform.Translate(Vector3.down * Time.deltaTime * 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            print("a");
            transform.Translate(Vector3.left * Time.deltaTime * 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            print("d");
            transform.Translate(Vector3.right * Time.deltaTime * 1);
        }


    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "trigger")
        {
            playerWon = true;
        }

        if (other.gameObject.name == "ground")
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            launchForce = 0;
            gameObject.transform.position = startPosition;
            gameObject.transform.rotation = startRotation;
        }
       
       
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(50, 50, 200, 200), launchForce.ToString());

        if (playerWon)
        {
            GUI.Label(new Rect(500, 500, 200, 200), "Scream!");
        }
    }
}