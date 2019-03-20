using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject selectedUnitPanel;


    public Image portraitImage;
    public Text nameText;
    public Text bioText;
    public ToggleGroup actionToggleGroup;

    public UnitScript selectedUnit;

    public string currentAction = "";



    public void selectUnit(UnitScript unit)
    {
        selectedUnit = unit;

        if (unit != null)
        {

            nameText.text = unit.unitName;
            bioText.text = unit.bio;
            portraitImage.sprite = unit.portrait;

            unit.selected = true;

            selectedUnitPanel.SetActive(true);
        }
        else
        {

            resetUI();
        }


        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");


        for (int i = 0; i < units.Length; i++)
        {
            UnitScript us = units[i].GetComponent<UnitScript>();
            if (selectedUnit != us)
            {
                us.selected = false;
            }
            us.updateVisuals();
        }

    }

    void resetUI()
    {
        actionToggleGroup.SetAllTogglesOff();
        currentAction = "";
        selectedUnitPanel.SetActive(false);
    }


}
