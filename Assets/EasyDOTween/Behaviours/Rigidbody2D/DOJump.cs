#if EasyDOTween_Physics2D
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyDOTween.Animation.Rigidbody2D
{
    using DG.Tweening;
    
    
    [UnityEngine.AddComponentMenu("EasyDOTween/Rigidbody2D/DOJump")]
    [UnityEngine.RequireComponent(typeof(UnityEngine.Rigidbody2D))]
    public class DOJump : EasyDOTween.Animation<UnityEngine.Rigidbody2D>
    {
        
        [UnityEngine.SerializeField()]
        private UnityEngine.Vector2 endValue;
        
        [UnityEngine.SerializeField()]
        private float jumpPower;
        
        [UnityEngine.SerializeField()]
        private int numJumps;
        
        [UnityEngine.SerializeField()]
        private bool snapping = false;
        
        protected override DG.Tweening.Tween CreateTween(UnityEngine.Rigidbody2D target, float duration)
        {
            return target.DOJump(endValue, jumpPower, numJumps, duration, snapping);
        }
    }
}

#endif