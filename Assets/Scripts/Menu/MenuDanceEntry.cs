using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using APITypes;

public class MenuDanceEntry : MenuEntry
{

    public Text songText;
    public Text userText;
    private DanceInfo dance;
    
    // Start is called before the first frame update
    void Start()
    {
        if (dance != null)
            SetDance(dance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    new void Activate() {
        // Display downloading message
        DanceLister danceLister = GameObject.Find("DanceLister").GetComponent<DanceLister>();
        danceLister.ShowMessage($"Downloading {dance.name}...");
        // Download
        GameObject go = new GameObject();
        go.name = "DanceInfo";
        DanceDownloader danceDownloader = go.AddComponent(typeof(DanceDownloader)) as DanceDownloader;
        danceDownloader.danceInfo = dance;
        danceDownloader.Play();
    }

    public void SetDance(DanceInfo dance) {
        this.dance = dance;
        mainText.text = dance.name;
        songText.text = $"{dance.song.name} - {dance.song.author.name}";
        userText.text = dance.creator.username;
    }
}
