using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public Image fadePanelImg;

    public GameObject zombieTalkPanel;
    public TMP_Text zombieText;

    public GameObject people;
    public GameObject stop;

    public GameObject police;
    public GameObject textcube;
    public GameObject zombie;
    public GameObject policeTalkPanel;
    public TMP_Text policeText;

    public GameObject police1TalkPanel;
    public TMP_Text police1Text;

    public GameObject peopleTalkPanel;
    public TMP_Text peopleText;

    public GameObject people1TalkPanel;
    public TMP_Text people1Text;


    void Start()
    {
        Screen.SetResolution(1024, 768, false);
        StartCoroutine(fadeIn());
    }


    void Update()
    {

        if (zombieTalkPanel.activeSelf && police != null)
        {
            Vector3 zombieTalkPanelPos = Camera.main.WorldToScreenPoint(zombie.transform.position + Vector3.forward * 1f);
            zombieTalkPanel.transform.position = zombieTalkPanelPos;
        }

    }
    
    IEnumerator fadeIn()
    {

        while (fadePanelImg.color.a > 0)
        {
            float newAlpha = fadePanelImg.color.a - 0.5f * Time.deltaTime;
            fadePanelImg.color = new Color(0, 0, 0, newAlpha);

            yield return null;

        }

        fadePanelImg.gameObject.SetActive(false);
        StartCoroutine(zombiesScene());
    }

    IEnumerator zombiesScene()
    {
        while (true)
        {

            yield return new WaitForSeconds(1);
            policeText.text = "There is a voice?";
            policeTalkPanel.SetActive(true);

            yield return new WaitForSeconds(1);
            policeTalkPanel.SetActive(false);

            yield return new WaitForSeconds(1);
            peopleText.text = "Help!!!";
            peopleTalkPanel.SetActive(true);

            yield return new WaitForSeconds(1);
            peopleTalkPanel.SetActive(false);

            yield return new WaitForSeconds(1);
            people1Text.text = "NO NO NO Run!!!";
            people1TalkPanel.SetActive(true);

            yield return new WaitForSeconds(0.5f);
            people1TalkPanel.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            police1Text.text = "Run!!! Aghhhh!";
            police1TalkPanel.SetActive(true);

            yield return new WaitForSeconds(1);
            police1TalkPanel.SetActive(false);

            yield return new WaitForSeconds(0.5f);



            zombieText.text = "brains!!!";
            zombieTalkPanel.SetActive(true);

            yield return new WaitForSeconds(3);

            zombieTalkPanel.SetActive(false);


            yield return new WaitForSeconds(3);

            Destroy(police);

        }
    }
}