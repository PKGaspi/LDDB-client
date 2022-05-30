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
        SceneManager.LoadScene("Scenes/DancePlayer");
    }

    public void SetDance(DanceInfo dance) {
        this.dance = dance;
        mainText.text = dance.name;
        songText.text = $"{dance.song.name} - {dance.song.author.name}";
        userText.text = dance.creator.username;
    }
}
