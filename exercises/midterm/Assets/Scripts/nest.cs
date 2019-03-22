using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nest : MonoBehaviour
{
    private int damage;
    public GameObject coinpreb;

    public Text winText;
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

        if (other.gameObject.tag == "attackcube")
        {

            damage = damage + 1;
            if (damage >= 5f)
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                other.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
                showcoin();
                setwinText();
            }

        }

    }
    void showcoin()
    {
        GameObject coin = Instantiate(coinpreb, transform.position, transform.rotation);
        transform.position = this.transform.position;
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }
    private void setwinText()
    {
        winText.text = "You win!!!";
    }
}
