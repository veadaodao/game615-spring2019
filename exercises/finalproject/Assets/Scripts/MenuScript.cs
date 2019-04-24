using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject paintingPanel;
    public Image oneImage;
    public Image twoImage;
    public Image threeImage;
    public Image fourImage;
    public Image fiveImage;
    public Image sixImage;
    public Image sevenImage;
    public Image eightImage;

    public GameObject informationPanel;
    void Start()
    {
        StartCoroutine(startpainting());
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    IEnumerator startpainting()
    {
        while (true)
        {

            yield return new WaitForSeconds(0.5f);

            oneImage.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            twoImage.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            threeImage.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            fourImage.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            fiveImage.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            sixImage.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            sevenImage.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            eightImage.gameObject.SetActive(false);
            paintingPanel.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            informationPanel.SetActive(true);
            yield return null;
        }
    }

    public void resetscreen()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void informationscreen()
    {
        SceneManager.LoadScene("SimpledescribeScene");
    }
}
