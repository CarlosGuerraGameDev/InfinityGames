using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace UI
{
    public class ScaleAnimationComponent : MonoBehaviour
    {
        
        [SerializeField] private AnimationConfigParameters scaleOutParams;
        [SerializeField] private AnimationConfigParameters scaleInParams;

        [SerializeField] private RectTransform scaleRectTransform;

        [SerializeField] private bool startOnAwake;

        public UnityEvent onScaleAnimationInEvent;
        public UnityEvent onScaleAnimationOutEvent;
        public UnityEvent onScaleAnimationAwakeEvent;

        private void Awake()
        {
            if (startOnAwake) onScaleAnimationAwakeEvent.Invoke();
        }
        
        public void AnimateIn()
        {

            if (scaleInParams is null)
            {
                Debug.Log("You have to define the scale parameters! Did you create the configuration file?");
                return;
            }
            
            Animate(scaleInParams, onScaleAnimationInEvent);

        }

        public void AnimateOut()
        {
            if (scaleOutParams is null)
            {
                Debug.Log("You have to define the scale parameters! Did you create the configuration file?");
                return;
            }
            
            Animate(scaleOutParams, onScaleAnimationOutEvent);
        }
        
        private void Animate(AnimationConfigParameters parameters, UnityEvent callbackEvent)
        {
            if (UIUtility.IsThisDeviceTablet())
            {
                scaleRectTransform.DOScale(parameters.scaleTabletValue, parameters.time).SetEase(parameters.ease).OnComplete(() =>
                {
                    callbackEvent.Invoke();
                });
                return;
            }
            
            scaleRectTransform.DOScale(parameters.scaleValue, parameters.time).SetEase(parameters.ease).OnComplete(() =>
            {
                callbackEvent.Invoke();
            });
        }
        
        
    }
}