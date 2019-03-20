using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class smalldragonmovement : MonoBehaviour
{
    public float speed;
    public GameObject tower;
    private Rigidbody rb;
    private float damage;

    public GameObject coinpreb;

    void Start()
    {

        tower = GameObject.Find("tower");
        speed = Random.Range(5f, 10f);
        damage = 0f;

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

            damage = damage + 1f;
            if (damage >= 5f)
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                other.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
                showcoin();
            }

        }
        if (other.gameObject.tag == "saber")
        {

            damage = damage + 1f;
            if (damage >= 5f)
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                other.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
                showcoin();
            }

        }

    }
    void showcoin()
    {
        GameObject coin = Instantiate(coinpreb, transform.position, transform.rotation);
        transform.position = this.transform.position;
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }

}