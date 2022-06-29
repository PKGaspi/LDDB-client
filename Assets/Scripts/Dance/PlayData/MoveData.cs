using UnityEngine;

namespace PlayData {
    public class MoveData {
        public int index;
        public float startTime;
        public float endTime;
        public string gestureName;
        public KinectGestures.Gestures gesture;
        public int points;
        
        public void LogInfo() {
            Debug.Log("Move " + index + " name - " + gestureName);
            Debug.Log("Move " + index + " gesture - " + gesture);
            Debug.Log("Move " + index + " start - " + startTime);
            Debug.Log("Move " + index + " end - " + endTime);
            Debug.Log("Move " + index + " points - " + points);
        }
    }
}