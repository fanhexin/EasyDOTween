#if EasyDOTween_Physics
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyDOTween.Animation.Rigidbody
{
    using DG.Tweening;
    
    
    [UnityEngine.AddComponentMenu("EasyDOTween/Rigidbody/DOMove")]
    [UnityEngine.RequireComponent(typeof(UnityEngine.Rigidbody))]
    public class DOMove : EasyDOTween.Animation<UnityEngine.Rigidbody>
    {
        
        [UnityEngine.SerializeField()]
        private UnityEngine.Vector3 endValue;
        
        [UnityEngine.SerializeField()]
        private bool snapping = false;
        
        protected override DG.Tweening.Tween CreateTween(UnityEngine.Rigidbody target, float duration)
        {
            return target.DOMove(endValue, duration, snapping);
        }
    }
}

#endif