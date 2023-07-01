using System;

namespace CodeBase.Data
{
    [Serializable]
    public class WorldData
    {
        public PlayerPositionOnLevel PlayerPositionOnLevel;

        public WorldData(string initialLevel)
        {
            PlayerPositionOnLevel = new PlayerPositionOnLevel(initialLevel);
        }
    }
}

