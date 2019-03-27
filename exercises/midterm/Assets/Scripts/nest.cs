using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nest : MonoBehaviour
{
    private int damage;
    public GameObject coinpreb;
    public GameObject spawnpoint;
    private float health;
    private float starthealth = 100;
    public Image healthbar;

    public Text winText;
    void Start()
    {
        winText.text = "";
        health = starthealth;
    }


    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "attackcube")
        {
            other.gameObject.SetActive(false);
            takedamage();
        }
    }
    public void takedamage()
    {
        health -= 5;
        healthbar.fillAmount = health / 100f;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            showcoin();
            setwinText();
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
        spawnpoint.gameObject.SetActive(false);
    }
}
