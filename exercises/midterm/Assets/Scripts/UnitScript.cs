using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UnitScript : MonoBehaviour
{
    Renderer renderer;
    CharacterController cc;
    GameManager gameManager;

    float moveSpeed = 50f;
    float rotateSpeed = 60f;
    float jumpForce = 0.5f;
    float gravityModifier = 0.2f;

    float yVelocity = 0;
    bool previousIsGroundedValue;

    public string unitName = "";
    public string bio = "";
    public Sprite portrait;

    public Color unselectedColor;
    public Color selectedColor;
    public Color hoverColor;

    public bool selected = false;
    bool hover = false;
    bool justSelected = false;

    public LayerMask layerMask;

    public GameObject attackcubepref;
    public GameObject wolfrider;
    private int coinnumber = 10;
    private float starthealth = 100;
    private float health;

    public Text coinText;
    public Text loseText;

    public Animator paladinanim;
    public Animator wolfrideranim;

    public Image healthbar;

    void Start()
    {
        renderer = gameObject.GetComponentInChildren<Renderer>();
        cc = gameObject.GetComponent<CharacterController>();

        GameObject gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();

        updateVisuals();
        previousIsGroundedValue = cc.isGrounded;

        loseText.text = "";
        setcoinText();

        paladinanim = GetComponent<Animator>();
        wolfrideranim = GetComponent<Animator>();

        health = starthealth;

    }

    void Update()
    {
        if (selected && !justSelected)
        {
            float hAxis = Input.GetAxis("Horizontal");
            float vAxis = Input.GetAxis("Vertical");


            transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0);


            if (!cc.isGrounded)
            {
                yVelocity = yVelocity + Physics.gravity.y * gravityModifier * Time.deltaTime;
                


                if (Input.GetKeyUp(KeyCode.Space) && yVelocity > 0)
                {
                    yVelocity = 0;
                    
                }
            }
            else
            {
                if (!previousIsGroundedValue)
                {

                    yVelocity = 0;
                }



                if (Input.GetKeyDown(KeyCode.Space))
                {
                    yVelocity = jumpForce;

                    float x = transform.position.x;
                    float z = transform.position.z+30;
                    Vector3 pos = new Vector3(x, transform.position.y+20, z);
                    GameObject attackcube = Instantiate(attackcubepref, pos, transform.rotation);

                    Destroy(attackcube, 1f);
                    paladinanim.SetBool("attack", true);
                    wolfrideranim.SetBool("attack", true);
                }
                else
                {
                    paladinanim.SetBool("attack", false);
                    wolfrideranim.SetBool("attack", false);
                }
            }

            Vector3 amountToMove = transform.forward * vAxis * moveSpeed * Time.deltaTime;


            amountToMove.y = yVelocity;


            cc.Move(amountToMove);


            previousIsGroundedValue = cc.isGrounded;


          
        }
        justSelected = false;
    }

    void OnMouseEnter()
    {
        hover = true;
        updateVisuals();
    }
    void OnMouseExit()
    {
        hover = false;
        updateVisuals();
    }
    void OnMouseDown()
    {
        
            selected = true;
            justSelected = true;
            gameManager.selectUnit(this);

            updateVisuals();
        
    }

    public void updateVisuals()
    {
        if (selected)
        {
            renderer.material.color = selectedColor;
        }
        else
        {
            if (hover)
            {
                renderer.material.color = hoverColor;
            }
            else
            {
                renderer.material.color = unselectedColor;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "coin")
        {
            coinnumber = coinnumber +1;
            setcoinText();
            other.gameObject.SetActive(false);
            Debug.Log("testing");
        }
        if (other.gameObject.tag == "dragon")
        {

            takedamage();
            setcoinText();
            other.gameObject.SetActive(false);

            if (health == 0)
            {
                this.gameObject.SetActive(false);
                setloseText();
            }
        }
    }

    public void takedamage()
    {
        health -= 10;
        healthbar.fillAmount = health/100f;
        if (health <= 0)
        {
            setloseText();
            this.gameObject.SetActive(false);
        }
    }

    public void activewolf()
    {
        coinnumber = coinnumber - 10;
        wolfrider.gameObject.SetActive(true);      
        setcoinText();       
    }


    public void setcoinText()
    {
        coinText.text = "coin:" + coinnumber.ToString();
        if (coinnumber < -30)
        {
            loseText.text = "You go broke";
            this.gameObject.SetActive(false);
        }
    }

    private void setloseText()
    {
        loseText.text = "You die";
    }

    public void resetscreen()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void healing()
    {
        coinnumber = coinnumber - 10;
        health = 100;
        setcoinText();
    }
}
