using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class policeScript : MonoBehaviour
{
    public Animator policeanim;
    private int speed=0;
    void Start()
    {
        policeanim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            policeanim.SetBool("collision", true);
            speed = 3;
        }
    }
}
