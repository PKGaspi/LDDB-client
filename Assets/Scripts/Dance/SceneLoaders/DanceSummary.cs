using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayData;

public class DanceSummary : MonoBehaviour
{
    public DanceData dance;
    public string songName;
    public string songAuthor;
    public int score;

    public void Show() {
        DontDestroyOnLoad(gameObject);
        GameObject.Destroy(GameObject.Find("KinectManager"));
        SceneManager.LoadScene("Scenes/DanceResults");
    }
}
