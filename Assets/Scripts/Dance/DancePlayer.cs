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
    private DanceData.DanceData dance;
    private int currentMoveIndex = 0;
    private bool scorable = true;
    
    public DanceUI danceUI;
    public string danceFileName;
    public bool autoplay = false;
    private bool playing = false;
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
        dance.creator = "El gran, único e inigualable Gaspi";
        
        // Debug
        dance.LogInfo();
        dance.LogMoveInfo(0);
        dance.LogMoveInfo(1);
        dance.LogMoveInfo(2);
        
        songSource.clip = await LoadClip(Path.Combine(Application.dataPath, danceFileFolder, dance.songFilePath));

        foreach (MoveData move in dance.moves) {
            danceUI.LoadMove(move);
        }

        songSource.mute = mute;

        if (autoplay)
            Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playing) {
            return;
        }

        timestamp += Time.deltaTime;

        if (timestamp >= dance.duration) {
            Stop();
            timestamp = dance.duration;
            // TODO: Change to results scene
        }

        if (currentMoveIndex < dance.moves.Length) {
            if (timestamp >= dance.moves[currentMoveIndex].endTime) {
                // Advance to the next move.
                currentMoveIndex++;
                scorable = true;
            }
        }
        
        danceUI.SetTime(timestamp);
        danceUI.SetScore(score);
        danceUI.SetMoveIndex(currentMoveIndex);

    }

    public void Play() {
        playing = true;
        songSource.Play();
        danceUI.gestureIconsBar.playing = true;

    }

    public void Stop() {
        playing = false;
        songSource.Stop();
        danceUI.gestureIconsBar.playing = false;
    }

    public void OnGestureComplete(KinectGestures.Gestures gesture) {
        if (!scorable)
            return;
        score += dance.moves[currentMoveIndex].points;
        scorable = false;
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
