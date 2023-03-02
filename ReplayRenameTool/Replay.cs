using System.IO;

namespace ReplayRenameTool
{
    internal class Replay
    {
        public static void Rename(string replayFile, string? team, string? player, string? bingo, string outputDir)
        {
            string gameId = GetGameId(replayFile);

            string newReplayFile = $"{outputDir}//{gameId}_ud{team}{player}{bingo}.rpy";
            int i = 0;
            while (File.Exists(newReplayFile))
            {
                i++;
                newReplayFile = $"{outputDir}//{gameId}_ud{team}{player}{bingo}-{i}.rpy";
            }

            File.Copy(replayFile, newReplayFile);
        }

        public static string GetGameId(string replayFile)
        {
            string replayName = Path.GetFileNameWithoutExtension(replayFile);
            return replayName.Split('_')[0];
        }
    }
}
