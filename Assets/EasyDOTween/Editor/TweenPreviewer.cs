using System.Linq;
using System.Reflection;
using DG.DOTweenEditor;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EasyDOTween.Editor
{
    [CustomPropertyDrawer(typeof(TweenPreviewAttribute))]
    public class TweenPreviewer : PropertyDrawer
    {
        MethodView[] _methods;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, true);
            if (!property.isExpanded)
            {
                return;
            }
            
            float height = EditorGUI.GetPropertyHeight(property, true);
            position.y += height + 2f;

            float h = EditorGUI.GetPropertyHeight(property, false);
            
            var attr = attribute as TweenPreviewAttribute;
            _methods = _methods ?? fieldInfo.FieldType
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(x => x.ReturnType == typeof(Tween) && x.Name.Contains(attr.funcFilter))
                .Select(x => new MethodView(x, h))
                .ToArray();

            var target = fieldInfo.GetValue(property.serializedObject.targetObject);
            using (new EditorGUI.IndentLevelScope())
            {
                foreach (MethodView methodView in _methods)
                {
                    methodView.Render(ref position, target);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float offset = 0;
            if (property.isExpanded && _methods != null)
            {
                offset = _methods.Sum(x => x.height);
            }
            return EditorGUI.GetPropertyHeight(property, true) + offset;
        }

        public override bool CanCacheInspectorGUI(SerializedProperty property)
        {
            return false;
        }

        class MethodView
        {
            const int BORDER_SIZE = 5;
            const int INDENT_WIDTH = 20;
            
            readonly MethodInfo _methodInfo;
            readonly float _itemHeight;
            readonly float _gap;
            
            public readonly float height;
            readonly ParameterInfo[] _parameterInfos;
            readonly object[] _parameters;

            Tween _tween;

            public MethodView(MethodInfo methodInfo, float itemHeight = 16f, float gap = 2f)
            {
                _methodInfo = methodInfo;
                _itemHeight = itemHeight;
                _gap = gap;
                _parameterInfos = methodInfo.GetParameters()
                    .Where(x => x.ParameterType.IsSubclassOf(typeof(Object)) 
                                || x.ParameterType.IsPrimitive)
                    .ToArray();
                _parameters = new object[_parameterInfos.Length];
                height = (_parameters.Length + 1) * (itemHeight + gap) - gap + BORDER_SIZE * 2;
            }

            public void Render(ref Rect position, object target)
            {
                Rect pos = position;
                pos.height = height;

                Rect boxPos = pos;
                SetIndent(ref boxPos, INDENT_WIDTH - _gap);
                GUI.Box(boxPos, string.Empty);
                
                pos.width -= BORDER_SIZE * 2;
                pos.x += BORDER_SIZE;
                pos.y += BORDER_SIZE;
                
                pos.height = _itemHeight;
                for (int i = 0; i < _parameterInfos.Length; i++)
                {
                    ParameterInfo parameterInfo = _parameterInfos[i];
                    parameterInfo.EditorGUI(pos, ref _parameters[i]);
                    pos.y += _itemHeight + _gap;
                }

                SetIndent(ref pos, INDENT_WIDTH);
                if (GUI.Button(pos, _tween == null ? _methodInfo.Name : "Stop"))
                {
                    if (_tween == null)
                    {
                        _tween = (Tween) _methodInfo.Invoke(target, _parameters);
                        _tween.OnComplete(CompleteTween);
                        DOTweenEditorPreview.PrepareTweenForPreview(_tween, false);
                        DOTweenEditorPreview.Start();
                    }
                    else
                    {
                        _tween.Kill();    
                        CompleteTween();
                    }
                }
                SetIndent(ref pos, -INDENT_WIDTH);

                position.y = pos.y + _itemHeight + _gap + BORDER_SIZE; 
            }
            
            void CompleteTween()
            {
                DOTweenEditorPreview.Stop(true);
                _tween = null;
            }

            void SetIndent(ref Rect position, float size)
            {
                position.x += size;
                position.width -= size;
            }
        }
    }
}