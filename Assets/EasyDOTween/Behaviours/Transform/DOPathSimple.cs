//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyDOTween.Animation.Transform
{
    using DG.Tweening;
    
    
    [UnityEngine.AddComponentMenu("EasyDOTween/Transform/DOPath")]
    public class DOPathSimple : EasyDOTween.Animation<UnityEngine.Transform>
    {
        
        [UnityEngine.SerializeField()]
        private UnityEngine.Vector3[] path;
        
        [UnityEngine.SerializeField()]
        private DG.Tweening.PathType pathType;
        
        [UnityEngine.SerializeField()]
        private DG.Tweening.PathMode pathMode;
        
        [UnityEngine.SerializeField()]
        private int resolution = 10;
        
        [UnityEngine.SerializeField()]
        private System.Nullable<UnityEngine.Color> gizmoColor;
        
        protected override DG.Tweening.Tween CreateTween(UnityEngine.Transform target, float duration)
        {
            return target.DOPath(path, duration, pathType, pathMode, resolution, gizmoColor);
        }
    }
}
