using DG.Tweening;
using UnityEngine;

namespace EasyDOTween
{
    public abstract class BaseAnimation : MonoBehaviour
    {
        [SerializeField] bool _autoPlay = true;
        [SerializeField] bool _speedBased;
        [SerializeField] float _duration = 1;
        [SerializeField] int _loops;
        [SerializeField] LoopType _loopType;
        [SerializeField] Ease _ease = Ease.Linear;
        
        public Tween playingTween { get; private set; }
        
        protected virtual void Start()
        {
            if (_autoPlay)
            {
                Play();
            }
        }

        protected abstract Tween CreateTween(float duration);

        public Tween Play()
        {
            playingTween = CreateTween(_duration)
                .SetSpeedBased(_speedBased)
                .SetEase(_ease)
                .SetLoops(_loops, _loopType);
            return playingTween;
        }
    }
}