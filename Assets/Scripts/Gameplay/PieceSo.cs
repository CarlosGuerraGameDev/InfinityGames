using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Piece", menuName = "Pieces/Add new piece", order = 0)]
    public class PieceSo : ScriptableObject
    {
        [SerializeField] public PieceDirections pieceDirection;
        [SerializeField] public RotationType rotationType;
    }
}