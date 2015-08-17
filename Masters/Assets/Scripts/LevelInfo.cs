using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Assets.Scripts.model
{
    [Serializable]
    public class LevelInfo
    {
        public static readonly LevelInfo Janacek = new LevelInfo(LevelName.Janacek, 60, 0x313eaa, "CgkIq-O5sYgTEAIQAQ");
        public static readonly LevelInfo Smetana = new LevelInfo(LevelName.Smetana, 60, 0xaa4531, "CgkIq-O5sYgTEAIQAg");
        public static readonly LevelInfo Rock = new LevelInfo(LevelName.Rock, 60, 0x51dbbe, "CgkIq-O5sYgTEAIQAw");

        public static readonly ReadOnlyCollection<LevelInfo> Levels =
            new ReadOnlyCollection<LevelInfo>(new List<LevelInfo>
            {
                Janacek,
                Smetana,
                Rock
            });

        public LevelInfo(LevelName levelName, int time, int color, string gpsLeaderboardId)
        {
            LevelName = levelName;
            Time = time;
            Color = color;
            GpsLeaderboardId = gpsLeaderboardId;
        }

        public LevelName LevelName { get; private set; }
        public int Time { get; private set; }
        public int Color { get; private set; }
        public string GpsLeaderboardId { get; private set; }

        public LevelInfo nextLevel()
        {
            var index = Levels.IndexOf(this) + 1;
            try
            {
                return Levels[index];
            }
            catch (ArgumentOutOfRangeException e)
            {
                return null;
            }
        }

        public LevelInfo previousLevel()
        {
            var index = Levels.IndexOf(this) - 1;
            try
            {
                return Levels[index];
            }
            catch (ArgumentOutOfRangeException e)
            {
                return null;
            }
        }

        public static LevelInfo get(LevelName levelName)
        {
            return Levels.First(l => l.LevelName == levelName);
        }
    }

    public enum LevelName
    {
        Janacek,
        Smetana,
        Rock
    }
}