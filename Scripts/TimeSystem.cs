using UnityEngine;
using UnityEngine.UI;  // Required if you're displaying time on UI
using TMPro;
using System;

public class TimeSystem : MonoBehaviour
{
    public TMP_Text timeDisplay;  // TextMeshPro component to display time

    // Variables for time
    public Image currImage;
    public Sprite Morning;
    public Sprite Night;
    public Sprite Afternoon;

    private DateTime currentTime;
    private TimeSpan timeOffset;

    void Start()
    {
        // Get current time in the Philippines time zone (UTC+8)
        TimeZoneInfo philippinesTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
        currentTime = TimeZoneInfo.ConvertTime(DateTime.Now, philippinesTimeZone);

        // Calculate the offset from UTC
        timeOffset = philippinesTimeZone.BaseUtcOffset;
    }

    void Update()
    {
        // Update the current time by adding the elapsed time since the last frame
        currentTime = currentTime.AddSeconds(Time.deltaTime);

        // Format the time as HH:MM:SS
        string formattedTime = currentTime.ToString("HH:mm tt");

        // Display the time (if timeDisplay is assigned)
        if (timeDisplay != null)
        {
            timeDisplay.text = formattedTime;
        }

        if (currImage != null)
        {
            if (currentTime.Hour >= 6 && currentTime.Hour < 12)
            {
                // It's morning
                currImage.sprite = Morning;
            }
            else if (currentTime.Hour >= 12 && currentTime.Hour < 18)
            {
                currImage.sprite = Afternoon;
            }
            else
            {
                // It's night
                currImage.sprite = Night;
            }
        }
    }

    // Method to reset the time
    public void ResetTime()
    {
        // Reset to current time in the Philippines time zone
        TimeZoneInfo philippinesTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
        currentTime = TimeZoneInfo.ConvertTime(DateTime.Now, philippinesTimeZone);
    }

    // Method to get the current time as a string
    public string GetCurrentTime()
    {
        return currentTime.ToString("HH:mm tt");
    }
}
