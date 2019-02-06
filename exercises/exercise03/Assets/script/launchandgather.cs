using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class launchandgather : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;
    public Text timecountText;
    public Text winText;
    public Text loseText;
    public Text ballsText;
    public Text goalsText;
    private Rigidbody rb;
    private int TotalTime;
    private int Totalballs;
    private int Totalgoals;
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        TotalTime = 60;
        Totalballs = 3;
        Totalgoals = 1;
        settimecountText();
        setballsText();
        setgoalsText();
        winText.text = "";
        loseText.text = "";
        goalsText.text = "";
        StartCoroutine(CountDown());
    }
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "trap")
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.transform.position = startPosition;
            gameObject.transform.rotation = startRotation;
            Totalballs = Totalballs - 1;
            setballsText();


        }
        if (other.gameObject.CompareTag("clock"))
        {
            other.gameObject.SetActive(false);
            TotalTime = TotalTime + 20;
            settimecountText();
        }
        if (other.gameObject.CompareTag("moreballs"))
        {
            other.gameObject.SetActive(false);
            Totalballs = Totalballs + 1;
        }
        if (other.gameObject.CompareTag("goal"))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.transform.position = startPosition;
            gameObject.transform.rotation = startRotation;
            setgoalsText();
            Totalgoals = Totalgoals + 1;
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
    private void settimecountText()
    {
        timecountText.text = "Time Count: " + TotalTime.ToString();
        if (TotalTime <= 0)
        {
            loseText.text = "You lose";
        }
    }
    private void setballsText()
    {
        ballsText.text = "balls:" + Totalballs.ToString();
        if (Totalballs <= 0)
        {
            loseText.text = "You lose";
        }
    }

    private void setgoalsText()
    {
        goalsText.text = "goals:" + Totalgoals.ToString();
        if (Totalgoals >= 3)
        {
            winText.text = "You win";
        }
    }
}