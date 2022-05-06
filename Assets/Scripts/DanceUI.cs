using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanceUI : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;
    public Text moveText;
    public GestureIconsBar gestureIconsBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(int value) {
        scoreText.text = $"Score: {value}";
    }

    public void SetTime(float value) {
        int mins = (int) Math.Floor(value / 60);
        int secs = (int) Math.Floor(value);
        int milisecs = (int) Math.Round((value - secs)*1000);
        timeText.text = $"Time: {mins:00}:{secs:00},{milisecs:000}";
    }

    public void SetMoveIndex(int value) {
        moveText.text = $"Move #: {value}";
    }

    public void LoadMove(MoveData move) {
        gestureIconsBar.LoadMove(move);
    }
}
