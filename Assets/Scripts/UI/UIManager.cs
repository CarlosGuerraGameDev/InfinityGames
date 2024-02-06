using DG.Tweening;
using Gameplay;
using LevelSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private RectTransform winningUI;
        [SerializeField] private RectTransform winningUIMessageText;
        [SerializeField] private RectTransform puzzle;

        [SerializeField] private TMP_Text winningXPText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private Ease easeBar = Ease.Flash;
        [SerializeField] private Ease easeOut = Ease.Flash;
        [SerializeField] private Slider slider;
        [SerializeField] private float winTextAnimDuration = 1;
        [SerializeField] private float winLevelBarAnimDuration = 1;
        [SerializeField] private float deviceTabletSize = 0.5f;
        
        public UnityEvent whenPuzzleIsFinishedEvent;
        private Sequence _sequenceLevelBar;

        private void Awake()
        {
            PuzzleManager.OnPuzzleFinishedEvent += PuzzleFinishedUIAnimation;
        }

        private void OnDestroy()
        {
            PuzzleManager.OnPuzzleFinishedEvent -= PuzzleFinishedUIAnimation;
        }

        private void PuzzleFinishedUIAnimation()
        {

            if (UIUtility.IsThisDeviceTablet())
            {
                puzzle.DOScale(deviceTabletSize, 0.5f);
            }
            
            whenPuzzleIsFinishedEvent.Invoke();
            levelText.text = "YOUR LEVEL\n" + levelManager.CurrentLevel;
            winningXPText.text = levelManager.CurrentExperience + ":XP / " + levelManager.NextLevelExperience + ":XP";
            _sequenceLevelBar = DOTween.Sequence();
            _sequenceLevelBar.Append(winningUIMessageText.DOAnchorPos(new Vector3(0, -246f, 0), winTextAnimDuration))
                .SetEase(easeOut);
            _sequenceLevelBar.Append(winningUI.DOAnchorPos(new Vector3(0, 0, 0), winLevelBarAnimDuration))
                .SetEase(easeOut).OnComplete(ShowSlideProgression);
            _sequenceLevelBar.Play();
        }

        [ContextMenu("TestProgression")]
        private void ShowSlideProgression()
        {
            float finalValue = (float)levelManager.CurrentExperience / levelManager.NextLevelExperience * 100;
            slider.DOValue(finalValue, 2).SetEase(easeBar).OnComplete(
                () => { Debug.Log("Slider value finished!"); });
        }
        
    }
}