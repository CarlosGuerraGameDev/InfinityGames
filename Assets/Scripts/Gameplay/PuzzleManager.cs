using System.Collections.Generic;
using LevelSystem;
using PlayerSystem;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class PuzzleManager : MonoBehaviour
    {
        [SerializeField] private StagesSO stagesSo;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private List<Piece> puzzlePieces;
        [SerializeField] private bool isPuzzleFinished;

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

        public void UpdateWinText(TMP_Text winText)
        {
            winText.text = "Congratulations you\nmade it\n\nYou got " + stagesSo.rewardScore + "+ points";
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
                
                levelManager.AddExperience(stagesSo.rewardXp);
                playerManager.AddScore(stagesSo.rewardScore);
                OnPuzzleFinishedEvent?.Invoke();
                whenPuzzeSolvedEvent.Invoke();
            }

        }
    }
}