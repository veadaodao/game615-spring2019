using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playercontrol : MonoBehaviour
{
    public float speed = 20.0f;
    private Vector3 startPosition;
    private Quaternion startRotation;

    public GameObject saberPrefab;
    private Rigidbody rb;
    public GameObject playerPrefab;

    public int sabernumber = 30;
    public int playerHP = 3;

    public Text sabernumberText;
    public Text backtohomeText;
    public Text lifeText;
    public Text loseText;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        setsabernumberText();
        lifeText.text = "life: 3";
        backtohomeText.text = "";
        loseText.text = "";
    }


    void Update()
    {
        Vector3 moveCamTo = transform.position - transform.forward * 20.0f + Vector3.up * 5.0f;
        float bias = 0.96f;
        Camera.main.transform.position = Camera.main.transform.position *bias +
                                         moveCamTo *(1.0f-bias);

        Camera.main.transform.LookAt(transform.position + transform.forward * 20.0f);


        transform.position += transform.forward * Time.deltaTime * speed;
        speed -= transform.forward.y * Time.deltaTime * 5.0f;
        if (speed < 5.0f)
        {
            speed = 5.0f;
        }
        transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
        
        float terrainHeightwhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);
        if (terrainHeightwhereWeAre > transform.position.y) 
        {
            transform.position = new Vector3(transform.position.x,
                                             terrainHeightwhereWeAre,
                                             transform.position.z);
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            if (sabernumber > 0)
            {
                GameObject saber = Instantiate(saberPrefab, transform.position, transform.rotation);
                sabernumber = sabernumber - 1;
                setsabernumberText();
                Destroy(saber, 10f);
                setbacktohomeText();

            }
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            if (sabernumber == 0)
            {
                transform.position = startPosition;
                transform.rotation = startRotation;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "hometower")
        {
            sabernumber = 30;
        }


        if (other.gameObject.CompareTag("tower"))
        {
            if (playerHP > 0)
            {
                other.gameObject.SetActive(false);
                playerHP = playerHP - 1;
                setlifeText();
            }
            
            if (playerHP == 0)
            {
                setloseText();
                playerPrefab.gameObject.SetActive(false);
                
            }
        }

    }
    private void setsabernumberText()
    {
        sabernumberText.text = "saber number:" + sabernumber.ToString();
    }
        private void setlifeText()
    {
        lifeText.text = "life: " + playerHP.ToString();
    }
    private void setbacktohomeText()
    {
        if (sabernumber == 0)
        {
            backtohomeText.text = "back to home!!! reload";
            if (sabernumber > 0)
            {
                Destroy(backtohomeText,3f);
            }

        }
        else
        {
            backtohomeText.text = "";
        }
    }
    private void setloseText()
    {
        loseText.text = "You lose";
        playerPrefab.gameObject.SetActive(false);
    }

    public void resetscreen()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
