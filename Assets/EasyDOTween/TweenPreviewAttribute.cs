using UnityEngine;

namespace EasyDOTween
{
    public class TweenPreviewAttribute : PropertyAttribute
    {
        public readonly string funcFilter;

        public TweenPreviewAttribute()
        {
            funcFilter = string.Empty;
        }

        public TweenPreviewAttribute(string funcFilter)
        {
            this.funcFilter = funcFilter;
        }    
    }
}