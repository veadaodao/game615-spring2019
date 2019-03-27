using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nest : MonoBehaviour
{
    private int damage;
    public GameObject coinpreb;
    public GameObject spawnpoint;
    private float health;
    private float starthealth = 100;
    public Image healthbar;
    public int win = 0;

    public Text winText;
    public Text gameoverText;
    void Start()
    {
        winText.text = "";
        gameoverText.text = "";
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
        health -= 2;
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
            Destroy(winText, 10f);
            setgameoverText();
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
    public void resetscreen()
    {
        SceneManager.LoadScene("openscene");
    }
    public void setgameoverText()
    {
        gameoverText.text = "click reset to try again";
    }
}
