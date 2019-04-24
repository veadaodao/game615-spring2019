using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    //player movement
    CharacterController cc;
    bool previousIsGroundedValue;
    float moveSpeed = 10f;
    float rotateSpeed = 60f;
    float jumpForce = 0.35f;
    float gravityModifier = 0.2f;
    float yVelocity = 0;

    //UI
    //original painting
    public GameObject DreamPanel;
    public Image FirstImg;
    public Image bootImage;
    public Image greentreeImage;
    public Image whiteflowerImage;
    public Image sunflowerImage;
    public Image blueflowerImage;
    public Image starnightImage;
    public Image roomImage;
    public Image selfvangoghImage;
    public Image crowImage;

    //tutorial
    public GameObject tutorialPanel;
    public Image hintcircle;

    public GameObject tutorial1Canvans;
    public GameObject tutorial2Canvans;
    public GameObject tutorial3Canvans;
    public GameObject tutorial4Canvans;
    public GameObject tutorial5Canvans;
    //hint question mark
    public GameObject HintcolorcirclePanel;

    //right or wrong
    public GameObject rightPanel;
    public GameObject wrongPanel;

    //food panel
    public GameObject buyfoodPanel;
    public GameObject needfoodPanel;
    //paintingseller panel
    public GameObject paintingsellerPanel;
    //inform money panel
    public GameObject InformsellpaintingPanel;

    //question painting
    public GameObject PaintingPanel;
    public Image FirstImagesketch;
    public Image BootImagesketch;
    public Image greentreesketchImage;
    public Image whiteflowersketchImage;
    public Image sunflowersketchImage;
    public Image blueflowersketchImage;
    public Image starnightsketchImage;
    public Image roomsketchImage;
    public Image selfvangoghsketchImage;
    public Image crowsketchImage;
    //time
    public Image time1bar;
    public Image time2bar;
    private float timerdreaming = 10;
    private float timerpainting = 10;

    private bool dream = false;
    private bool painting = false;
    private bool playermove = true;
    private int tutorial = 1;

    private bool resetbuttonclick = false;
    private bool playeranswer = false;

    private int hintalreadyopen = 1;

    private int gotobed = 0;
    private int gotoeasel = 0;
    //Text

    public Text DreamText;
    //money 
    private int Money;
    public Image MoneyImage;
    //health
    private float Health;
    public Image HealthImage;
    //hungry
    private float Hungry;
    public Image HungryImage;

    //possess pigment
    private bool Havingpigment = true;

    //finish painting
    private bool Finishpainting = false;

    //day and night
    private float Oneday = 90;
    public GameObject directlight;

    public Image fadePanelImg;

    public Animator hintanimation;

    //audio 
    public AudioClip eataudio;
    public AudioSource plate;

    public AudioClip drinkaudio;
    public AudioSource vesel;

    //color button
    private bool white = false;
    private bool lemonyellow = false;
    private bool primroseyellow = false;
    private bool orange = false;
    private bool cerise = false;
    private bool grassgreen = false;
    private bool olivegreen = false;
    private bool lakeblue = false;
    private bool navyblue = false;
    private bool prussianblue = false;

    void Start()
    {
        Screen.SetResolution(1280, 768, false);
        cc = gameObject.GetComponent<CharacterController>();
        previousIsGroundedValue = cc.isGrounded;
        Money = 0;
        Health = 100;
        Hungry = 100;
        StartCoroutine(Fadein());
    }

    void Update()
    {
        //player movement
        if (playermove == true)
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
        }

        //Dream Time go
        if (dream == true)
        {
            Timerdreaming();
        }
        //Painting Time go
        if (painting == true)
        {
            Timerpainting();
        }
        if (resetbuttonclick == true)
        {
            wrongPanel.SetActive(false);
        }
        if (Hungry > 0)
        {
            Hungry -= Time.deltaTime/2;
            HungryImage.fillAmount = Hungry / 100.0f;
        }
        if (Hungry == 0)
        {
            Health -= Time.deltaTime*2;
            HealthImage.fillAmount = Health / 100.0f;
        }
        MoneyImage.fillAmount = Money / 100.0f;
        if (Money > 100)
        {
            Money = 100;
        }
        if (Hungry > 100)
        {
            Hungry = 100;
        }
        if (Hungry < 0)
        {
            Hungry = 0f;
        }
        if (Health > 100)
        {
            Health = 100;
        }
        if (Hungry < 20)
        {
            needfoodPanel.SetActive(true);
        }
        if (Hungry > 30)
        {
            needfoodPanel.SetActive(false);
        }
        if (Money < 0 && Finishpainting == true)
        {
            InformsellpaintingPanel.SetActive(true);
        }
        if (Money == 100)
        {
            InformsellpaintingPanel.SetActive(false);
        }
        if (Oneday >= 0)
        {
            Oneday -= Time.deltaTime;
            directlight.gameObject.SetActive(true);
        }
        if (Oneday < 0)
        {
            Oneday -= Time.deltaTime;
            directlight.gameObject.SetActive(false);
        }
        if (Oneday <= -90)
        {
            Oneday = 90;
        }
        if (Health <= 0)
        {
            SceneManager.LoadScene("GameoverScene");
        }

    }

    void OnTriggerEnter(Collider other)
    {
        //player ontriggerenter with bed
        if (other.gameObject.name == "Bed")
        {
            ResetdreamingTimer();
            if (gotobed == 0)
            {
                DreamPanel.SetActive(true);
                FirstImg.gameObject.SetActive(true);
                tutorial = tutorial + 1;
                StartCoroutine(Showtutorial());
                dream = true;
                playermove = false;
                DreamText.text = "I need paint this painting, which is so beautiful.The potato eaters, they have simple but happy life.";
            }
            if (gotobed == 1)
            {
                DreamPanel.SetActive(true);
                bootImage.gameObject.SetActive(true);
                dream = true;
                playermove = false;
            }
            if (gotobed == 2)
            {
                DreamPanel.SetActive(true);
                greentreeImage.gameObject.SetActive(true);
                dream = true;
                playermove = false;
            }
            if (gotobed == 3)
            {
                DreamPanel.SetActive(true);
                whiteflowerImage.gameObject.SetActive(true);
                dream = true;
                playermove = false;
            }
            if (gotobed == 4)
            {
                DreamPanel.SetActive(true);
                sunflowerImage.gameObject.SetActive(true);
                dream = true;
                playermove = false;
            }
            if (gotobed == 5)
            {
                DreamPanel.SetActive(true);
                blueflowerImage.gameObject.SetActive(true);
                dream = true;
                playermove = false;
            }
            if (gotobed == 6)
            {
                DreamPanel.SetActive(true);
                starnightImage.gameObject.SetActive(true);
                dream = true;
                playermove = false;
            }
            if (gotobed == 7)
            {
                DreamPanel.SetActive(true);
                roomImage.gameObject.SetActive(true);
                dream = true;
                playermove = false;
            }
            if (gotobed == 8)
            {
                DreamPanel.SetActive(true);
                selfvangoghImage.gameObject.SetActive(true);
                dream = true;
                playermove = false;
            }
            if (gotobed == 9)
            {
                DreamPanel.SetActive(true);
                crowImage.gameObject.SetActive(true);
                dream = true;
                playermove = false;
            }
        }
        //player ontriggerenter with easel
        if (other.gameObject.name == "easel")
        {
            ResetpaintingTimer();
            Resetcolor();
            if (gotoeasel == 0 && Havingpigment==true)
            {
                tutorial2Canvans.gameObject.SetActive(false);
                painting = true;
                PaintingPanel.SetActive(true);
                FirstImagesketch.gameObject.SetActive(true);
                playermove = false;
                Finishpainting = false;
            }
            if (gotoeasel == 1 && Havingpigment == true)
            {
                PaintingPanel.SetActive(true);
                BootImagesketch.gameObject.SetActive(true);
                painting = true;
                playermove = false;
                Finishpainting = false;
            }
            if (gotoeasel == 2 && Havingpigment == true)
            {
                PaintingPanel.SetActive(true);
                greentreesketchImage.gameObject.SetActive(true);
                painting = true;
                playermove = false;
                Finishpainting = false;
            }
            if (gotoeasel == 3 && Havingpigment == true)
            {
                PaintingPanel.SetActive(true);
                whiteflowersketchImage.gameObject.SetActive(true);
                painting = true;
                playermove = false;
                Finishpainting = false;
            }
            if (gotoeasel == 4 && Havingpigment == true)
            {
                PaintingPanel.SetActive(true);
                sunflowersketchImage.gameObject.SetActive(true);
                painting = true;
                playermove = false;
                Finishpainting = false;
            }
            if (gotoeasel == 5 && Havingpigment == true)
            {
                PaintingPanel.SetActive(true);
                blueflowersketchImage.gameObject.SetActive(true);
                painting = true;
                playermove = false;
                Finishpainting = false;
            }
            if (gotoeasel == 6 && Havingpigment == true)
            {
                PaintingPanel.SetActive(true);
                starnightsketchImage.gameObject.SetActive(true);
                painting = true;
                playermove = false;
                Finishpainting = false;
            }
            if (gotoeasel == 7 && Havingpigment == true)
            {
                PaintingPanel.SetActive(true);
                roomsketchImage.gameObject.SetActive(true);
                painting = true;
                playermove = false;
                Finishpainting = false;
            }
            if (gotoeasel == 8 && Havingpigment == true)
            {
                PaintingPanel.SetActive(true);
                selfvangoghsketchImage.gameObject.SetActive(true);
                painting = true;
                playermove = false;
                Finishpainting = false;
            }
            if (gotoeasel == 9 && Havingpigment == true)
            {
                PaintingPanel.SetActive(true);
                crowsketchImage.gameObject.SetActive(true);
                painting = true;
                playermove = false;
                Finishpainting = false;
            }
        }
        //buy food
        if (other.gameObject.name == "Bench")
        {
            buyfoodPanel.SetActive(true);
            tutorial4Canvans.gameObject.SetActive(false);
        }
        //painting seller
        if (other.gameObject.name == "paintingsellerCube")
        {
            paintingsellerPanel.SetActive(true);
            tutorial3Canvans.gameObject.SetActive(false);
            tutorial5Canvans.gameObject.SetActive(false);
        }
    }

    public void Timerdreaming()
    {
        //dreaming process
        //1 painting
        if (gotobed == 0)
        {
            if (timerdreaming > 0)
            {
                timerdreaming -= Time.deltaTime;
                time1bar.fillAmount = timerdreaming / 10.0f;

                if (timerdreaming < 0)
                {
                    DreamPanel.SetActive(false);
                    FirstImg.gameObject.SetActive(false);
                    playermove = true;
                    gotobed += 1;
                }
            }
        }
        //2 painting
        if (gotobed == 1)
        {
            if (timerdreaming > 0)
            {
                timerdreaming -= Time.deltaTime;
                time1bar.fillAmount = timerdreaming / 10.0f;
                if (timerdreaming < 0)
                {
                    DreamPanel.SetActive(false);
                    bootImage.gameObject.SetActive(false);
                    playermove = true;
                    gotobed = 2;
                }
            }
        }
        //3 painting
        if (gotobed == 2)
        {
            if (timerdreaming > 0)
            {
                timerdreaming -= Time.deltaTime;
                time1bar.fillAmount = timerdreaming / 10.0f;
                if (timerdreaming < 0)
                {
                    DreamPanel.SetActive(false);
                    greentreeImage.gameObject.SetActive(false);
                    playermove = true;
                    gotobed = 3;
                }
            }
        }
        if (gotobed == 3)
        {
            if (timerdreaming > 0)
            {
                timerdreaming -= Time.deltaTime;
                time1bar.fillAmount = timerdreaming / 10.0f;
                if (timerdreaming < 0)
                {
                    DreamPanel.SetActive(false);
                    whiteflowerImage.gameObject.SetActive(false);
                    playermove = true;
                    gotobed = 4;
                }
            }
        }
        if (gotobed == 4)
        {
            if (timerdreaming > 0)
            {
                timerdreaming -= Time.deltaTime;
                time1bar.fillAmount = timerdreaming / 10.0f;
                if (timerdreaming < 0)
                {
                    DreamPanel.SetActive(false);
                    sunflowerImage.gameObject.SetActive(false);
                    playermove = true;
                    gotobed = 5;
                }
            }
        }
        if (gotobed == 5)
        {
            if (timerdreaming > 0)
            {
                timerdreaming -= Time.deltaTime;
                time1bar.fillAmount = timerdreaming / 10.0f;
                if (timerdreaming < 0)
                {
                    DreamPanel.SetActive(false);
                    blueflowerImage.gameObject.SetActive(false);
                    playermove = true;
                    gotobed = 6;
                }
            }
        }
        if (gotobed == 6)
        {
            if (timerdreaming > 0)
            {
                timerdreaming -= Time.deltaTime;
                time1bar.fillAmount = timerdreaming / 10.0f;
                if (timerdreaming < 0)
                {
                    DreamPanel.SetActive(false);
                    starnightImage.gameObject.SetActive(false);
                    playermove = true;
                    gotobed = 7;
                }
            }
        }
        if (gotobed == 7)
        {
            if (timerdreaming > 0)
            {
                timerdreaming -= Time.deltaTime;
                time1bar.fillAmount = timerdreaming / 10.0f;
                if (timerdreaming < 0)
                {
                    DreamPanel.SetActive(false);
                    roomImage.gameObject.SetActive(false);
                    playermove = true;
                    gotobed = 8;
                }
            }
        }
        if (gotobed == 8)
        {
            if (timerdreaming > 0)
            {
                timerdreaming -= Time.deltaTime;
                time1bar.fillAmount = timerdreaming / 10.0f;
                if (timerdreaming < 0)
                {
                    DreamPanel.SetActive(false);
                    selfvangoghImage.gameObject.SetActive(false);
                    playermove = true;
                    gotobed = 9;
                }
            }
        }
        if (gotobed == 9)
        {
            if (timerdreaming > 0)
            {
                timerdreaming -= Time.deltaTime;
                time1bar.fillAmount = timerdreaming / 10.0f;
                if (timerdreaming < 0)
                {
                    DreamPanel.SetActive(false);
                    crowImage.gameObject.SetActive(false);
                    playermove = true;
                    gotobed = 10;
                }
            }
        }
    }

    public void Timerpainting()
    {
        //0 painting sketch
        if (gotoeasel == 0)
        {
            if (timerpainting > 0)
            {
                timerpainting -= Time.deltaTime;
                time2bar.fillAmount = timerpainting / 10.0f;
                if (orange == true && lakeblue == true)
                {
                    rightPanel.SetActive(true);
                    PaintingPanel.SetActive(false);
                    FirstImagesketch.gameObject.SetActive(false);
                    DreamPanel.SetActive(true);
                    FirstImg.gameObject.SetActive(true);
                    playermove = true;
                    resetbuttonclick = true;
                    playeranswer = true;
                    Finishpainting = true;
                    Havingpigment = false;
                    Hungry -= 0.09f;
                    tutorial5Canvans.gameObject.SetActive(true);
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        DreamPanel.SetActive(false);
                        FirstImg.gameObject.SetActive(false);
                        rightPanel.SetActive(false);
                        playermove = true;
                        gotoeasel += 1;
                        painting = false;                        
                    }
                }
                if (white == true || lemonyellow == true || primroseyellow == true || cerise == true || navyblue == true || grassgreen == true
                    || olivegreen == true || prussianblue == true)
                {
                    Debug.Log("wrong color");
                    resetbuttonclick = false;
                    wrongPanel.SetActive(true);
                    Hungry -= 10f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        FirstImg.gameObject.SetActive(false);
                        wrongPanel.SetActive(false);
                        playermove = true;
                        painting = false;
                    }
                }
                if (timerpainting < 0 && resetbuttonclick == false && playeranswer==false)
                {
                    Debug.Log("1button");
                    PaintingPanel.SetActive(false);
                    FirstImg.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                }
                if (timerpainting < 0 && resetbuttonclick == true && playeranswer == false)
                {
                    Debug.Log("2button");
                    PaintingPanel.SetActive(false);
                    FirstImg.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                    resetbuttonclick = false;
                }
            }
        }
        //1 boot painting sketch
        if (gotoeasel == 1)
        {
            if (timerpainting > 0)
            {
                timerpainting -= Time.deltaTime;
                time2bar.fillAmount = timerpainting / 10.0f;
                if (orange == true && lakeblue == true && white== true)
                {
                    rightPanel.SetActive(true);
                    PaintingPanel.SetActive(false);
                    BootImagesketch.gameObject.SetActive(false);
                    DreamPanel.SetActive(true);
                    bootImage.gameObject.SetActive(true);
                    playermove = true;
                    resetbuttonclick = true;
                    playeranswer = true;
                    Finishpainting = true;
                    Havingpigment = false;
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        DreamPanel.SetActive(false);
                        bootImage.gameObject.SetActive(false);
                        rightPanel.SetActive(false);
                        playermove = true;
                        gotoeasel += 1;
                        painting = false;
                        resetbuttonclick = true;
                    }
                }
                if (lemonyellow == true || primroseyellow == true || cerise == true || navyblue == true || grassgreen == true
                    || olivegreen == true || prussianblue == true)
                {
                    Debug.Log("wrong color");
                    resetbuttonclick = false;
                    wrongPanel.SetActive(true);
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        bootImage.gameObject.SetActive(false);
                        wrongPanel.SetActive(false);
                        playermove = true;
                        painting = false;
                    }
                }
                if (timerpainting < 0 && resetbuttonclick == false && playeranswer == false)
                {
                    PaintingPanel.SetActive(false);
                    bootImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                }
                if (timerpainting < 0 && resetbuttonclick == true && playeranswer == false)
                {
                    PaintingPanel.SetActive(false);
                    bootImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                    resetbuttonclick = false;
                }
            }
        }
        //2 greentree painting sketch
        if (gotoeasel == 2)
        {
            if (timerpainting > 0)
            {
                Debug.Log("2painting");
                timerpainting -= Time.deltaTime;
                time2bar.fillAmount = timerpainting / 10.0f;
                if (olivegreen == true && lakeblue == true && white == true && lemonyellow == true && grassgreen==true)
                {
                    rightPanel.SetActive(true);
                    PaintingPanel.SetActive(false);
                    greentreesketchImage.gameObject.SetActive(false);
                    DreamPanel.SetActive(true);
                    greentreeImage.gameObject.SetActive(true);
                    playermove = true;
                    resetbuttonclick = true;
                    playeranswer = true;
                    Finishpainting = true;
                    Havingpigment = false;
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        DreamPanel.SetActive(false);
                        greentreeImage.gameObject.SetActive(false);
                        rightPanel.SetActive(false);
                        playermove = true;
                        gotoeasel += 1;
                        painting = false;
                        resetbuttonclick = true;
                    }
                }
                if (primroseyellow == true || cerise == true || navyblue == true || orange == true|| prussianblue == true)                    
                {
                    Debug.Log("wrong color");
                    resetbuttonclick = false;
                    wrongPanel.SetActive(true);
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        greentreeImage.gameObject.SetActive(false);
                        wrongPanel.SetActive(false);
                        playermove = true;
                        painting = false;
                    }
                }
                if (timerpainting < 0 && resetbuttonclick == false && playeranswer == false)
                {
                    PaintingPanel.SetActive(false);
                    greentreeImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                }
                if (timerpainting < 0 && resetbuttonclick == true && playeranswer == false)
                {
                    PaintingPanel.SetActive(false);
                    greentreeImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                    resetbuttonclick = false;
                }
            }
        }
        //3 whiteflower painting sketch
        if (gotoeasel == 3)
        {
            if (timerpainting > 0)
            {
                Debug.Log("3painting");
                timerpainting -= Time.deltaTime;
                time2bar.fillAmount = timerpainting / 10.0f;
                if (orange == true && lakeblue == true && white == true && lemonyellow == true && grassgreen == true)
                {
                    rightPanel.SetActive(true);
                    PaintingPanel.SetActive(false);
                    whiteflowersketchImage.gameObject.SetActive(false);
                    DreamPanel.SetActive(true);
                    whiteflowerImage.gameObject.SetActive(true);
                    playermove = true;
                    resetbuttonclick = true;
                    playeranswer = true;
                    Finishpainting = true;
                    Havingpigment = false;
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        DreamPanel.SetActive(false);
                        whiteflowerImage.gameObject.SetActive(false);
                        rightPanel.SetActive(false);
                        playermove = true;
                        gotoeasel += 1;
                        painting = false;
                        resetbuttonclick = true;
                    }
                }
                if (primroseyellow == true || cerise == true || navyblue == true || olivegreen == true || prussianblue == true)
                {
                    Debug.Log("wrong color");
                    resetbuttonclick = false;
                    wrongPanel.SetActive(true);
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        whiteflowerImage.gameObject.SetActive(false);
                        wrongPanel.SetActive(false);
                        playermove = true;
                        painting = false;
                    }
                }
                if (timerpainting < 0 && resetbuttonclick == false && playeranswer == false)
                {
                    PaintingPanel.SetActive(false);
                    whiteflowerImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                }
                if (timerpainting < 0 && resetbuttonclick == true && playeranswer == false)
                {
                    PaintingPanel.SetActive(false);
                    whiteflowerImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                    resetbuttonclick = false;
                }
            }
        }
        //4 sunflower painting sketch
        if (gotoeasel == 4)
        {
            if (timerpainting > 0)
            {
                timerpainting -= Time.deltaTime;
                time2bar.fillAmount = timerpainting / 10.0f;
                if (orange == true && lakeblue == true && white == true && olivegreen==true && navyblue == true && lemonyellow == true && cerise == true)
                {
                    rightPanel.SetActive(true);
                    PaintingPanel.SetActive(false);
                    sunflowersketchImage.gameObject.SetActive(false);
                    DreamPanel.SetActive(true);
                    sunflowerImage.gameObject.SetActive(true);
                    playermove = true;
                    resetbuttonclick = true;
                    playeranswer = true;
                    Finishpainting = true;
                    Havingpigment = false;
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        DreamPanel.SetActive(false);
                        sunflowerImage.gameObject.SetActive(false);
                        rightPanel.SetActive(false);
                        playermove = true;
                        gotoeasel += 1;
                        painting = false;
                    }
                }
                if (primroseyellow == true || grassgreen == true || prussianblue == true)           
                {
                    Debug.Log("wrong color");
                    resetbuttonclick = false;
                    wrongPanel.SetActive(true);
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        sunflowerImage.gameObject.SetActive(false);
                        wrongPanel.SetActive(false);
                        playermove = true;
                        painting = false;
                    }
                }
                if (timerpainting < 0 && resetbuttonclick == false && playeranswer == false)
                {
                    Debug.Log("1button");
                    PaintingPanel.SetActive(false);
                    sunflowerImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                }
                if (timerpainting < 0 && resetbuttonclick == true && playeranswer == false)
                {
                    Debug.Log("2button");
                    PaintingPanel.SetActive(false);
                    sunflowerImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                    resetbuttonclick = false;
                }
            }
        }
        //5 blueflower painting sketch
        if (gotoeasel == 5)
        {
            if (timerpainting > 0)
            {
                timerpainting -= Time.deltaTime;
                time2bar.fillAmount = timerpainting / 10.0f;
                if (orange == true && lakeblue == true && white == true && olivegreen == true && navyblue == true && lemonyellow == true 
                    && cerise == true && grassgreen == true)
                {
                    rightPanel.SetActive(true);
                    PaintingPanel.SetActive(false);
                    blueflowersketchImage.gameObject.SetActive(false);
                    DreamPanel.SetActive(true);
                    blueflowerImage.gameObject.SetActive(true);
                    playermove = true;
                    resetbuttonclick = true;
                    playeranswer = true;
                    Finishpainting = true;
                    Havingpigment = false;
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        DreamPanel.SetActive(false);
                        blueflowerImage.gameObject.SetActive(false);
                        rightPanel.SetActive(false);
                        playermove = true;
                        gotoeasel += 1;
                        painting = false;
                    }
                }
                if (primroseyellow == true || prussianblue == true)
                {
                    Debug.Log("wrong color");
                    resetbuttonclick = false;
                    wrongPanel.SetActive(true);
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        blueflowerImage.gameObject.SetActive(false);
                        wrongPanel.SetActive(false);
                        playermove = true;
                        painting = false;
                    }
                }
                if (timerpainting < 0 && resetbuttonclick == false && playeranswer == false)
                {
                    Debug.Log("1button");
                    PaintingPanel.SetActive(false);
                    blueflowerImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                }
                if (timerpainting < 0 && resetbuttonclick == true && playeranswer == false)
                {
                    Debug.Log("2button");
                    PaintingPanel.SetActive(false);
                    blueflowerImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                    resetbuttonclick = false;
                }
            }
        }
        //6 starnight painting sketch
        if (gotoeasel == 6)
        {
            if (timerpainting > 0)
            {
                timerpainting -= Time.deltaTime;
                time2bar.fillAmount = timerpainting / 10.0f;
                if (lakeblue == true && white == true && navyblue == true && lemonyellow == true && prussianblue == true && primroseyellow == true)
                {
                    rightPanel.SetActive(true);
                    PaintingPanel.SetActive(false);
                    starnightsketchImage.gameObject.SetActive(false);
                    DreamPanel.SetActive(true);
                    starnightImage.gameObject.SetActive(true);
                    playermove = true;
                    resetbuttonclick = true;
                    playeranswer = true;
                    Finishpainting = true;
                    Havingpigment = false;
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        DreamPanel.SetActive(false);
                        starnightImage.gameObject.SetActive(false);
                        rightPanel.SetActive(false);
                        playermove = true;
                        gotoeasel += 1;
                        painting = false;
                    }
                }
                if (orange == true || olivegreen == true || cerise == true || grassgreen == true)                   
                {
                    Debug.Log("wrong color");
                    resetbuttonclick = false;
                    wrongPanel.SetActive(true);
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        starnightImage.gameObject.SetActive(false);
                        wrongPanel.SetActive(false);
                        playermove = true;
                        painting = false;
                    }
                }
                if (timerpainting < 0 && resetbuttonclick == false && playeranswer == false)
                {
                    Debug.Log("1button");
                    PaintingPanel.SetActive(false);
                    starnightImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                }
                if (timerpainting < 0 && resetbuttonclick == true && playeranswer == false)
                {
                    Debug.Log("2button");
                    PaintingPanel.SetActive(false);
                    starnightImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                    resetbuttonclick = false;
                }
            }
        }
        //7 room painting sketch
        if (gotoeasel == 7)
        {
            if (timerpainting > 0)
            {
                timerpainting -= Time.deltaTime;
                time2bar.fillAmount = timerpainting / 10.0f;
                if (white == true && navyblue == true && lemonyellow == true && prussianblue == true && primroseyellow == true && cerise==true)
                {
                    rightPanel.SetActive(true);
                    PaintingPanel.SetActive(false);
                    roomsketchImage.gameObject.SetActive(false);
                    DreamPanel.SetActive(true);
                    roomImage.gameObject.SetActive(true);
                    playermove = true;
                    resetbuttonclick = true;
                    playeranswer = true;
                    Finishpainting = true;
                    Havingpigment = false;
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        DreamPanel.SetActive(false);
                        roomImage.gameObject.SetActive(false);
                        rightPanel.SetActive(false);
                        playermove = true;
                        gotoeasel += 1;
                        painting = false;
                    }
                }
                if (orange == true || olivegreen == true || grassgreen == true || lakeblue==true)
                {
                    Debug.Log("wrong color");
                    resetbuttonclick = false;
                    wrongPanel.SetActive(true);
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        roomImage.gameObject.SetActive(false);
                        wrongPanel.SetActive(false);
                        playermove = true;
                        painting = false;
                    }
                }
                if (timerpainting < 0 && resetbuttonclick == false && playeranswer == false)
                {
                    Debug.Log("1button");
                    PaintingPanel.SetActive(false);
                    roomImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                }
                if (timerpainting < 0 && resetbuttonclick == true && playeranswer == false)
                {
                    Debug.Log("2button");
                    PaintingPanel.SetActive(false);
                    roomImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                    resetbuttonclick = false;
                }
            }
        }
        //8 selfvangogh painting sketch
        if (gotoeasel == 8)
        {
            if (timerpainting > 0)
            {
                timerpainting -= Time.deltaTime;
                time2bar.fillAmount = timerpainting / 10.0f;
                if (white == true && navyblue == true && lemonyellow == true  && primroseyellow == true && cerise == true && orange ==true && olivegreen == true)
                {
                    rightPanel.SetActive(true);
                    PaintingPanel.SetActive(false);
                    selfvangoghsketchImage.gameObject.SetActive(false);
                    DreamPanel.SetActive(true);
                    selfvangoghImage.gameObject.SetActive(true);
                    playermove = true;
                    resetbuttonclick = true;
                    playeranswer = true;
                    Finishpainting = true;
                    Havingpigment = false;
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        DreamPanel.SetActive(false);
                        selfvangoghImage.gameObject.SetActive(false);
                        rightPanel.SetActive(false);
                        playermove = true;
                        gotoeasel += 1;
                        painting = false;
                    }
                }
                if (grassgreen == true || lakeblue == true || prussianblue == false)
                {
                    Debug.Log("wrong color");
                    resetbuttonclick = false;
                    wrongPanel.SetActive(true);
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        selfvangoghImage.gameObject.SetActive(false);
                        wrongPanel.SetActive(false);
                        playermove = true;
                        painting = false;
                    }
                }
                if (timerpainting < 0 && resetbuttonclick == false && playeranswer == false)
                {
                    Debug.Log("1button");
                    PaintingPanel.SetActive(false);
                    selfvangoghImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                }
                if (timerpainting < 0 && resetbuttonclick == true && playeranswer == false)
                {
                    Debug.Log("2button");
                    PaintingPanel.SetActive(false);
                    selfvangoghImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                    resetbuttonclick = false;
                }
            }
        }
        //9 crow painting sketch
        if (gotoeasel == 9)
        {
            if (timerpainting > 0)
            {
                timerpainting -= Time.deltaTime;
                time2bar.fillAmount = timerpainting / 10.0f;
                if (lemonyellow == true && lakeblue == true && white == true && primroseyellow == true && cerise == true && olivegreen == true && prussianblue == true)
                {
                    rightPanel.SetActive(true);
                    PaintingPanel.SetActive(false);
                    crowsketchImage.gameObject.SetActive(false);
                    DreamPanel.SetActive(true);
                    crowImage.gameObject.SetActive(true);
                    playermove = true;
                    resetbuttonclick = true;
                    playeranswer = true;
                    Finishpainting = true;
                    Havingpigment = false;
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        DreamPanel.SetActive(false);
                        crowImage.gameObject.SetActive(false);
                        rightPanel.SetActive(false);
                        playermove = true;
                        gotoeasel += 1;
                        painting = false;
                    }
                }
                if (navyblue == true || grassgreen == true)                    
                {
                    Debug.Log("wrong color");
                    resetbuttonclick = false;
                    wrongPanel.SetActive(true);
                    Hungry -= 0.09f;
                    if (timerpainting < 0)
                    {
                        PaintingPanel.SetActive(false);
                        crowImage.gameObject.SetActive(false);
                        wrongPanel.SetActive(false);
                        playermove = true;
                        painting = false;
                    }
                }
                if (timerpainting < 0 && resetbuttonclick == false && playeranswer == false)
                {
                    Debug.Log("1button");
                    PaintingPanel.SetActive(false);
                    crowImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                }
                if (timerpainting < 0 && resetbuttonclick == true && playeranswer == false)
                {
                    Debug.Log("2button");
                    PaintingPanel.SetActive(false);
                    crowImage.gameObject.SetActive(false);
                    wrongPanel.SetActive(true);
                    playermove = true;
                    painting = false;
                    resetbuttonclick = false;
                }
            }
        }
    }

    IEnumerator Fadein()
    {
        while (fadePanelImg.color.a > 0)
        {
            float newAlpha = fadePanelImg.color.a - 0.5f * Time.deltaTime;
            fadePanelImg.color = new Color(0, 0, 0, newAlpha);

            yield return null;

        }

        fadePanelImg.gameObject.SetActive(false);
    }

    //eat potato tutorial
    IEnumerator Showtutorial()
    {
        if (tutorial < 3)
        {
            tutorialPanel.SetActive(true);
            tutorial1Canvans.gameObject.SetActive(false);
            yield return new WaitForSeconds(7);
            tutorialPanel.SetActive(false);
            yield return new WaitForSeconds(3);
            tutorial2Canvans.gameObject.SetActive(true);
            yield return null;
        }
    }
   

    private void ResetdreamingTimer()
    {
        if (timerdreaming != 10)
        {
            timerdreaming = 10;
        }       
    }
    private void ResetpaintingTimer()
    {
        if (timerpainting != 10)
        {
            timerpainting = 10;
        }
    }
    private void Playeranwserright() 
    {
        if (playeranswer == false)
        {
            playeranswer = true;
        }
    }

    public void resetbutton()
    {
        white = false;
        lemonyellow = false;
        primroseyellow = false;
        orange = false;
        cerise = false;
        grassgreen = false;
        olivegreen = false;
        lakeblue = false;
        navyblue = false;
        prussianblue = false;
        resetbuttonclick = true;
        ResetpaintingTimer();
    }
    private void Resetcolor()
    {
        white = false;
        lemonyellow = false;
        primroseyellow = false;
        orange = false;
        cerise = false;        
        grassgreen = false;
        olivegreen = false;
        lakeblue = false;
        navyblue = false;
        prussianblue = false;
    }

    public void Openhintcolorcircle()
    {
        HintcolorcirclePanel.SetActive(true);
        hintalreadyopen +=1;
        hintanimation.SetBool("hintopen", true);
        if (hintalreadyopen > 2)
        {
            HintcolorcirclePanel.SetActive(false);
            hintanimation.SetBool("hintopen", false);
            hintalreadyopen = 1;
        }
    }

    //color button
    public void Whitecolor()
    {
        white = true;
    }
    public void Lemonyellowcolor()
    {
        lemonyellow = true;
    }
    public void Primroseyellowcolor()
    {
        primroseyellow = true;
    }
    public void Orangecolor()
    {
        orange = true;
    }
    public void Cerisecolor()
    {
        cerise = true;
    }
    public void Grassgreencolor()
    {
        grassgreen = true;
    }
    public void Olivegreencolor()
    {
        olivegreen = true;
    }
    public void Lakebluecolor()
    {
        lakeblue = true;
    }
    public void Navybluecolor()
    {
        navyblue = true;
    }
    public void Prussianbluecolor()
    {
        prussianblue = true;
    }


    //food button
    public void Buybread()
    {
        Money -= 10;
        if (Hungry < 100)
        {
            Hungry += 50f;
        }
        if (Health<100&& Health > 0)
        {
            Health += 50;
        }
        buyfoodPanel.SetActive(false);
        plate.clip = eataudio;
        plate.Play();
    }

    public void Buypotato()
    {
        Money -= 5;
        if (Hungry < 100)
        {
            Hungry += 20f;
        }
        if (Health < 100 && Health > 0)
        {
            Health += 20;
        }
        buyfoodPanel.SetActive(false);
        plate.clip = eataudio;
        plate.Play();
    }

    public void Buybeer()
    {
        Money -= 3;
        if (Hungry < 100)
        {
            Hungry += 80f;
        }
        if (Health <= 100 && Health > 0)
        {
            Health -= 30;
        }
        buyfoodPanel.SetActive(false);
        vesel.clip = drinkaudio;
        vesel.Play();
    }

    //painting seller
    public void Sellpainting()
    {
        if(Finishpainting == true)
        {
            Money += 100;
            Finishpainting = false;
        }
        paintingsellerPanel.SetActive(false);
    }

    public void Buypigment()
    {
        Money -= 30;
        paintingsellerPanel.SetActive(false);
        if (Havingpigment == false)
        {
            Havingpigment = true;
        }
    }
}
