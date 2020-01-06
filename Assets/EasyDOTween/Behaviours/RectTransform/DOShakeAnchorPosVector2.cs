#if EasyDOTween_UI
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyDOTween.Animation.RectTransform
{
    using DG.Tweening;
    
    
    [UnityEngine.AddComponentMenu("EasyDOTween/RectTransform/DOShakeAnchorPos")]
    [UnityEngine.RequireComponent(typeof(UnityEngine.RectTransform))]
    public class DOShakeAnchorPosVector2 : EasyDOTween.Animation<UnityEngine.RectTransform>
    {
        
        [UnityEngine.SerializeField()]
        private UnityEngine.Vector2 strength;
        
        [UnityEngine.SerializeField()]
        private int vibrato = 10;
        
        [UnityEngine.SerializeField()]
        private float randomness = 90F;
        
        [UnityEngine.SerializeField()]
        private bool snapping = false;
        
        [UnityEngine.SerializeField()]
        private bool fadeOut = true;
        
        protected override DG.Tweening.Tween CreateTween(UnityEngine.RectTransform target, float duration)
        {
            return target.DOShakeAnchorPos(duration, strength, vibrato, randomness, snapping, fadeOut);
        }
    }
}

#endif