using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using UnityEngine;

public static class APIServices
{
    private const String BASE_URL = "http://localhost:8000";
    
    public static DanceInfo GetDanceInfo(String id) {
        String url = BASE_URL + "/dance/" + id + "/info";
        Debug.Log("GET " + url);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        String jsonResponse = reader.ReadToEnd();
        Debug.Log("GET " + url + " response: " + jsonResponse);
        DanceInfo dance = JsonUtility.FromJson<DanceInfo>(jsonResponse);
        return dance;
    }

    public static SongInfo GetSongInfo(String id) {
        String url = BASE_URL + "/song/" + id + "/info";
        Debug.Log("GET " + url);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        String jsonResponse = reader.ReadToEnd();
        Debug.Log("GET " + url + " response: " + jsonResponse);
        SongInfo song = JsonUtility.FromJson<SongInfo>(jsonResponse);
        return song;
    }

    public static InfoList<SongInfo> GetSongInfoList() {
        String url = BASE_URL + "/song_list";
        Debug.Log("GET " + url);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        String jsonResponse = reader.ReadToEnd();
        Debug.Log("GET " + url + " response: " + jsonResponse);
        InfoList<SongInfo> list = JsonUtility.FromJson<InfoList<SongInfo>>(jsonResponse);
        return list;
    }

    public static InfoList<DanceInfo> GetDanceInfoList() {
        String url = BASE_URL + "/dance_list";
        Debug.Log("GET " + url);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        String jsonResponse = reader.ReadToEnd();
        Debug.Log("GET " + url + " response: " + jsonResponse);
        InfoList<DanceInfo> list = JsonUtility.FromJson<InfoList<DanceInfo>>(jsonResponse);
        return list;
    }
    

    [Serializable]
    public class DanceInfo {
        public int code;
        public String id;
        public DateTime created_at;
        public DateTime modified_at;
        public String name;
        public UserInfo creator;
        public SongInfo song;
    }

    [Serializable]
    public class UserInfo {
        public int code;
        public String id;
        public DateTime created_at;
        public DateTime modified_at;
        public String name;
    }

    [Serializable]
    public class SongInfo {
        public int code;
        public String id;
        public DateTime created_at;
        public DateTime modified_at;
        public String name;
        public SongAuthorInfo author;
    }

    [Serializable]
    public class SongAuthorInfo {
        public int code;
        public String id;
        public DateTime created_at;
        public DateTime modified_at;
        public String name;
    }

    [Serializable]
    public class InfoList<T> {
        public int code;
        public List<T> list;
    }
}
