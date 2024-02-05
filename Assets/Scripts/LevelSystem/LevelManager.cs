using PlayerSystem;
using UnityEngine;

namespace LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private AnimationCurve difficultyCurve;

        [SerializeField] private int maxLevel = 100;
        [SerializeField] private int currentLevel = 1;

        [SerializeField] private int currentExperience;
        [SerializeField] private int previousLevelExperience;
        [SerializeField] private int nextLevelExperience;
        
        public int CurrentLevel
        {
            get => currentLevel;
            set => currentLevel = value;
        }

        
        public int CurrentExperience
        {
            get => currentExperience;
            set => currentExperience = value;
        }

        public int PreviousLevelExperience
        {
            get => previousLevelExperience;
            set => previousLevelExperience = value;
        }

        public int NextLevelExperience
        {
            get => nextLevelExperience;
            set => nextLevelExperience = value;
        }

        private void Start()
        {
            currentLevel = playerManager.CurrentSaveData.level;
            currentExperience = playerManager.CurrentSaveData.xp;
            SetNextLevelDifficulty();
        }

        private void SetNextLevelDifficulty()
        {
            previousLevelExperience = (int)difficultyCurve.Evaluate(currentLevel);
            nextLevelExperience = (int)difficultyCurve.Evaluate(currentLevel + 1);
        }

        public void AddExperience(int amount)
        {
            Debug.Log($"You received {amount} experience.");
            currentExperience += amount;
            
            playerManager.saveManager.EditDataProperty(gameData =>
            {
                gameData.xp += amount;
                return gameData;
            });

            CheckHasLevelUp();
        }

        public void CheckHasLevelUp()
        {
            if (currentExperience >= nextLevelExperience)
            {
                Debug.Log($"You are now on the level {currentLevel} for the next level you will need {nextLevelExperience} experience.");
                currentLevel++;
                currentExperience = 0;
                playerManager.saveManager.EditDataProperty(gameData =>
                {
                    gameData.level = currentLevel;
                    gameData.xp = 0;
                    return gameData;
                });
                SetNextLevelDifficulty();
            }
        }
    }
}