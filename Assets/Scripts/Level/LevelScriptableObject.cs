using ClassroomIGI.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace ClassroomIGI.Level
{
    [CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "ScriptableObjects/LevelScriptableObject")]
    public class LevelScriptableObject : ScriptableObject
    {
        public int ID;
        public GameObject LevelPrefab;
        public List<EnemyScriptableObject> EnemyScriptableObjects;
    }
}