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

public class DancePlayer : MonoBehaviour, KinectGestures.GestureListenerInterface
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
    public CountdownUI countdownUI;
    public ParticleSystem[] endParticles;
    public AudioSource[] endSounds;

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
            End();
        }

        if (currentMoveIndex+1 < dance.moves.Length) {
            if (timestamp >= dance.moves[currentMoveIndex].endTime) {
                // Advance to the next move.
                currentMoveIndex++;
                scorable = true;
            }
        }
        
        danceUI.SetTime(timestamp);
        //danceUI.SetMoveIndex(currentMoveIndex);

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
    
        danceUI.SetTime(timestamp);
        danceUI.SetScore(score);
    }

    public void Play() {
        songSource.Play();
        playing = true;
        danceUI.gestureIconsBar.playing = true;
    }

    public void UnPause() {
        songSource.UnPause();
        playing = true;
        danceUI.gestureIconsBar.playing = true;
    }

    public void Pause() {
        songSource.Pause();
        playing = false;
        danceUI.gestureIconsBar.playing = false;
        // Reset countdown
        countdownUI.Reset();
    }

    public void Stop() {
        songSource.Stop();
        playing = false;
        danceUI.gestureIconsBar.playing = false;
    }

    public void End() {
        Debug.Log("Dance End");
        
        // Stop dance
        timestamp = dance.duration;
        Stop();
        // Celebrate~!
        foreach (ParticleSystem particles in endParticles)
        {
            particles.Play();
        }
        foreach (AudioSource source in endSounds)
        {
            source.Play();
        }
        // Transition to results screen.
        // Create results asset to carry to next screen.
        GameObject go = new GameObject();
        go.name = "DanceSummary";
        DanceSummary danceSummary = go.AddComponent(typeof(DanceSummary)) as DanceSummary;
        danceSummary.dance = dance;
        if (danceInfo != null) {
            danceSummary.songName = danceInfo.song.author.name;
            danceSummary.songAuthor = danceInfo.song.name;
        }
        danceSummary.score = score;
        danceSummary.Invoke("Show", endDelay);
    }

    public void UserDetected(uint userId, int userIndex)
	{
        countdownUI.StartCounter();
		// Register every gesture in this dance.
		KinectManager manager = KinectManager.Instance;

        foreach (MoveData move in dance.moves) {
		    manager.DetectGesture(userId, move.gesture);
        }

	}
	
	public void UserLost(uint userId, int userIndex)
	{
        Pause();
	}

	public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{

	}

	public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
        if (scorable && gesture == dance.moves[currentMoveIndex].gesture) {
            int noise = dance.moves[currentMoveIndex].points / 10;
            int points = dance.moves[currentMoveIndex].points;
            
            if (UnityEngine.Random.Range(0f, 1f) <= .25f) {
                // Sorry, bad move!
                points = (int) Math.Round(points * UnityEngine.Random.Range(.7f, .9f));
            }

            score += points;
            danceUI.SetScore(score);
            scorable = false;
            Debug.Log($"Rewarded {points} for move #{currentMoveIndex} - {gesture}");
        }
        return false;
	}

	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint)
	{
        return false;
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
