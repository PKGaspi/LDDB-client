using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using APITypes;
using PlayData;

public class DancePlayer : MonoBehaviour
{
    private float timestamp = 0f;
    private int score = 0;
    private AudioSource songSource;
    private const string DANCES_PATH = "Dances";
    private DanceData dance;
    private DanceInfo danceInfo;
    private int currentMoveIndex = 0;
    private bool scorable = true;
    
    public DanceUI danceUI;
    public string danceFileName;
    public string songFileName;
    private bool playing = false;
    public float endDelay = 2f;

    // Start is called before the first frame update
    void Start() {
        songSource = gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
        GameObject danceDataGO = GameObject.Find("DanceInfo");
        if (danceDataGO != null) {
            // Load data from the go
            danceInfo = danceDataGO.GetComponent<DanceDownloader>().danceInfo;
            danceFileName = Path.Combine(danceInfo.id, danceInfo.name + ".dnc");
            songFileName = Path.Combine(danceInfo.id, $"{danceInfo.song.name} - {danceInfo.song.author.name}.wav");
            GameObject.Destroy(danceDataGO);
        }

        Setup(danceFileName, songFileName);
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
            // TODO: Play End animation
            // TODO: Change to results scene
            Invoke("End", endDelay);
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

    public async void Setup(String danceFileName, String songFileName) {
        if (danceFileName == null) {
            return;
        }

        dance = DanceParser.Parse(Path.Combine(Application.persistentDataPath, DANCES_PATH, danceFileName)); 
        if (danceInfo != null) {
            dance.danceName = danceInfo.name;
            dance.creator = danceInfo.creator.username;
        }

        
        songSource.clip = await LoadClip(Path.Combine(Application.persistentDataPath, DANCES_PATH, songFileName));

        foreach (MoveData move in dance.moves) {
            danceUI.LoadMove(move);
        }
        dance.LogMoveInfo(2);
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

    public void End() {
        // Create results asset to carry to next screen.
        Debug.Log("Dance End");
        GameObject go = new GameObject();
        go.name = "DanceSummary";
        DanceSummary danceSummary = go.AddComponent(typeof(DanceSummary)) as DanceSummary;
        danceSummary.dance = dance;
        danceSummary.songName = danceInfo.song.author.name;
        danceSummary.songAuthor = danceInfo.song.name;
        danceSummary.score = score;
        danceSummary.Show();
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
