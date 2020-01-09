using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace EasyDOTween.Editor
{
    public static class Extension
    {
        public static void EditorGUI(this ParameterInfo info, Rect position, ref object value)
        {
            if (info.ParameterType.IsSubclassOf(typeof(Object)))
            {
                value = UnityEditor.EditorGUI.ObjectField(position, info.Name, (Object) value, info.ParameterType);
            }
            else if (info.ParameterType == typeof(bool))
            {
                value = UnityEditor.EditorGUI.ToggleLeft(position, info.Name, (bool) value);
            }
            else if (info.ParameterType == typeof(int))
            {
                value = UnityEditor.EditorGUI.IntField(position, info.Name, (int) value);
            }
            else if (info.ParameterType == typeof(float))
            {
                value = UnityEditor.EditorGUI.FloatField(position, info.Name, (float) value);
            }
            else if (info.ParameterType == typeof(double))
            {
                value = UnityEditor.EditorGUI.DoubleField(position, info.Name, (double) value);
            }
            else if (info.ParameterType == typeof(string))
            {
                value = UnityEditor.EditorGUI.TextField(position, info.Name, (string) value);
            }
            else if (info.ParameterType == typeof(Vector3))
            {
                value = UnityEditor.EditorGUI.Vector3Field(position, info.Name, (Vector3) value);
            }
            else if (info.ParameterType == typeof(Color))
            {
                value = UnityEditor.EditorGUI.ColorField(position, info.Name, (Color) value);
            }
        }

        public static object ReflectionGetTarget(this SerializedProperty sp)
        {
            object target = sp.serializedObject.targetObject;
            if (sp.depth == 0)
            {
                return target;
            }

            object ret = null;
            string[] propertyPath = sp.propertyPath.Split('.');
            for (var i = 0; i < propertyPath.Length; i++)
            {
                string path = propertyPath[i];
                var field = target.GetType().GetField(path, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                ret = field.GetValue(target);
                target = ret;
            }
            return ret;
        }
    }
}