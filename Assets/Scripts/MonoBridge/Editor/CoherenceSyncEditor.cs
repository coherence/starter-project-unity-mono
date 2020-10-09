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
        private readonly Type[] supportedTypes = {
            typeof(Vector3),
            typeof(Quaternion),
            typeof(float),
            typeof(int),
            typeof(uint),
            typeof(string)
        };

        private Texture2D texture;

        private void OnEnable()
        {
            texture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Coherence Samples/Interface/Textures/Logo.png");
        }

        private bool IsTypeSupported(Type type)
        {
            foreach (Type ct in supportedTypes)
            {
                if (type.IsSubclassOf(ct) || type == ct)
                {
                    return true;
                }
            }

            return false;
        }

        public override void OnInspectorGUI()
        {
            CoherenceSync coherenceSync = (CoherenceSync)target;
            if (coherenceSync == null)
            {
                return;
            }

            bool anyChangesMade = false;
            serializedObject.Update();

            _ = EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Select none", EditorStyles.miniButtonLeft))
            {
                (target as CoherenceSync)?.Reset();
                anyChangesMade = true;
            }
            EditorGUI.BeginDisabledGroup(true);
            if (GUILayout.Button("Select all", EditorStyles.miniButtonRight))
            {
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel++;
            CycleThroughPublicVariables();
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Instantiation on non-simulators", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            anyChangesMade = SynchronizedPrefabDropdown(coherenceSync, anyChangesMade);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            EditorGUI.BeginDisabledGroup(true);
            _ = GUILayout.Button("Bake network components");
            EditorGUILayout.HelpBox("Syncing data using reflection. Bake network components for additional performance.", MessageType.Warning);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Linked entity", coherenceSync.LinkedEntity.ToString());
            EditorGUILayout.LabelField("Network prefab", coherenceSync.remoteVersionPrefabName);
            EditorGUILayout.LabelField("Simulated", coherenceSync.isSimulated.ToString());
            // EditorGUILayout.LabelField("Debug", coherenceSync.GetDebugData().ToString());

            // if (GUILayout.Button("Test enable/disable scripts"))
            // {
            //     coherenceSync.EnableAndDisableScripts();
            // }

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
                if (selection == CoherenceSync.SynchronizedPrefabOptions.Self)
                {
                    coherenceSync.remoteVersionPrefabName = GetPrefabName(coherenceSync.gameObject);
                }
                coherenceSync.SelectedSynchronizedPrefabOption = selection;
                anyChangesMade = true;
            }

            EditorGUI.BeginDisabledGroup(selection != CoherenceSync.SynchronizedPrefabOptions.Other);
            EditorGUI.BeginChangeCheck();
            string newFieldContent = EditorGUILayout.TextField("Resource name path", coherenceSync.remoteVersionPrefabName);
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
            EditorGUILayout.HelpBox("GameObjects are loaded using Resources.Load.", MessageType.None);

            EditorGUI.BeginDisabledGroup(selection != CoherenceSync.SynchronizedPrefabOptions.Self);
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
                    coherenceSync.SetEnabledScriptToggle(compTypeString, compTypeIncluded);
                    anyChangesMade = true;
                }
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

        public Type GetUnderlyingType(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                case MemberTypes.All:
                case MemberTypes.Constructor:
                case MemberTypes.Custom:
                case MemberTypes.NestedType:
                case MemberTypes.TypeInfo:
                default:
                    throw new ArgumentException
                    (
                        "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }

        private void CycleThroughPublicVariables()
        {
            CoherenceSync coherenceSync = (CoherenceSync)target;

            if (target == null)
            {
                return;
            }

            bool anyChangesMade = false;

            Type monoBehaviourType = typeof(MonoBehaviour);
            const BindingFlags monoBindingFlags = BindingFlags.Public | BindingFlags.Instance;
            MemberInfo[] monoMembers = monoBehaviourType.GetFields(monoBindingFlags).Cast<MemberInfo>()
                .Concat(monoBehaviourType.GetProperties(monoBindingFlags)).ToArray();

            Component[] components = coherenceSync.gameObject.GetComponents(typeof(Component));

            foreach (Component myComp in components)
            {
                Type compType = myComp.GetType();

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

                if (compType.IsSubclassOf(typeof(Collider)) || compType == typeof(Collider))
                {
                    continue;
                }

                if (compType.IsSubclassOf(typeof(Rigidbody)) || compType == typeof(Rigidbody))
                {
                    continue;
                }

                if (compType.IsSubclassOf(typeof(Rigidbody2D)) || compType == typeof(Rigidbody2D))
                {
                    continue;
                }

                if (compType.IsSubclassOf(typeof(Collider)) || compType == typeof(Collider))
                {
                    continue;
                }

                if (compType.IsSubclassOf(typeof(Collider2D)) || compType == typeof(Collider2D))
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

                const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
                MemberInfo[] members = compType.GetFields(bindingFlags).Cast<MemberInfo>()
                    .Concat(compType.GetProperties(bindingFlags)).ToArray();

                foreach (MemberInfo variable in members)
                {
                    if (compType == typeof(Transform))
                    {
                        if (variable.Name != "rotation" && variable.Name != "position")
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

                    Type fieldType = GetUnderlyingType(variable);

                    if (!IsTypeSupported(fieldType))
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
            }

            if (anyChangesMade)
            {
                Undo.RecordObject(target, "Coherence Sync");
                EditorUtility.SetDirty(target);
            }
        }
    }
}