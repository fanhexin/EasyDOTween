//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyDOTween.Animation.Camera
{
    using DG.Tweening;
    
    
    [UnityEngine.AddComponentMenu("EasyDOTween/Camera/DOShakeRotation")]
    [UnityEngine.RequireComponent(typeof(UnityEngine.Camera))]
    public class DOShakeRotationVector3 : EasyDOTween.Animation<UnityEngine.Camera>
    {
        
        [UnityEngine.SerializeField()]
        private UnityEngine.Vector3 strength;
        
        [UnityEngine.SerializeField()]
        private int vibrato = 10;
        
        [UnityEngine.SerializeField()]
        private float randomness = 90F;
        
        [UnityEngine.SerializeField()]
        private bool fadeOut = true;
        
        protected override DG.Tweening.Tween CreateTween(UnityEngine.Camera target, float duration)
        {
            return target.DOShakeRotation(duration, strength, vibrato, randomness, fadeOut);
        }
    }
}
