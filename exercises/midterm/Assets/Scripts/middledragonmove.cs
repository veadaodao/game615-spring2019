using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class middledragonmove : MonoBehaviour
{
    public float speed;
    public GameObject tower;
    private Rigidbody rb;


    public GameObject coinpreb;
    private float health;
    private float starthealth = 100;
    public Image healthbar;

    void Start()
    {

        tower = GameObject.Find("tower");
        speed = Random.Range(8f, 12f);
        health = starthealth;
    }


    void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;

        float terrainHeightwhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);
        if (terrainHeightwhereWeAre > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x,
                                             terrainHeightwhereWeAre,
                                             transform.position.z);
        }

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "attackcube")
        {
            other.gameObject.SetActive(false);
            takedamage();
        }
        if (other.gameObject.tag == "saber")
        {
            other.gameObject.SetActive(false);
            takedamage();
        }

    }

    public void takedamage()
    {
        health -= 20;
        healthbar.fillAmount = health / 100f;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            showcoin();
        }
    }
    void showcoin()
    {
        GameObject coin = Instantiate(coinpreb, transform.position, transform.rotation);
        transform.position = this.transform.position;
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }
}
