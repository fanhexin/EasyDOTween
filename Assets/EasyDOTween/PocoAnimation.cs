using System;
using DG.Tweening;
using UnityEngine;

namespace EasyDOTween
{
    [Serializable]
    public abstract class PocoAnimation<T, V>
    {
        [SerializeField] float _duration;
        [SerializeField] V _endValue;
        [SerializeField] Ease _ease;

        public Tween Play(T target)
        {
            return Play(target, _endValue, _duration).SetEase(_ease);
        }

        protected abstract Tween Play(T target, V endValue, float duration);
    }
}