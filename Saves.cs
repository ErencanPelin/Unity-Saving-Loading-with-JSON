using UnityEngine;

namespace SaveLoad
{
    public static class Saves
    {
        [System.Serializable]
        public record PlayerSaveData : SaveProfileData
        {
            public Vector3 position;
            public int currentLevel;
            public float health;
            public float xp;
        }
    }
}