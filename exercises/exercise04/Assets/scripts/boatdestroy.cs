using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatdestroy : MonoBehaviour
{
    private int damage = 0;
    private Rigidbody rb;
    public GameObject badboat;
    void Start()
    {

    }


    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "bullet")
        {

            damage = damage + 1;
            if (damage >= 30)
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                other.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
                badboat.gameObject.SetActive(false);
            }
        }
    }
}
