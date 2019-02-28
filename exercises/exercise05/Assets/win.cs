using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class win : MonoBehaviour
{
    private Rigidbody rb;
    private int damage;
    public Text winText;
    public GameObject player;
    void Start()
    {

        damage = 0;
        winText.text = "";
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
                setwinText();
            }

        }
    }
    private void setwinText()
    {
        winText.text = "legendary!!!";
        Destroy(winText, 10f);
        player.gameObject.SetActive(false);
    }
}
