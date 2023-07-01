using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerPositionOnLevel
    {
        public string Lavel;
        public Vector3Data PlayerPosition;

        public PlayerPositionOnLevel(string lavel, Vector3Data vector3Data)
        {
            Lavel = lavel;
            PlayerPosition = vector3Data;
        }

        public PlayerPositionOnLevel(string lavel)
        {
            Lavel = lavel;
        }
    }
}