using System;
using System.IO;
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

    private async void Download() {
        if (downloaded)
            return;
        string danceData = await APIServices.GetDanceData(danceInfo.id);
        string savePath = Path.Combine(Application.dataPath, DOWNLOAD_PATH, danceInfo.id); 
        Directory.CreateDirectory(savePath);
        await File.WriteAllTextAsync(Path.Combine(savePath, danceInfo.name, ".dnc"), danceData);
        // TODO: download song

        downloaded = true;
    }

    public void Play() {
        // Ensure file is downloaded.
        Download();
        DontDestroyOnLoad(gameObject);
        // TODO: Load DancePlayer Scene
        SceneManager.LoadScene("Scenes/DancePlayer");
        Debug.Log(SceneManager.GetActiveScene().GetRootGameObjects());
        GameObject.Destroy(gameObject);
    }

}
