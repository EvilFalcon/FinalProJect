using System;
using UnityEngine.Serialization;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerPositionOnLevel
    {
        [FormerlySerializedAs("Lavel")] public string _level;
        public Vector3Data PlayerPosition;

        public PlayerPositionOnLevel(string level, Vector3Data vector3Data)
        {
            _level = level;
            PlayerPosition = vector3Data;
        }

        public PlayerPositionOnLevel(string level)
        {
            _level = level;
        }
    }
}