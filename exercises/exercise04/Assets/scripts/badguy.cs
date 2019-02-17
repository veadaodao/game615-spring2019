using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class badguy : MonoBehaviour
{
    public float speed;
    GameObject playerobj;
    private Rigidbody rb;
    public float rotateSpeed = 100f;
    private int damage;



    void Start()
    {

        playerobj = GameObject.Find("Player");
        speed = Random.Range(5f, 10f);
        damage = 0;
    }


    void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag ("bag"))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "bullet")
        {

            damage = damage + 1;
            if (damage >= 2)
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                other.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }

        }

        if (other.gameObject.CompareTag ("walldestroy"))
        {
            this.gameObject.SetActive(false);
        }
    }

}