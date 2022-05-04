using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DancePlayer : MonoBehaviour
{
    private float timestamp = 0f;
    private int score = 0;
    private AudioSource songSource;
    private string danceFileFolder = "Dances";
    public DanceUI danceUI;
    public string danceFileName;
    public bool playing = true;
    public DanceData dance;
    public bool mute = false;

    // Start is called before the first frame update
    async void Start()
    {  
        if (danceFileName == null) {
            return;
        }

        songSource = gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
        dance = DanceParser.Parse(Path.Combine(Application.dataPath, danceFileFolder, danceFileName)); 
        dance.danceName = "Super chachi dance";
        dance.author = "El gran, único e inigualable Gaspi";
        
        // Debug
        dance.LogInfo();
        dance.LogMoveInfo(0);
        dance.LogMoveInfo(1);
        dance.LogMoveInfo(2);
        
        songSource.clip = await LoadClip(Path.Combine(Application.dataPath, danceFileFolder, dance.songFilePath));
        if (!mute)
            songSource.Play();
        foreach (MoveData move in dance.moves) {
            danceUI.LoadMove(move);
        }
        danceUI.gestureIconsBar.playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playing) {
            return;
        }

        timestamp += Time.deltaTime;
        Debug.Log("timestamp" +( timestamp - Mathf.Floor(timestamp)));
        danceUI.SetTime(timestamp);
        danceUI.SetScore(100);

    }

    // Source: https://answers.unity.com/questions/1518536/load-audioclip-from-folder-on-computer-into-game-i.html
    async Task<AudioClip> LoadClip(string path)
    {
        AudioClip clip = null;
        using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
        {
            uwr.SendWebRequest();
    
            // wrap tasks in try/catch, otherwise it'll fail silently
            try
            {
                while (!uwr.isDone) await Task.Delay(5);
    
                if ((uwr.result == UnityWebRequest.Result.ConnectionError) || 
                    (uwr.result == UnityWebRequest.Result.ProtocolError)) {
                    Debug.LogError($"{uwr.error}");
                }
                else
                {
                    clip = DownloadHandlerAudioClip.GetContent(uwr);
                }
            }
            catch (Exception err)
            {
                Debug.LogError($"{err.Message}, {err.StackTrace}");
            }
        }
    
        return clip;
    }

}
