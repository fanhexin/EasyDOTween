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

namespace EasyDOTween.Animation.ScrollRect
{
    using DG.Tweening;
    
    
    [UnityEngine.AddComponentMenu("EasyDOTween/ScrollRect/DOHorizontalNormalizedPos")]
    [UnityEngine.RequireComponent(typeof(UnityEngine.UI.ScrollRect))]
    public class DOHorizontalNormalizedPos : EasyDOTween.Animation<UnityEngine.UI.ScrollRect>
    {
        
        [UnityEngine.SerializeField()]
        private float endValue;
        
        [UnityEngine.SerializeField()]
        private bool snapping = false;
        
        protected override DG.Tweening.Tween CreateTween(UnityEngine.UI.ScrollRect target, float duration)
        {
            return target.DOHorizontalNormalizedPos(endValue, duration, snapping);
        }
    }
}

#endif