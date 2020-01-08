using System;
using DG.Tweening;
using EasyDOTween;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField, TweenPreview]
    MoveTo _moveTo;

    [SerializeField]
    TweenAnimBundle _bundle;

    [Serializable]
    class MoveTo
    {
        [SerializeField]
        Vector3 _pos;
        
        [SerializeField]
        float _duration;

        public Tween PlayMoveX(Transform t)
        {
            return t.DOMoveX(_pos.x, _duration);
        }

        public Tween PlayMoveY(Transform t)
        {
            return t.DOMoveY(_pos.y, _duration);
        }
    }

    [Serializable]
    class TweenAnimBundle
    {
        [SerializeField, TweenPreview]
        MoveTo _moveTo;
    }
}
