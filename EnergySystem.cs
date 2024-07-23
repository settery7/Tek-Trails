using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    public int maxEnergy = 3;
    private int currentEnergy;
    public Image[] energyImages;
    public Button ReplenishButton;

    public bool NotInteractable;

    void Start()
    {
        currentEnergy = maxEnergy;
        UpdateEnergyUI();
        ReplenishButton.onClick.AddListener(ReplenishEnergy);
    }

    public void UseEnergy()
    {
        if (currentEnergy > 0)
        {
            currentEnergy--;
            UpdateEnergyUI();

            if(currentEnergy == 0)
            {
                NotInteractable = true;
            }
        }
    }

    void UpdateEnergyUI()
    {
        for (int i = 0; i < energyImages.Length; i++)
        {
            if (i < currentEnergy)
            {
                energyImages[i].enabled = true;
            }
            else
            {
                energyImages[i].enabled = false;
            }
        }
    }

    public void ReplenishEnergy()
    {
        currentEnergy = maxEnergy;
        UpdateEnergyUI();
        NotInteractable = false;
    }

    public bool IsEnergyDepleted()
    {
        return currentEnergy == 0;
    }
    /*public void EnergyButtons()
    {
        switch (gameObject.name)
        {
            case "CIT-NGE":
                UseEnergy();
                break;
            case "CIT-RTL":
                UseEnergy();
                break;
            case "CIT-Allied":
                UseEnergy();
                break;
            case "CIT-SAL":
                UseEnergy();
                break;
            case "CIT-Library-Alt":
                UseEnergy();
                break;
            case "CIT-GLE":
                UseEnergy();
                break;
            case "CIT-Elementary Blg.":
                UseEnergy();
                break;
            case "CIT-Elementarym Blg. (1)":
                UseEnergy();
                break;
            case "CIT-ACAD":
                UseEnergy();
                break;
            case "CIT-Canteen":
                UseEnergy();
                break;
            case "CIT-Gym":
                UseEnergy();
                break;
            default:
                break;
        }
    }

    public void TriggerEnergyButton(string buttonName)
    {
        gameObject.name = buttonName;
        EnergyButtons();
    }*/
}

