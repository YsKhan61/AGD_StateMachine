using ClassroomIGI.Utilities;
using UnityEngine;


namespace ClassroomIGI.Player
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject")]
    public class PlayerScriptableObject : ScriptableObject
    {
        public PlayerView PlayerPrefab;
        public Vector3 SpawnPosition;
        public Vector3 SpawnRotation;
        public float MovementSpeed;
        public float RotationSpeed;
        public int MaximumHealth;
        public int AttackValue;
        public IntDataSO CurrentScore;
    }
}