using UnityEngine;

public class DanceData {
    public int version;
    public string filePath;
    public string danceName;
    public string author;
    public string songFilePath;
    public float duration;
    public MoveData[] moves;

    public void LogInfo() {
        Debug.Log("File version - " + version);
        Debug.Log("Name - " + danceName);
        Debug.Log("Author - " + author);
        Debug.Log("File - " + songFilePath);
        Debug.Log("Duration - " + duration);
        Debug.Log("N Moves - " + moves.Length);
    }

    public void LogMoveInfo(int moveIndex) {
        moves[moveIndex].LogInfo();
    }
}