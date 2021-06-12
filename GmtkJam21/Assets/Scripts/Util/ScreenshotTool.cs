using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenshotTool : MonoBehaviour
{
    public GameObject popup;
    public TextMeshProUGUI popupText;
    
    void Update()
    {
        if (Keyboard.current.printScreenKey.wasPressedThisFrame)
        {
            StopAllCoroutines();

            string folderPath = $"{Directory.GetCurrentDirectory()}\\Screenshots\\";
            string fileName = "screenshot_seed-" + Game.inst.config.seed + "_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".png";
            string fullPath = folderPath + fileName;
            
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            
            popupText.text = $"Screenshot saved to {fullPath}. (path copied to clipboard)";
            ScreenCapture.CaptureScreenshot(Path.Combine(folderPath, fileName));
            GUIUtility.systemCopyBuffer = folderPath;

            StartCoroutine(HidePopupAfterSeconds(4f));
        }
    }

    private IEnumerator HidePopupAfterSeconds(float waitTime)
    {
        yield return null;
        popup.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        popup.SetActive(false);
    }
    
    private static double GetUnixEpoch(DateTime dateTime)
    {
        var unixTime = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return unixTime.TotalSeconds;
    }
}
