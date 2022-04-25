using System;
using System.IO;
using System.Collections.Generic;

public static class DanceParser {

    public static DanceData Parse(String danceFilePath) {
        // Create DanceData
        DanceData dance = new DanceData();
        StreamReader reader = File.OpenText(danceFilePath);
        dance.filePath = danceFilePath;
        
        // Parse song name and length
        dance.songFilePath = reader.ReadLine();
        dance.duration = float.Parse(reader.ReadLine());
        reader.ReadLine(); // Empty line

        // Parse list of moves
        int moveIndex = 0;
        string line;
        List<MoveData> moves = new List<MoveData>();
        while ((line = reader.ReadLine()) != null) {
            //print("Parsing move " + moveIndex);
            if (moveIndex != int.Parse(line)) {
                // Bad format
                return null;
            }
            // Move timestamps
            string[] timestamps = reader.ReadLine().Split("-->");
            float moveStart = float.Parse(timestamps[0]);
            float moveEnd = float.Parse(timestamps[1]);
            // Move name
            string gestureName = reader.ReadLine();
            // Empty line
            reader.ReadLine();
            MoveData move = new MoveData();
            move.moveStart = moveStart;
            move.moveEnd = moveEnd;
            move.gestureName = gestureName;
            moves.Add(move);
            moveIndex++;
        }
        dance.moves = moves.ToArray();
        return dance;
    }
}