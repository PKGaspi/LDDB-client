using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceLoader : MonoBehaviour
{
    private string danceFileFolder = "/Dances/";
    public string danceFileName;
    public DanceData dance;
    // Start is called before the first frame update
    void Start()
    {  
        dance = DanceParser.Parse(Application.dataPath + danceFileFolder + danceFileName); 
        dance.danceName = "Super chachi dance";
        dance.author = "El gran, único e inigualable Gaspi";
        dance.LogDanceInfo();
        dance.LogMoveInfo(0);
        dance.LogMoveInfo(1);
        dance.LogMoveInfo(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
