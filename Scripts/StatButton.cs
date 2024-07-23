using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class StatButton : MonoBehaviour
{
    public CharacterStats characterStats;
    /*public Button intellectButton;
    public Button physicalButton;
    public Button happinessButton;
    public Button hungerButton;
    public TMP_Text buttonText;*/
    public int increaseAmountStats = 1;
    public int increaseAmountMoodHunger = 10;

    void Start()
    {
        /*if (intellectButton != null)
        {
            intellectButton.onClick.AddListener(RaiseIntellect);
        }
        if (physicalButton != null)
        {
            physicalButton.onClick.AddListener(RaisePhysical);
        }
        if (happinessButton != null)
        {
            happinessButton.onClick.AddListener(RaiseHappiness);
        }
        if (hungerButton != null)
        {
            hungerButton.onClick.AddListener(RaiseHunger);
        }*/
    }

    public void RaiseIntellect()
    {
        StartCoroutine(RaiseStatAfterDelay("Intellect"));
    }

    public void RaisePhysical()
    {
        StartCoroutine(RaiseStatAfterDelay("Physical"));
    }

    public void RaiseHappiness()
    {
        StartCoroutine(RaiseMoodHungerAfterDelay("Happiness"));
    }

    public void RaiseHunger()
    {
        StartCoroutine(RaiseMoodHungerAfterDelay("Hunger"));
    }

    IEnumerator RaiseStatAfterDelay(string stat)
    {
        //buttonText.text = "Please wait...";
        //yield return new WaitForSeconds(5);
        //characterStats.SetStat(stat, characterStats.GetStatValue(stat) + increaseAmountStats);
        //buttonText.text = $"Add {increaseAmountStats} to {stat}";

        yield return new WaitForSeconds(1);

        int currentStatValue = characterStats.GetStatValue(stat);
        int newValue = Mathf.Clamp(currentStatValue + increaseAmountStats, 0, 100);

        Debug.Log($"Current {stat}: {currentStatValue}, New Value: {newValue}");
        characterStats.SetStat(stat, newValue);
    }

    IEnumerator RaiseMoodHungerAfterDelay(string stat)
    {
        // buttonText.text = "Please wait...";
        //yield return new WaitForSeconds(5);
        //characterStats.SetStat(stat, characterStats.GetStatValue(stat) + increaseAmountMoodHunger);
        //buttonText.text = $"Add {increaseAmountMoodHunger} to {stat}";

        yield return new WaitForSeconds(1);

        int currentStatValue = characterStats.GetStatValue(stat);
        int newValue = Mathf.Clamp(currentStatValue + increaseAmountMoodHunger, 0, 100);

        Debug.Log($"Current {stat}: {currentStatValue}, New Value: {newValue}");
        characterStats.SetStat(stat, newValue);
    }
}
