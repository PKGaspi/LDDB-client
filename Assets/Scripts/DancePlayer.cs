using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DancePlayer : MonoBehaviour
{
    private AudioSource songSource;
    private string danceFileFolder = "Dances";
    public string danceFileName;
    public DanceData dance;

    // Start is called before the first frame update
    async void Start()
    {  
        songSource = gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
        dance = DanceParser.Parse(Path.Combine(Application.dataPath, danceFileFolder, danceFileName)); 
        dance.danceName = "Super chachi dance";
        dance.author = "El gran, único e inigualable Gaspi";
        dance.LogDanceInfo();
        dance.LogMoveInfo(0);
        dance.LogMoveInfo(1);
        dance.LogMoveInfo(2);
        songSource.clip = await LoadClip(Path.Combine(Application.dataPath, danceFileFolder, dance.songFilePath));
        Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Play() {
        songSource.Play();
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
    
                if (uwr.isNetworkError || uwr.isHttpError) Debug.Log($"{uwr.error}");
                else
                {
                    clip = DownloadHandlerAudioClip.GetContent(uwr);
                }
            }
            catch (Exception err)
            {
                Debug.Log($"{err.Message}, {err.StackTrace}");
            }
        }
    
        return clip;
 }
}
