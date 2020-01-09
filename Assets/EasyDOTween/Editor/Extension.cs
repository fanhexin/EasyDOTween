using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

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
                value = UnityEditor.EditorGUI.ToggleLeft(position, info.Name, Convert.ToBoolean(value));
            }
            else if (info.ParameterType == typeof(int))
            {
                value = UnityEditor.EditorGUI.IntField(position, info.Name, Convert.ToInt32(value));
            }
            else if (info.ParameterType == typeof(float))
            {
                value = UnityEditor.EditorGUI.FloatField(position, info.Name, (float)Convert.ToDouble(value));
            }
            else if (info.ParameterType == typeof(double))
            {
                value = UnityEditor.EditorGUI.DoubleField(position, info.Name, Convert.ToDouble(value));
            }
            else if (info.ParameterType == typeof(string))
            {
                value = UnityEditor.EditorGUI.TextField(position, info.Name, value == null ? string.Empty : (string) value);
            }
            else if (info.ParameterType == typeof(Vector3))
            {
                value = UnityEditor.EditorGUI.Vector3Field(position, info.Name, (Vector3?) value ?? Vector3.zero);
            }
            else if (info.ParameterType == typeof(Color))
            {
                value = UnityEditor.EditorGUI.ColorField(position, info.Name, (Color?) value ?? Color.white);
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