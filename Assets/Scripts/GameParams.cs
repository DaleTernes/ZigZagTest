using UnityEngine;


namespace ZigZag
{
    [CreateAssetMenu]
    public class GameParams : ScriptableObject
    {
        public float Speed;
        public DifficultyLevel Difficulty;
        public CrystalGenerationMode CrystalGenerationMode;
    }


    public enum DifficultyLevel
    {
        Easy = 1,
        Medium = 2,
        Hard = 3
    }


    public enum CrystalGenerationMode
    {
        Ordered,
        Random
    }
}