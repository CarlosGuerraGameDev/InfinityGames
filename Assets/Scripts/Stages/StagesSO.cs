using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Stage", menuName = "States/Create", order = 0)]
    public class StagesSO : ScriptableObject
    {
        public int id;
        public string name;
        public Material backgroundStage;
    }
}