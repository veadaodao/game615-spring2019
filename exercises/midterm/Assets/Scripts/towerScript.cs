using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerScript : MonoBehaviour
{
    public GameObject saberPrefab;
    int fire = 1;
    CharacterController cc;
    void Start()
    {

        
    }

    void Update()
    {
        fire = fire + 1;
        makesaber();
    }
    void makesaber()
    {
        if (fire>=10)
        {
            fire = 1;
            float x = transform.position.x;
            float z = transform.position.z;
            Vector3 pos = new Vector3(x, transform.position.y + 32, z);
            transform.Rotate(new Vector3(0, 0, 0) * Time.deltaTime);
            GameObject saber = Instantiate(saberPrefab, pos, transform.rotation);
            Destroy(saber, 5f);
        
            Rigidbody saberRB = saber.GetComponent<Rigidbody>();
            saberRB.AddForce(saber.gameObject.transform.forward * Random.Range(50, 200));
            if (Input.GetKeyUp(KeyCode.R))
            {

            saber.transform.Rotate(0, Random.Range(0, 360), 0, Space.World);

            }
        }

        
    }
}
