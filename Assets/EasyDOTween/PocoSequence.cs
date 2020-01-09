using System;
using DG.Tweening;
using UnityEngine;

namespace EasyDOTween
{
    [Serializable]
    public class PocoSequence<A, T, V> where A : PocoAnimation<T, V>
    {
        [SerializeField]
        A[] _animations;
        
        public Tween Play(T target)
        {
            var sequence = DOTween.Sequence();
            foreach (A animation in _animations)
            {
                sequence.Append(animation.Play(target));
            }
            return sequence;
        }
    }
}