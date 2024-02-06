using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Stages
{
    [CreateAssetMenu(fileName = "Config", menuName = "Stage/Create Configuration", order = 0)]
    public class StageConfiguration : ScriptableObject
    {
        public List<StagesSO> StagesSos;
    }
}