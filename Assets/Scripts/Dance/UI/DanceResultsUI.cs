using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayData;

public class DanceResultsUI : MonoBehaviour
{
    public Text danceText;
    public Text songText;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("DanceSummary");
        if (go != null) {
            // Load data from the go
            DanceSummary danceSummary = go.GetComponent<DanceSummary>();
            DanceData dance = danceSummary.dance;
            danceText.text = dance.danceName;
            songText.text = $"{danceSummary.songName} - {danceSummary.songAuthor}";
            scoreText.text = $"{danceSummary.score}/{dance.MaxScore()}";
            GameObject.Destroy(go);
        }
    }

}
