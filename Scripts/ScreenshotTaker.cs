using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotTaker : MonoBehaviour
{
    public Button screenshotButton;
    private string screenshotFolder = @"D:\MSAT Project\TestPhoto";

    private void Start()
    {
        if (screenshotButton != null)
        {
            screenshotButton.onClick.AddListener(TakeScreenshot);
        }

        // Ensure the screenshot folder exists
        if (!Directory.Exists(screenshotFolder))
        {
            Directory.CreateDirectory(screenshotFolder);
        }
    }

    public void TakeScreenshot()
    {
        string screenshotName = "screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string screenshotPath = Path.Combine(screenshotFolder, screenshotName);
        ScreenCapture.CaptureScreenshot(screenshotPath);

        Debug.Log("Screenshot taken: " + screenshotPath);
    }
}
