using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class tower : MonoBehaviour
{
    private Rigidbody rb;
    private int damage;


    void Start()
    {

        damage = 0;
    }


    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "saber")
        {

            damage = damage + 1;
            other.gameObject.SetActive(false);
            if (damage >= 3)
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();

                other.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }

        }
    }

}