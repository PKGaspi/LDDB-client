using UnityEngine;

public class DanceData {
    public int version;
    public string filePath;
    public string danceName;
    public string creator;
    public string songFilePath;
    public float duration;
    public MoveData[] moves;

    public void LogInfo() {
        Debug.Log("File version - " + version);
        Debug.Log("Name - " + danceName);
        Debug.Log("Creator - " + creator);
        Debug.Log("File - " + songFilePath);
        Debug.Log("Duration - " + duration);
        Debug.Log("N Moves - " + moves.Length);
    }

    public void LogMoveInfo(int moveIndex) {
        if (moveIndex < moves.Length)
            moves[moveIndex].LogInfo();
        else
            Debug.LogWarning($"No move #{moveIndex} for this dance.");
    }
}