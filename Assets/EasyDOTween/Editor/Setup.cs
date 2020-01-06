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
            DetectModule(dotweenSettings.modules.audioEnabled, "Audio");
            DetectModule(dotweenSettings.modules.physicsEnabled, "Physics");
            DetectModule(dotweenSettings.modules.physics2DEnabled, "Physics2D");
            DetectModule(dotweenSettings.modules.spriteEnabled, "Sprite");
            DetectModule(dotweenSettings.modules.uiEnabled, "UI");
        }

        static void DetectModule(bool moduleEnabled, string name)
        {
            if (moduleEnabled)
            {
                AddMicroDefine(name);
            }
            else
            {
                DeleteMicroDefine(name);
            }
        }

        static void AddMicroDefine(string name)
        {
            name = GetMacroName(name);
            AddMicroDefine(EditorUserBuildSettings.selectedBuildTargetGroup, name);
            AddMicroDefine(BuildTargetGroup.Android, name);
            AddMicroDefine(BuildTargetGroup.iOS, name);
        }

        static string GetMacroName(string name)
        {
            return $"{nameof(EasyDOTween)}_{name}";
        }

        static void DeleteMicroDefine(string name)
        {
            name = GetMacroName(name);
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