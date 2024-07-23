using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class CharacterStats : MonoBehaviour
{
    public int Intellect { get; private set; } = 5;
    public int Physical { get; private set; } = 5;
    public int Mood { get; private set; } = 100;
    public int Hunger { get; private set; } = 100;
    public string Status { get; private set; } = "Average";

    public TMP_Text intellectText;
    public TMP_Text physicalText;
    public TMP_Text happinessText;
    public TMP_Text hungerText;
    public TMP_Text statusText;

    

    void Start()
    {
        UpdateStatus();
        UpdateUI();
        StartCoroutine(DeteriorateStats());
    }

    public void SetStat(string stat, int value)
    {
        value = Mathf.Clamp(value, 0, 100);

        switch (stat)
        {
            case "Intellect":
                Intellect = value;
                break;
            case "Physical":
                Physical = value;
                break;
            case "Happiness":
                Mood = value;
                break;
            case "Hunger":
                Hunger = value;
                break;
            default:
                Debug.LogError("Invalid stat name.");
                return;
        }

        UpdateStatus();
        UpdateUI();
    }

    private void UpdateStatus()
    {
        if (Intellect >= 30 && Physical >= 30 && Mood >= 30 && Hunger >= 30)
        {
            Status = "Well-disciplined";
        }
        else if (Physical >= Intellect + 10)
        {
            Status = "Athletic";
        }
        else if (Intellect >= Physical + 10)
        {
            Status = "Intelligent";
        }
        else if (IsAverage())
        {
            Status = "Average";
        }
        else
        {
            Status = "Average";
        }
    }

    private bool IsAverage()
    {
        int[] possibleValues = { 5, 10, 15, 20, 25 };
        return System.Array.Exists(possibleValues, value =>
            value == Intellect && value == Physical && value == Mood && value == Hunger);
    }

    private void UpdateUI()
    {
        intellectText.text = $": {Intellect}";
        physicalText.text = $": {Physical}";
        happinessText.text = $": {Mood}";
        hungerText.text = $": {Hunger}";
        statusText.text = $"{Status}";
    }

    public int GetStatValue(string stat)
    {
        switch (stat)
        {
            case "Intellect":
                return Intellect;
            case "Physical":
                return Physical;
            case "Happiness":
                return Mood;
            case "Hunger":
                return Hunger;
            default:
                Debug.LogError("Invalid stat name.");
                return 0;
        }
    }

    private IEnumerator DeteriorateStats()
    {
        while (true)
        {
            yield return new WaitForSeconds(180); // Wait for 3 minutes

            SetStat("Happiness", Mathf.Max(Mood - 2, 0)); // Decrease Happiness by 2 but not below 0
            SetStat("Hunger", Mathf.Max(Hunger - 2, 0)); // Decrease Hunger by 2 but not below 0

            UpdateStatus();
            UpdateUI();
        }
    }

    /*public void Stats()
    {
        switch(gameObject.name)
        {
            case "IntStats":
                Debug.Log($":{Intellect}");
                break;
            case "PStats":
                Debug.Log($":{Physical}");
                break;
            case "HungerStats":
                Debug.Log($":{Hunger}");
                break;
            case "MoodStats":
                Debug.Log($":{Mood}");
                break;
            default:
                Debug.LogError("Invalid game object name.");
                break;
        }
    }*/

}
