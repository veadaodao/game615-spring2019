using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxbroke : MonoBehaviour
{
    private Rigidbody rb;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag ("bullet"))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }
}
