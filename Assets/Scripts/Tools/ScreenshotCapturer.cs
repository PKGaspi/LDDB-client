using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ScreenshotCapturer : MonoBehaviour
{
    const string SCREENSHOTS_PATH = "Screenshots";
    const int SCALE = 2;
    public void Capture(InputAction.CallbackContext context) {
        if (!context.performed) {
            return;
        }

        string path = Path.Combine(Application.persistentDataPath, SCREENSHOTS_PATH);
        Directory.CreateDirectory(path);
        string filename = $"{Application.productName} - {DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}";
        string fullpath = Path.Combine(path, filename);

        for (int i = 2; File.Exists($"{fullpath}.png"); i++) {
            fullpath = Path.Combine(path, $"{filename} ({i})");
        }
        fullpath += ".png";

        ScreenCapture.CaptureScreenshot(fullpath, SCALE);
        Debug.Log($"Captured screenshot at {fullpath}");
    }
}
