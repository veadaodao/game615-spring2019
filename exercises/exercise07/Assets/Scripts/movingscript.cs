using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class movingscript : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "stop")
        {
            speed = 0;
            if (speed ==0)
            {
                speed = speed + 1;
            }
            if (speed == 1)
            {
                speed = speed + 3;
            }
        }
    }

}