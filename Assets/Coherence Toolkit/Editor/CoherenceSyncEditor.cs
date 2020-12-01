﻿using UnityEditor.Animations;

namespace Coherence.MonoBridge
{
    using System;
    using System.Linq;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(CoherenceSync))]
    [CanEditMultipleObjects]
    public class CoherenceSyncEditor : Editor
    {
#if COHERENCE_TOOLKIT
        private Texture2D logo;

        private void OnEnable()
        {
            if (logo == null)
            {
                logo = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Coherence Toolkit/Editor/Textures/Logo.png");
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space(6f);
            Rect rect = EditorGUILayout.GetControlRect(false, logo.height / 6f, GUILayout.Width(logo.width / 6f));
            GUI.DrawTexture(rect, logo);
            EditorGUILayout.Space();

            CoherenceSync coherenceSync = (CoherenceSync)target;
            if (coherenceSync == null)
            {
                return;
            }

            bool anyChangesMade = false;
            serializedObject.Update();

            EditorGUILayout.LabelField("Components you want to sync over the network", EditorStyles.boldLabel);
            _ = EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Deselect All", EditorStyles.miniButtonLeft))
            {
                (target as CoherenceSync)?.Reset();
                anyChangesMade = true;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel++;
            CycleThroughPublicVariables();
            EditorGUI.indentLevel--;

            EditorGUILayout.Space(12f);

            EditorGUILayout.LabelField("When this object is synchronized over the network", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            anyChangesMade = SynchronizedPrefabDropdown(coherenceSync, anyChangesMade);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space(12f);

            EditorGUI.BeginDisabledGroup(false);

            var usingReflection = coherenceSync.usingReflection;

            if (usingReflection)
            {
                var cmp = coherenceSync.gameObject.GetComponent<CoherenceSyncBaked>();
                if (cmp != null)
                {
                    usingReflection = false;
                }
            }

            if(GUILayout.Button("Bake (all) network components"))
            {
                Coherence.MonoBridge.SchemaCreator.GatherSyncBehavioursAndEmit();
            }

            if(usingReflection)
            {
                EditorGUILayout.HelpBox("Using reflection is slow. Bake network components for additional performance.", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.HelpBox("This game object has baked its network components.", MessageType.Info);
            }

            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space(6f);

            EditorGUILayout.LabelField("Linked entity", coherenceSync.LinkedEntity.ToString());
            EditorGUILayout.LabelField("Network prefab", coherenceSync.remoteVersionPrefabName);
            EditorGUILayout.LabelField("Simulated", coherenceSync.isSimulated.ToString());
            EditorGUILayout.LabelField("Reflection", coherenceSync.usingReflection.ToString());

            if (anyChangesMade)
            {
                Undo.RecordObject(target, "Changed selected scripts");
                EditorUtility.SetDirty(target);
            }

            _ = serializedObject.ApplyModifiedProperties();
        }

        private static bool SynchronizedPrefabDropdown(CoherenceSync coherenceSync, bool anyChangesMade)
        {
            if (coherenceSync.remoteVersionPrefabName == null)
            {
                coherenceSync.remoteVersionPrefabName = GetPrefabName(coherenceSync.gameObject);
                anyChangesMade = true;
            }

            EditorGUI.BeginChangeCheck();
            CoherenceSync.SynchronizedPrefabOptions selection = (CoherenceSync.SynchronizedPrefabOptions)EditorGUILayout.EnumPopup("Instantiate", coherenceSync.SelectedSynchronizedPrefabOption);
            if (EditorGUI.EndChangeCheck())
            {
                if (selection == CoherenceSync.SynchronizedPrefabOptions.This)
                {
                    coherenceSync.remoteVersionPrefabName = GetPrefabName(coherenceSync.gameObject);
                }
                coherenceSync.SelectedSynchronizedPrefabOption = selection;
                anyChangesMade = true;
            }

            EditorGUI.BeginDisabledGroup(selection != CoherenceSync.SynchronizedPrefabOptions.Other);
            EditorGUI.BeginChangeCheck();
            string newFieldContent = EditorGUILayout.TextField(new GUIContent("Resource name path", "GameObjects are loaded using Resources.Load"), coherenceSync.remoteVersionPrefabName);
            if (EditorGUI.EndChangeCheck())
            {
                coherenceSync.remoteVersionPrefabName = newFieldContent;
                anyChangesMade = true;
            }
            if (newFieldContent != coherenceSync.remoteVersionPrefabName)
            {
                coherenceSync.remoteVersionPrefabName = newFieldContent;
                anyChangesMade = true;
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(selection != CoherenceSync.SynchronizedPrefabOptions.This);
            EditorGUILayout.LabelField("Components enabled", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            Component[] components = coherenceSync.gameObject.GetComponents<Component>();

            foreach (Component myComp in components)
            {
                Type compType = myComp.GetType();
                string compTypeString = compType.AssemblyQualifiedName;

                if (compType.IsSubclassOf(typeof(Renderer)) || compType == typeof(Renderer))
                {
                    continue;
                }

                if (compType.IsSubclassOf(typeof(MeshFilter)) || compType == typeof(MeshFilter))
                {
                    continue;
                }

                if (compType.IsSubclassOf(typeof(CoherenceSync)) || compType == typeof(CoherenceSync))
                {
                    continue;
                }

                if (compType.IsSubclassOf(typeof(Transform)) || compType == typeof(Transform))
                {
                    continue;
                }

                bool? oldVal = coherenceSync.GetEnabledScriptToggle(compTypeString);
                EditorGUI.BeginChangeCheck();
                bool compTypeIncluded = EditorGUILayout.ToggleLeft(compType.ToString(), oldVal ?? false);
                if (EditorGUI.EndChangeCheck())
                {
                    anyChangesMade = true;
                }
                coherenceSync.SetEnabledScriptToggle(compTypeString, compTypeIncluded);
            }
            EditorGUI.indentLevel--;
            EditorGUI.EndDisabledGroup();

            return anyChangesMade;
        }

        private static string GetPrefabName(GameObject obj)
        {
            GameObject prefabGameObject = PrefabUtility.GetCorrespondingObjectFromSource(obj);
            if (prefabGameObject)
            {
                return prefabGameObject.name;
            }

            // now assuming we're in Project view, so we just return the prefab name
            return obj.name;
        }

        private void CycleThroughPublicVariables()
        {
            CoherenceSync coherenceSync = (CoherenceSync)target;

            var fieldsTurnedOnInBackend = coherenceSync.CountSyncFieldsTurnedOn();
            uint fieldsCheckedInEditorGUI = 0;

            if (target == null)
            {
                return;
            }

            bool anyChangesMade = false;

            MemberInfo[] monoMembers = TypeHelpers.MonoMembers;

            Component[] components = coherenceSync.gameObject.GetComponents(typeof(Component));

            foreach (Component myComp in components)
            {
                Type compType = myComp.GetType();

                if(TypeHelpers.SkipThisType(compType))
                {
                    continue;
                }

                string compTypeString = compType.AssemblyQualifiedName;
                bool? prevTypeIncluded = coherenceSync.GetScriptToggle(compTypeString);
                EditorGUI.BeginChangeCheck();
                bool compTypeIncluded = EditorGUILayout.ToggleLeft(compType.ToString(), prevTypeIncluded ?? false);

                if (EditorGUI.EndChangeCheck())
                {
                    anyChangesMade = true;
                    coherenceSync.SetScriptToggle(compTypeString, compTypeIncluded);
                }

                EditorGUI.BeginDisabledGroup(!compTypeIncluded);

                if (compType.IsSubclassOf(typeof(Animator)) || compType == typeof(Animator))
                {
                    Animator animator = coherenceSync.gameObject.GetComponent<Animator>();

                    AnimatorController controller = animator.runtimeAnimatorController as AnimatorController;

                    if(controller == null)
                    {
                        continue;
                    }

                    foreach (var parameter in controller.parameters)
                    {
                        EditorGUI.indentLevel++;
                        try
                        {
                            string varString = compTypeString + CoherenceSync.KeyDelimiter + parameter.name;

                            bool? prevVarIncluded = coherenceSync.GetFieldToggle(varString);

                            Type fieldType = typeof(bool);

                            switch (parameter.type)
                            {
                                case AnimatorControllerParameterType.Bool:
                                    fieldType = typeof(bool);
                                    break;

                                case AnimatorControllerParameterType.Float:
                                    fieldType = typeof(float);
                                    break;

                                case AnimatorControllerParameterType.Int:
                                    fieldType = typeof(int);
                                    break;

                                case AnimatorControllerParameterType.Trigger:
                                    // TODO: support triggers through commands
                                    break;
                            }

                            EditorGUI.BeginChangeCheck();
                            bool varIncluded = EditorGUILayout.ToggleLeft($"{parameter.name} [{fieldType.Name}]", prevVarIncluded ?? false);

                            if (varIncluded) fieldsCheckedInEditorGUI++;

                            if (EditorGUI.EndChangeCheck())
                            {
                                anyChangesMade = true;
                                coherenceSync.SetFieldToggle(varString, varIncluded);
                                coherenceSync.ToggleFieldSync(varString, fieldType, varIncluded);
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(e);
                        }

                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.EndDisabledGroup();
                    continue;
                }

                var members = TypeHelpers.Members(compType);

                foreach (MemberInfo variable in members)
                {
                    if (compType == typeof(Transform))
                    {
                        if (variable.Name != "rotation" && variable.Name != "position" && variable.Name != "localScale")
                        {
                            continue;
                        }
                    }

                    bool doProceed = true;
                    // Remove those inherited from MonoBehaviour (de-clutter)
                    foreach (MemberInfo monoVariable in monoMembers)
                    {
                        if (variable.Name == monoVariable.Name)
                        {
                            doProceed = false;
                            break;
                        }
                    }

                    if (!doProceed)
                    {
                        continue;
                    }

                    Type fieldType = TypeHelpers.GetUnderlyingType(variable);

                    if (!TypeHelpers.IsTypeSupported(fieldType))
                    {
                        continue;
                    }

                    EditorGUI.indentLevel++;
                    try
                    {
                        string varString = compTypeString + CoherenceSync.KeyDelimiter + variable.Name;

                        bool? prevVarIncluded = coherenceSync.GetFieldToggle(varString);

                        EditorGUI.BeginChangeCheck();
                        bool varIncluded = EditorGUILayout.ToggleLeft($"{variable.Name} [{fieldType.Name}]", prevVarIncluded ?? false);

                        if (varIncluded) fieldsCheckedInEditorGUI++;

                        if (EditorGUI.EndChangeCheck())
                        {
                            anyChangesMade = true;
                            coherenceSync.SetFieldToggle(varString, varIncluded);
                            coherenceSync.ToggleFieldSync(varString, fieldType, varIncluded);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }

                    EditorGUI.indentLevel--;
                }

                var methods = TypeHelpers.Methods(compType);

                foreach (var methodInfo in methods)
                {
                    if(!TypeHelpers.ShowThisMethod(methodInfo) ||
                       compType == typeof(Transform))
                    {
                        continue;
                    }

                    var declaringType = methodInfo.DeclaringType; // What class/struct/etc the method is created on
                    var argsString = TypeHelpers.MethodArgsAsString(methodInfo);

                    EditorGUI.indentLevel++;
                    try
                    {
                        string methodString = compTypeString + CoherenceSync.KeyDelimiter + methodInfo.Name;

                        bool? prevMethodIncluded = coherenceSync.GetFieldToggle(methodString);

                        EditorGUI.BeginChangeCheck();
                        // This is the name that has to be used in .SendCommand when invoking the method remotely
                        var toggleLabelText = TypeHelpers.NamespacedMethodName(methodInfo);
                        bool varIncluded = EditorGUILayout.ToggleLeft(toggleLabelText, prevMethodIncluded ?? false);

                        if (varIncluded) fieldsCheckedInEditorGUI++;

                        if (EditorGUI.EndChangeCheck())
                        {
                            anyChangesMade = true;
                            coherenceSync.SetFieldToggle(methodString, varIncluded);
                            coherenceSync.ToggleFieldSync(methodString, methodInfo, varIncluded);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }

                    EditorGUI.indentLevel--;
                }

                EditorGUI.EndDisabledGroup();
            }

            if (anyChangesMade)
            {
                Undo.RecordObject(target, "Coherence Sync");
                EditorUtility.SetDirty(target);
            }
            else
            {
                if (fieldsTurnedOnInBackend != fieldsCheckedInEditorGUI)
                {
                    Debug.Log($"DEBUG: Field selection mismatch, GUI:{fieldsCheckedInEditorGUI}, data:{fieldsTurnedOnInBackend}. Fixing data (probably cause: source field renamed or removed).");
                    coherenceSync.TestAndFixSyncReferenceData();
                    Undo.RecordObject(target, "Coherence Sync");
                    EditorUtility.SetDirty(target);
                }
            }
        }
#endif
    }
}
