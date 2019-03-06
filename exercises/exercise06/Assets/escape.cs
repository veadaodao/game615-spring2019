using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class escape : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject laptop;
    public GameObject room;
    private int jumptimes = 0;

    public Text winText;


    void Start()
    {
        winText.text = "";

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            

            jumptimes = jumptimes + 1;

            if (jumptimes == 3)
            {
                this.gameObject.SetActive(false);
                laptop.gameObject.SetActive(true);
                room.gameObject.SetActive(true);
                setwinText();

            }


        }
    }
    private void setwinText()
    {
        winText.text = "You are the ONE";
    }
}
