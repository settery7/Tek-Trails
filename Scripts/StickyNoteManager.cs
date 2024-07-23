using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StickyNoteManager : MonoBehaviour
{
    public InputField noteInputField;
    public Button createNoteButton;
    public Transform notesContainer;
    public GameObject stickyNoteButtonPrefab;
    public Dropdown hoursDropdown;
    public Dropdown minutesDropdown;
    public Dropdown amPmDropdown;
    public TMP_Text noteDisplayText; // Reference to the Text GameObject

    private List<GameObject> stickyNotes = new List<GameObject>();

    void Start()
    {
        createNoteButton.onClick.AddListener(CreateStickyNote);
    }

    void CreateStickyNote()
    {
        if (stickyNotes.Count >= 8)
        {
            Debug.Log("Maximum number of sticky notes reached.");
            return;
        }

        string noteText = noteInputField.text;
        if (string.IsNullOrEmpty(noteText))
        {
            Debug.Log("Note text is empty.");
            return;
        }

        // Get the selected hour, minute, and AM/PM
        int selectedHour = int.Parse(hoursDropdown.options[hoursDropdown.value].text);
        int selectedMinute = int.Parse(minutesDropdown.options[minutesDropdown.value].text);
        string amPm = amPmDropdown.options[amPmDropdown.value].text;

        // Convert to 24-hour format
        if (amPm == "PM" && selectedHour != 12)
        {
            selectedHour += 12;
        }
        else if (amPm == "AM" && selectedHour == 12)
        {
            selectedHour = 0;
        }

        // Get the current date in Singapore time and combine it with the selected time
        DateTime currentDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Singapore Standard Time");
        DateTime reminderTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, selectedHour, selectedMinute, 0);

        // Check if the reminder time is valid (not in the past)
        if (reminderTime <= currentDateTime)
        {
            Debug.Log("Reminder time is in the past.");
            return;
        }

        // Create a new sticky note button
        GameObject newNoteButton = Instantiate(stickyNoteButtonPrefab, notesContainer);
        newNoteButton.GetComponentInChildren<Text>().text = noteText;
        newNoteButton.GetComponent<Button>().onClick.AddListener(() => ShowStickyNoteUI(noteText));

        stickyNotes.Add(newNoteButton);
        noteInputField.text = string.Empty;

        // Update the noteDisplayText with the new note text
        noteDisplayText.text = noteText;

        // Schedule the reminder
        ScheduleReminder(noteText, reminderTime);
    }

    void ShowStickyNoteUI(string noteText)
    {
        // Implement the UI display logic here.
        Debug.Log("Sticky Note: " + noteText);
    }

    void ScheduleReminder(string noteText, DateTime reminderTime)
    {
        // Implement your reminder scheduling logic here.
        Debug.Log("Reminder set for: " + reminderTime.ToString("yyyy-MM-dd HH:mm:ss") + " (Singapore Standard Time)");
    }
}


