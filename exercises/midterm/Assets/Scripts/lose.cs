using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lose : MonoBehaviour
{
    public Text loseText;

    private int villagedamage;
    void Start()
    {
        villagedamage = 0;
    }
    // Update is called once per frame
    void Update()
    {
        loseText.text = "";
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "dragon")
        {

            setloseText();
            villagedamage = villagedamage + 1;
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            other.gameObject.SetActive(false);
            
     
    



        }

    }
    private void setloseText()
    {
        loseText.text = "You lose";
    }
}
