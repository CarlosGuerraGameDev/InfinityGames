using DG.Tweening;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "AnimationParameters", menuName = "Animation/Create new parameters", order = 0)]

    public class AnimationConfigParameters : ScriptableObject
    {
        public Vector2 position = new Vector2(0,0);
        public int alpha = 1;
        public int time = 1;
        public Ease ease = Ease.Linear;
        public float scaleValue = 1f;
        public float scaleTabletValue = 0.5f;
    }
}