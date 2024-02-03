using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PieceDirections", menuName = "Pieces/Create new Directions", order = 0)]
    public class PieceDirections : ScriptableObject
    {
        public List<PieceDirection> Directions;
    }
} 