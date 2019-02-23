using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontrol : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Rigidbody rb;
    private Vector3 startPosition;
    private Quaternion startRotation;
    public GameObject playerPrefab;
    public GameObject badguyboatsPrefab;
    public GameObject battleshipPrefab;
    public float speed = 10;
    public float rotatespeed = 5;
    public int bulletnumber = 30;
    public int playerHP = 3;

    public Text bulletnumberText;
    public Text reloadText;
    public Text lifeText;
    public Text winText;
    public Text loseText;

    void Start()
    {
        Debug.Log(transform.forward);
        startPosition = transform.position;
        startRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();

        setbulletnumberText();
        lifeText.text = "life: 3";
        reloadText.text = "";
        winText.text = "";
        loseText.text = "";
        playerPrefab.gameObject.SetActive(false);
        badguyboatsPrefab.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            if (bulletnumber > 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bulletnumber = bulletnumber - 1;
                setbulletnumberText();
                Destroy(bullet,10f);
                setreloadText();

            }  
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "magazine")
        {
            bulletnumber = 30;
        }

        if (other.gameObject.name == "ak47unlimited")
        {
            bulletnumber = 2023610852;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag ("badguy"))
        {
            if (playerHP > 0)
            {
                other.gameObject.SetActive(false);
                playerHP = playerHP - 1;
                setlifeText();
            }
            else
            {
                other.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }
                if (playerHP == 0)
                {
                    setloseText();
                    playerPrefab.gameObject.SetActive(false);
                }  
        }
        if (other.gameObject.name =="battleship")
        {
            this.gameObject.SetActive(false);
            setwinText();
        }
    }

    private void setbulletnumberText()
    {
        bulletnumberText.text = "bullet number:" + bulletnumber.ToString();

    }
    private void setreloadText()
    {
        if (bulletnumber == 0)
        {
            reloadText.text = "reload!!!";
            if (bulletnumber > 0)
            {
                Destroy(reloadText);
            }
            
        }
        else
        {
            reloadText.text = "";
        }
    }

    private void setlifeText()
    {
        lifeText.text = "life: " + playerHP.ToString();
    }

    private void setloseText()
    {
        loseText.text = "You lose";
    }

    private void setwinText()
    {
        winText.text = "You are saved";
    }
    void FixedUpdate()
    {


        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -speed * Time.deltaTime * rotatespeed, 0);
            print("q");
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, speed * Time.deltaTime * rotatespeed, 0);
            print("e");
        }
        if (Input.GetKey(KeyCode.A))
        {
            print("a");
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            print("d");
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            print("w");
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            print("s");
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }

        
    }
    public void resetscreen()
    {
        SceneManager.LoadScene("SampleScene");
    }
}