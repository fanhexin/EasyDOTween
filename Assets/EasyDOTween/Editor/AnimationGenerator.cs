using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DG.Tweening;
using Microsoft.CSharp;
using UnityEditor;
using UnityEngine;

namespace EasyDOTween.Editor
{
    public static class AnimationGenerator
    {
        const string SAVE_PATH = "Assets/EasyDOTween";
        
        [MenuItem("EasyDOTween/GenerateAnimations")]
        static void Generate()
        {        
            Generate(typeof(ShortcutExtensions));
        }

        static void Generate(Type t)
        {
            foreach (MethodInfo methodInfo in t.GetMethods(BindingFlags.Static | BindingFlags.Public))
            {
                var p = methodInfo.GetParameters().First();
                if (!p.ParameterType.IsSubclassOf(typeof(Component)))
                {
                    continue;
                }
                
                Generate(methodInfo);
            }
            
            AssetDatabase.Refresh();
        }

        static void Generate(MethodInfo methodInfo)
        {
            ParameterInfo[] parameters = methodInfo.GetParameters();
            
            ParameterInfo firstParameter = parameters.First();
            ParameterInfo durationParameter = parameters.First(x => x.Name == "duration");
            
            var compileUnit = new CodeCompileUnit();
            var codeNamespace = new CodeNamespace($"{nameof(EasyDOTween)}.Animation.{firstParameter.ParameterType.Name}");
            codeNamespace.Imports.Add(new CodeNamespaceImport("DG.Tweening"));

            var animation = new CodeTypeDeclaration(methodInfo.Name)
            {
                IsClass = true, 
                BaseTypes =
                {
                    new CodeTypeReference(typeof(Animation<>))
                    {
                        TypeArguments = { new CodeTypeReference(firstParameter.ParameterType) }
                    }
                },
                CustomAttributes =
                {
                    new CodeAttributeDeclaration(new CodeTypeReference(typeof(AddComponentMenu)),
                        new CodeAttributeArgument(
                            new CodePrimitiveExpression($"{nameof(EasyDOTween)}/{firstParameter.ParameterType.Name}/{methodInfo.Name}")))
                }
            };
            
            if (firstParameter.ParameterType != typeof(Transform))
            {
                animation.CustomAttributes.Add(firstParameter.ParameterType.ToRequireComponentCodeDom());
            }
            
            foreach (ParameterInfo info in parameters)
            {
                if (info == firstParameter || info == durationParameter)
                {
                    continue;
                }

                var field = new CodeMemberField(info.ParameterType, info.Name)
                {
                    CustomAttributes = {new CodeAttributeDeclaration(new CodeTypeReference(typeof(SerializeField)))}
                };

                if (info.ParameterType.IsPrimitive && info.HasDefaultValue)
                {
                    field.InitExpression = new CodePrimitiveExpression(info.DefaultValue);
                }
                animation.Members.Add(field);
            }
            
            var method = new CodeMemberMethod
            {
                Name = "CreateTween",
                Attributes = MemberAttributes.Override | MemberAttributes.Family,
                ReturnType = new CodeTypeReference(typeof(Tween)),
                Parameters =
                {
                    firstParameter.ToCodeDom(),
                    durationParameter.ToCodeDom()
                }
            };

            var varExpression = new CodeVariableReferenceExpression(firstParameter.Name);
            var methodInvokeExpression = new CodeMethodInvokeExpression(varExpression, methodInfo.Name);
            for (int i = 1; i < parameters.Length; i++)
            {
                var info = parameters[i];
                methodInvokeExpression.Parameters.Add(new CodeVariableReferenceExpression(info.Name));
            }
            method.Statements.Add(new CodeMethodReturnStatement(methodInvokeExpression));

            animation.Members.Add(method);

            codeNamespace.Types.Add(animation);
            
            compileUnit.Namespaces.Add(codeNamespace);
            
            var sb = new StringBuilder();
            var provider = new CSharpCodeProvider();
            using (var sw = new StringWriter(sb))
            {
                provider.GenerateCodeFromCompileUnit(compileUnit, sw, new CodeGeneratorOptions
                {
                    BracingStyle = "C"
                });
            }

            string savePath = $"{SAVE_PATH}/Animations";
            if (!Directory.Exists(savePath))
            {
                AssetDatabase.CreateFolder(SAVE_PATH, "Animations");
            }
            
            File.WriteAllText($"{savePath}/{methodInfo.Name}.cs", sb.ToString());
        }

        static CodeParameterDeclarationExpression ToCodeDom(this ParameterInfo parameterInfo)
        {
            return new CodeParameterDeclarationExpression(parameterInfo.ParameterType, parameterInfo.Name);
        }

        static CodeAttributeDeclaration ToRequireComponentCodeDom(this Type type)
        {
            var typeRef = new CodeTypeReference(typeof(RequireComponent));
            var attrArg = new CodeAttributeArgument(new CodeTypeOfExpression(type));
            return new CodeAttributeDeclaration(typeRef, attrArg);
        }

        static bool IsSubclassOf(this Type t, Type parentType)
        {
            if (t.BaseType == null)
            {
                return false;
            }
            
            if (t.BaseType == parentType)
            {
                return true;
            }

            return t.BaseType.IsSubclassOf(parentType);
        }

        static string ToSuitedName(this Type t)
        {
            if (!t.IsPrimitive)
            {
                return t.FullName;
            }

            if (t == typeof(int))
            {
                return "int";
            }

            if (t == typeof(float))
            {
                return "float";
            }

            if (t == typeof(double))
            {
                return "double";
            }

            return t.FullName;
        }
    }
}