using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lose : MonoBehaviour
{
    public Text loseText;

    public GameObject Paladin;
    public GameObject wolfrider;
    public GameObject tower;

    private int villagedamage = 0;
    void Start()
    {
        loseText.text = "";
    }

    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "dragon")
        {
            villagedamage = villagedamage + 1;
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            other.gameObject.SetActive(false);
            if (villagedamage == 5)

            {
                Debug.Log("die");
                setloseText();
                
            }     
        }
    }
    private void setloseText()
    {

        loseText.text = "So many dragons invade your home village, you lose";
        Paladin.gameObject.SetActive(false);
        wolfrider.gameObject.SetActive(false);
        tower.gameObject.SetActive(false);

    }
}
