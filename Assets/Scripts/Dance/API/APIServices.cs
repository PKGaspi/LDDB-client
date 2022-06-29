using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using APITypes;

public static class APIServices
{
    private const String BASE_URL = "http://localhost:8000";
    
    public static async Task<StatusInfo> GetStatus() {
        String url = BASE_URL + "/status";
        try {
            using var www = UnityWebRequest.Get(url);
            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.LogError($"Failed: {www.error}");
                StatusInfo status = new StatusInfo();
                status.code = www.responseCode;
                status.message = www.error;
                return status;
            }
            
            Debug.Log("GET " + url + " Result: " + www.downloadHandler.text);

            return JsonUtility.FromJson<StatusInfo>(www.downloadHandler.text);
        }
        catch (Exception e) {
            Debug.LogError($"{nameof(GetStatus)} failed: {e.Message}");
        }
        return null;
    }
    // Source: https://github.com/crevelop/unitywebrequest-tutorial
    private static async Task<T> GetInfo<T>(String uri) {
        String url = BASE_URL + uri;
        try {
            using var www = UnityWebRequest.Get(url);
            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError($"Failed: {www.error}");
            
            Debug.Log("GET " + url + " Result: " + www.downloadHandler.text);

            return JsonUtility.FromJson<T>(www.downloadHandler.text);
        }
        catch (Exception e) {
            Debug.LogError($"{nameof(GetInfo)} failed: {e.Message}");
        }
        return default;
    }
        
    private static async Task<Byte[]> GetData(String uri) {
        String url = BASE_URL + uri;
        try {
            using var www = UnityWebRequest.Get(url);
            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError($"Failed: {www.error}");
            
            Debug.Log("GET " + url + " Result: " + www.downloadHandler.data);

            return www.downloadHandler.data;
        }
        catch (Exception e) {
            Debug.LogError($"{nameof(GetData)} failed: {e.Message}");
        }
        return default;
    }

    private async static Task<InfoList<T>> GetInfoList<T>(String uri) {
        String url = BASE_URL + uri;
        try {
            using var www = UnityWebRequest.Get(url);
            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError($"Failed: {www.error}");
            
            Debug.Log("GET " + url + " Result: " + www.downloadHandler.text);

            return JsonUtility.FromJson<InfoList<T>>(www.downloadHandler.text);
        }
        catch (Exception e) {
            Debug.LogError($"{nameof(GetInfoList)} failed: {e.Message}");
        }
        return default;
    }

    public static Task<SongInfo> GetSongInfo(String id) {
        return GetInfo<SongInfo>($"/song/{id}/info");
    }
    
    public static Task<DanceInfo> GetDanceInfo(String id) {
        return GetInfo<DanceInfo>($"/dance/{id}/info");
    }

    public static async Task<String> GetDanceData(String id) {
        var result = await GetData($"/dance/{id}/data");
        return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(System.Text.Encoding.UTF8.GetString(result)));
    }

    public static async Task<byte[]> GetSongData(String id) {
        var result = await GetData($"/song/{id}/data");
        return result;
    }

    public static Task<InfoList<SongInfo>> GetSongInfoList() {
        return GetInfoList<SongInfo>("/song_list");
    }

    public static Task<InfoList<DanceInfo>> GetDanceInfoList() {
        return GetInfoList<DanceInfo>("/dance_list");
    }
}

namespace APITypes {
    [Serializable]
    public class StatusInfo {
        public long code;
        public String message;
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
        public String username;
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
