using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Stage", menuName = "States/Create", order = 0)]
    public class StagesSO : ScriptableObject
    {
        public int id;
        public string name;
        public string levelName;
        public int rewardXp;
        public int rewardScore;
        public Material backgroundStage;
        public Material backgroundLockedStage;
    }
}