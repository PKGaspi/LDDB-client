using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenshotCapturer : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Capture() {
        String filename = $"Let's Dance! Dikir Barat - {DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}.png";
        ScreenCapture.CaptureScreenshot(filename, 2);
        Debug.Log($"Captured screenshot {filename}");

    }
}
