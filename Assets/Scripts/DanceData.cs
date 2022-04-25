using UnityEngine;

public class DanceData {
    public string filePath;
    public string danceName;
    public string author;
    public string songFilePath;
    public float duration;
    public MoveData[] moves;

    public void LogDanceInfo() {
        Debug.Log("Name - " + danceName);
        Debug.Log("Author - " + author);
        Debug.Log("File - " + songFilePath);
        Debug.Log("Duration - " + duration);
        Debug.Log("N Moves - " + moves.Length);
    }

    public void LogMoveInfo(int moveIndex) {
        Debug.Log("Move " + moveIndex + " name - " + moves[moveIndex].gestureName);
        Debug.Log("Move " + moveIndex + " start - " + moves[moveIndex].moveStart);
        Debug.Log("Move " + moveIndex + " end - " + moves[moveIndex].moveEnd);
    }
}