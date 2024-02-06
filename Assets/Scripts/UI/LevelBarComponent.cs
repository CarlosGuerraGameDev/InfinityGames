using DG.Tweening;
using LevelSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class LevelBar : MonoBehaviour
    {
        
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private TMP_Text winningXPText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private Ease easeBar = Ease.Flash;
        [SerializeField] private Slider slider;
        
        public void PlayLevelBar()
        {
            levelManager.SetNextLevelDifficulty();
            levelText.text = "YOUR LEVEL\n" + levelManager.CurrentLevel;
            winningXPText.text = levelManager.CurrentExperience + ":XP / " + levelManager.NextLevelExperience + ":XP";
            StartSliderAnimationProgress();
        }

        private void StartSliderAnimationProgress()
        {
            float finalValue = (float)levelManager.CurrentExperience / levelManager.NextLevelExperience * 100;
            slider.DOValue(finalValue, 2).SetEase(easeBar).OnComplete(
                () => { Debug.Log("Level Slider animation finished!"); });
        }
        
    }
}