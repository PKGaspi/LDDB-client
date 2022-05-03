using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public static class DanceParser {

    public static readonly int[] VALID_FILE_VERSIONS = {1};

    public static DanceData Parse(String danceFilePath) {
        // Create DanceData
        DanceData dance = new DanceData();
        StreamReader reader = File.OpenText(danceFilePath);
        dance.filePath = danceFilePath;
        
        // Parse file version, song name and length
        int version = int.Parse(reader.ReadLine());
        if (!VALID_FILE_VERSIONS.Contains(version)) {
            // TODO: Throw invalid version exception
            return null;
        }
        dance.version = version;
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
                // TODO: Throw bad format exception
                return null;
            }
            // Move timestamps
            string[] timestamps = reader.ReadLine().Split("-->");
            float startTime = float.Parse(timestamps[0]);
            float endTime = float.Parse(timestamps[1]);
            // Move name
            string gestureName = reader.ReadLine();
            // Move points
            int movePoints = int.Parse(reader.ReadLine());
            // Empty line
            reader.ReadLine();

            MoveData move = new MoveData();
            move.index = moveIndex;
            move.startTime = startTime;
            move.endTime = endTime;
            move.gestureName = gestureName;
            move.points = movePoints;
            moves.Add(move);
            moveIndex++;
        }
        dance.moves = moves.ToArray();
        return dance;
    }
}