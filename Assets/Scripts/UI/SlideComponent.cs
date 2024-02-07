using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class SlideComponent : MonoBehaviour
    {

        [SerializeField] private AnimationConfigParameters slideInParams;
        [SerializeField] private AnimationConfigParameters slideOutParams;

        [SerializeField] private RectTransform slideRectTransform;

        [SerializeField] private bool startOnAwake;
        
        public UnityEvent onSlideAnimationInEvent;
        public UnityEvent onSlideAnimationOutEvent;
        public UnityEvent onSlideAnimationAwakeEvent;
        public UnityEvent onSlideAnimationStartEvent;

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
            slideRectTransform.DOAnchorPos(parameters.position, parameters.time)
                .SetEase(parameters.ease).OnComplete(() => { callbackEvent.Invoke(); })
                .OnPlay(() => onSlideAnimationStartEvent.Invoke());
        }

    }

}