using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playercontrol : MonoBehaviour
{
    float moveSpeed = 5f;
    float rotateSpeed = 60f;
    float jumpForce = 0.35f;

    float gravityModifier = 0.2f;

    float yVelocity = 0;
    bool previousIsGroundedValue;

    CharacterController cc;

  
    float camLookAhead = 8f;
    float camFollowBehind = 5f;
    float camFollowAbove = 3f;

    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject door4;

    private int wallopen1 = 0;
    private int wallopen2 = 0;
    private int wallopen3 = 0;
    private int jumptimes = 0;
    private int TotalTime;

    public Text axText;
    public Text errorText;
    public Text loseText;
    public Text timecountText;

    void Start()
    {
 
        cc = gameObject.GetComponent<CharacterController>();

 
        previousIsGroundedValue = cc.isGrounded;

        axText.text = "";
        errorText.text = "";
        loseText.text = "";

        TotalTime = 300;
    }

    void Update()
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
            }
        }

        Vector3 amountToMove = transform.forward * vAxis * moveSpeed * Time.deltaTime;

   
        amountToMove.y = yVelocity;


        cc.Move(amountToMove);


        previousIsGroundedValue = cc.isGrounded;


        Vector3 cameraPosition = transform.position + (Vector3.up * camFollowAbove) + (-transform.forward * camFollowBehind);
        Camera.main.transform.position = cameraPosition;
        Camera.main.transform.LookAt(transform.position + transform.forward * camLookAhead);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "walldestroyer")
        {
            wallopen1 = 1;
            other.gameObject.SetActive(false);
            setaxText();
            Destroy(axText,3f);

        }
        if (other.gameObject.name == "sunglasses")
        {
            other.gameObject.SetActive(false);
            wallopen2 = 1;
        }
        if (other.gameObject.name == "laptop")
        {
            other.gameObject.SetActive(false);
            wallopen3 = 1;
        }

        if (other.gameObject.name == "flooropen1")
        {
            
            if (jumptimes == 5 && wallopen1 >= 0)
            {
                jumptimes = jumptimes + 1;
                door1.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
                jumptimes = 0;
            }
        }
        if (other.gameObject.name == "flooropen2")
        {
            jumptimes = jumptimes + 1;
            if (jumptimes == 5 && wallopen1 == 1)
            {

                door2.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
                jumptimes = 0;
            }
        }
        if (other.gameObject.name == "flooropen3")
        {
            jumptimes = jumptimes + 1;
            if (jumptimes == 3 && wallopen2 == 1)
            {

                door3.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
                jumptimes = 0;
            }
        }
        if (other.gameObject.name == "flooropen4")
        {
            jumptimes = jumptimes + 1;
            if (jumptimes == 5 && wallopen1 == 1 && wallopen2 == 1 && wallopen3 == 1)
            {

                door4.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
                jumptimes = 0;
            }
            if(wallopen2 == 0)
            {
                seterrorText();
            }
        }
        if (other.gameObject.name == "lose")
        {
            setloseText();
        }
    }
    IEnumerator CountDown()
    {
        while (TotalTime >= 0)
        {
            timecountText.GetComponent<Text>().text = TotalTime.ToString();
            yield return new WaitForSeconds(1);
            TotalTime--;
        }
    }

    private void setaxText()
    {
        axText.text = "you get ax to break the wall, press space to use it";
    }
    private void seterrorText()
    {
        errorText.text = "you have to possess sunglassess";
    }
    private void setloseText()
    {
        loseText.text = "you lose";
    }
    private void settimecountText()
    {
        timecountText.text = "Time Count: " + TotalTime.ToString();
        if (TotalTime <= 0)
        {
            loseText.text = "you lose";
        }
    }

    public void resetscreen()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
