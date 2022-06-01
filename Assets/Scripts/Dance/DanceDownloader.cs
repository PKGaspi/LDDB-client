using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using APITypes;

public class DanceDownloader : MonoBehaviour
{
    public DanceInfo danceInfo;
    private const string DOWNLOAD_PATH = "Dances";
    private bool downloaded = false;

    private async Task Download() {
        if (downloaded)
            return;

        string savePath = Path.Combine(Application.persistentDataPath, DOWNLOAD_PATH, danceInfo.id);
        string danceFilePath = Path.Combine(savePath, danceInfo.name + ".dnc"); 
        string songFilePath = Path.Combine(savePath, $"{danceInfo.song.name} - {danceInfo.song.author.name}.wav"); 
        Directory.CreateDirectory(savePath);

        // Get Dance file
        string danceData = await APIServices.GetDanceData(danceInfo.id);
        if (File.Exists(danceFilePath)) 
            File.Delete(danceFilePath);
        StreamWriter danceFile = File.CreateText(danceFilePath);
        await danceFile.WriteAsync(danceData);
        danceFile.Close();
        // Get Song file
        byte[] songData = await APIServices.GetSongData(danceInfo.song.id);
        if (File.Exists(songFilePath)) 
            File.Delete(songFilePath);
        FileStream songFile = File.Create(songFilePath);
        await songFile.WriteAsync(songData);
        songFile.Close();

        downloaded = true;
    }

    public async void Play() {
        // Ensure file is downloaded.
        await Download();
        DontDestroyOnLoad(gameObject);
        var operation = SceneManager.LoadSceneAsync("Scenes/DancePlayer");

    }

}
