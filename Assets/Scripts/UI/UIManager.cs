using DG.Tweening;
using Gameplay;
using LevelSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private RectTransform winningUI;
        [SerializeField] private Ease easeBar = Ease.Flash;
        [SerializeField] private Ease easeOut = Ease.Flash;
        [SerializeField] private Slider slider;

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
            _sequenceLevelBar = DOTween.Sequence();
            _sequenceLevelBar.Append(winningUI.DOAnchorPos(new Vector3(0, 0, 0), 1f)).SetEase(easeOut).OnComplete(ShowSlideProgression);
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