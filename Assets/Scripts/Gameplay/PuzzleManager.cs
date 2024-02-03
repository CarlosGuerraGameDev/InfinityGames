using System.Collections.Generic;
using LevelSystem;
using PlayerSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class PuzzleManager : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private List<Piece> puzzlePieces;
        [SerializeField] private bool isPuzzleFinished;
        [SerializeField] private int rewardPoints;
        [SerializeField] private int rewardExperience;
        [SerializeField] private string nextLevel;

        public UnityEvent whenPuzzeSolvedEvent;
        
        public delegate void OnPuzzleFinished();
        public static event OnPuzzleFinished OnPuzzleFinishedEvent;

        private void Awake()
        {
            Piece.OnCheckPuzzleConditionIsMet += CheckIfPuzzleIsSolved;
        }

        private void OnDestroy()
        {
            Piece.OnCheckPuzzleConditionIsMet -= CheckIfPuzzleIsSolved;
        }

        private void CheckIfPuzzleIsSolved()
        {
            foreach (var piece in puzzlePieces)
            {
                if (!piece.HasRightRotation)
                {
                    isPuzzleFinished = false;
                    break;
                }

                isPuzzleFinished = true;
            }

            if (isPuzzleFinished)
            {
                
                for (int i = 0; i < puzzlePieces.Count; ++i)
                {
                    puzzlePieces[i].CanRotate = false;
                    puzzlePieces[i].SetWinningColor();
                }
                
                levelManager.AddExperience(rewardPoints);
                playerManager.AddScore(rewardExperience);
                OnPuzzleFinishedEvent?.Invoke();
                whenPuzzeSolvedEvent.Invoke();
            }

        }
    }
}