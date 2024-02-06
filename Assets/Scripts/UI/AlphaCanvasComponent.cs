using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class AlphaCanvasComponent : MonoBehaviour
    {
        [SerializeField] private AnimationConfigParameters slideInParams;
        [SerializeField] private AnimationConfigParameters slideOutParams;

        [SerializeField] private CanvasGroup slideRectTransform;

        [SerializeField] private bool startOnAwake;
        
        public UnityEvent onSlideAnimationInEvent;
        public UnityEvent onSlideAnimationOutEvent;
        public UnityEvent onSlideAnimationAwakeEvent;

        private void Awake()
        {
            if (startOnAwake) onSlideAnimationAwakeEvent.Invoke();
        }
        
        public void AnimateIn()
        {
            Animate(slideInParams, onSlideAnimationInEvent);
        }

        public void AnimateOut()
        {
            Animate(slideOutParams, onSlideAnimationOutEvent);
        }
        
        private void Animate(AnimationConfigParameters parameters, UnityEvent callbackEvent)
        {
            slideRectTransform.DOFade(parameters.alpha, parameters.time).SetEase(parameters.ease).OnComplete(() =>
            {
                callbackEvent.Invoke();
            });
        }

    }
}