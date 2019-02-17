using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class lose : MonoBehaviour
{
    public int wallHP = 10;
    public GameObject player;
    public GameObject badguyboats;
    private Rigidbody rb;
    public Text loseText;
    public Text wallText;
    void Start()
    {
        wallHP = 10;
        loseText.text = "";
        setwallText();
    }

    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag ("badguy"))
        {
            wallHP = wallHP - 1;
            other.gameObject.SetActive(false);
            setwallText();
        }
    }
    private void setwallText()
    {
        wallText.text = "walls:" + wallHP.ToString();
        if (wallHP == 0)
        {
            setloseText();
            player.gameObject.SetActive(false);
            badguyboats.gameObject.SetActive(false);
            this.gameObject.SetActive(false);

        }

    }
    private void setloseText()
    {
        loseText.text = "You lose";
    }
}
