using UnityEngine;

namespace PlayData {
    public class DanceData {
        public int version;
        public string filePath;
        public string danceName;
        public string author;
        public string creator;
        public string songFilePath;
        public float songOffset;
        public float duration;
        public MoveData[] moves;

        public int MaxScore() {
            int maxScore = 0;
            foreach (MoveData move in moves) {
                maxScore += move.points;
            }
            return maxScore;
        }
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
}