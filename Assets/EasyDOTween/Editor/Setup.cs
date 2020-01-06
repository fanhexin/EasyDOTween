using System;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEditor;
using UnityEngine;

namespace EasyDOTween.Editor
{
    public static class Setup
    {
        [UnityEditor.Callbacks.DidReloadScripts]
        static void OnScriptsReloaded()
        {
            var dotweenSettings = Resources.Load<DOTweenSettings>(nameof(DOTweenSettings));
            DetectModule(dotweenSettings.modules.audioEnabled, typeof(DOTweenModuleAudio));
            DetectModule(dotweenSettings.modules.physicsEnabled, typeof(DOTweenModulePhysics));
            DetectModule(dotweenSettings.modules.physics2DEnabled, typeof(DOTweenModulePhysics2D));
            DetectModule(dotweenSettings.modules.spriteEnabled, typeof(DOTweenModuleSprite));
            DetectModule(dotweenSettings.modules.uiEnabled, typeof(DOTweenModuleUI));
        }

        static void DetectModule(bool moduleEnabled, Type type)
        {
            if (moduleEnabled)
            {
                AddMicroDefine(type);
            }
            else
            {
                DeleteMicroDefine(type);
            }
        }

        static void AddMicroDefine(Type type)
        {
            string name = GetMacroName(type);
            AddMicroDefine(EditorUserBuildSettings.selectedBuildTargetGroup, name);
            AddMicroDefine(BuildTargetGroup.Android, name);
            AddMicroDefine(BuildTargetGroup.iOS, name);
        }

        static string GetMacroName(Type type)
        {
            return $"{nameof(EasyDOTween)}_{type.Name.Replace("DOTweenModule", string.Empty)}";
        }

        static void DeleteMicroDefine(Type type)
        {
            string name = GetMacroName(type);
            DeleteMicroDefine(EditorUserBuildSettings.selectedBuildTargetGroup, name);
            DeleteMicroDefine(BuildTargetGroup.Android, name);
            DeleteMicroDefine(BuildTargetGroup.iOS, name);
        }

        static void AddMicroDefine(BuildTargetGroup buildTargetGroup, string name)
        {
            string defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            if (defineSymbols.Contains(name))
            {
                return;
            }
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, $"{defineSymbols};{name}");        
        }

        static void DeleteMicroDefine(BuildTargetGroup buildTargetGroup, string name)
        {
            string defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            defineSymbols.Replace($"{name}", string.Empty);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, defineSymbols);        
        }
    }
}