using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace Coherence.MonoBridge
{
    [CustomEditor(typeof(CoherenceSync))]
    [CanEditMultipleObjects]
    public class CoherenceSyncEditor : UnityEditor.Editor
    {
        private Type[] supportedTypes =
            {typeof(Vector3), typeof(Quaternion), typeof(float), typeof(int), typeof(uint), typeof(string)};

        private Texture2D texture;
        
        void OnEnable()
        {
            texture = (Texture2D)Resources.Load("Coherence_Main_Logotype_Positive");
        }

        bool IsTypeSupported(Type type)
        {
            foreach (Type ct in supportedTypes)
            {
                if (type.IsSubclassOf(ct) || type == ct) return true;
            }

            return false;
        }

        public override void OnInspectorGUI()
        {
            var coherenceSync = (CoherenceSync)target;
            if (coherenceSync == null) return;
            bool anyChangesMade = false;            
            serializedObject.Update ();
            
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();
            GUI.DrawTexture(new Rect(20, 5, 180, 51), texture, ScaleMode.ScaleToFit);
            
            for(int i=0; i<9; i++)EditorGUILayout.Space();
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Select the components you want to sync over the network:");

            EditorGUI.indentLevel++;
            CycleThroughPublicVariables();
            EditorGUI.indentLevel--;
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("When this object is synchronized over the network:");
            EditorGUI.indentLevel++;
            anyChangesMade = SynchronizedPrefabDropdown(coherenceSync, anyChangesMade);
            EditorGUI.indentLevel--;
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            Color prevColor = GUI.color;
            GUI.color = Color.red;
            EditorGUILayout.LabelField("Warning! Using reflection (slow). Bake network components for more performance.");
            GUI.color = prevColor;
            GUILayout.Button("Bake network components");
            if (GUILayout.Button("Reset"))
            {
                (target as CoherenceSync)?.Reset();
                anyChangesMade = true;
            }
            
            EditorGUILayout.LabelField($"Linked entity: {coherenceSync.LinkedEntity}");
            EditorGUILayout.LabelField($"IsSimulated: {coherenceSync.isSimulated}");
            EditorGUILayout.LabelField($"Network prefab: {coherenceSync.remoteVersionPrefabName}");
/*            EditorGUILayout.LabelField($"Debug: [{coherenceSync.GetDebugData()}]");

            if (GUILayout.Button("Test enable/disable scripts"))
            {
                coherenceSync.EnableAndDisableScripts();
            }
 */           
            if (anyChangesMade)
            {
                Undo.RecordObject(target, "Changed selected scripts");
                EditorUtility.SetDirty(target);
            }
            serializedObject.ApplyModifiedProperties ();
        }

        private static bool SynchronizedPrefabDropdown(CoherenceSync coherenceSync, bool anyChangesMade)
        {
            if (coherenceSync.remoteVersionPrefabName == null)
            {
                coherenceSync.remoteVersionPrefabName = GetPrefabName(coherenceSync.gameObject);
                anyChangesMade = true;
            }
            
            EditorGUILayout.LabelField("Network version of prefab to load:", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            int selection = EditorGUILayout.Popup((int) coherenceSync.SelectedSynchronizedPrefabOption, Enum
                .GetNames(typeof(CoherenceSync.SynchronizedPrefabOptions))
                .ToArray());
            
            if (selection != (int) coherenceSync.SelectedSynchronizedPrefabOption)
            {
                coherenceSync.SelectedSynchronizedPrefabOption = (CoherenceSync.SynchronizedPrefabOptions) selection;
                anyChangesMade = true;
            }
            
            if (coherenceSync.SelectedSynchronizedPrefabOption == CoherenceSync.SynchronizedPrefabOptions.This)
            {
                coherenceSync.remoteVersionPrefabName = GetPrefabName(coherenceSync.gameObject);

                EditorGUI.BeginDisabledGroup(true);
                var newFieldContent = EditorGUILayout.TextField(coherenceSync.remoteVersionPrefabName);
                EditorGUI.EndDisabledGroup();
                
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
                
                EditorGUILayout.LabelField("Scripts left enabled:", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                Component[] components = coherenceSync.gameObject.GetComponents(typeof(Component));

                EditorGUILayout.BeginVertical();
                foreach (Component myComp in components)
                {
                    Type compType = myComp.GetType();
                    string compTypeString = compType.AssemblyQualifiedName;
                    if (compType.IsSubclassOf(typeof(Renderer)) || compType == typeof(Renderer)) continue;
                    if (compType.IsSubclassOf(typeof(MeshFilter)) || compType == typeof(MeshFilter)) continue;
                    if (compType.IsSubclassOf(typeof(CoherenceSync)) || compType == typeof(CoherenceSync)) continue;
                    if (compType.IsSubclassOf(typeof(Transform)) || compType == typeof(Transform)) continue;

                    // EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(compType.ToString(), EditorStyles.boldLabel);

                    bool? oldVal = coherenceSync.GetEnabledScriptToggle(compTypeString);
                    bool? compTypeIncluded = EditorGUILayout.Toggle( oldVal ?? false );
                    if (oldVal != compTypeIncluded)
                    {
                        coherenceSync.SetEnabledScriptToggle(compTypeString, (bool)compTypeIncluded);
                        anyChangesMade = true;
                    }
                    
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel--;
                EditorGUILayout.EndVertical();
            }
            else
            {
                var newFieldContent = EditorGUILayout.TextField(coherenceSync.remoteVersionPrefabName);
                EditorGUILayout.EndHorizontal();
                if (newFieldContent != coherenceSync.remoteVersionPrefabName)
                {
                    coherenceSync.remoteVersionPrefabName = newFieldContent;
                    anyChangesMade = true;
                }
            }
            
            return anyChangesMade;
        }

        private static string GetPrefabName(GameObject obj)
        {
            var prefabGameObject = PrefabUtility.GetCorrespondingObjectFromSource(obj);
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
                default:
                    throw new ArgumentException
                    (
                        "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }
        
        void CycleThroughPublicVariables()
        {
            CoherenceSync coherenceSync = (CoherenceSync)target;

            if (target == null) return;

            bool anyChangesMade = false;
            
            Type monoBehaviourType = typeof(MonoBehaviour);
            const BindingFlags monoBindingFlags = BindingFlags.Public | BindingFlags.Instance;
            MemberInfo[] monoMembers = monoBehaviourType.GetFields(monoBindingFlags).Cast<MemberInfo>()
                .Concat(monoBehaviourType.GetProperties(monoBindingFlags)).ToArray();
            
            Component[] components = coherenceSync.gameObject.GetComponents(typeof(Component));

            foreach (Component myComp in components)
            {
                Type compType = myComp.GetType();

                if (compType.IsSubclassOf(typeof(Renderer)) || compType == typeof(Renderer)) continue;
                if (compType.IsSubclassOf(typeof(MeshFilter)) || compType == typeof(MeshFilter)) continue;
                if (compType.IsSubclassOf(typeof(CoherenceSync)) || compType == typeof(CoherenceSync)) continue;
                if (compType.IsSubclassOf(typeof(Collider)) || compType == typeof(Collider)) continue;
                if (compType.IsSubclassOf(typeof(Rigidbody)) || compType == typeof(Rigidbody)) continue;
                if (compType.IsSubclassOf(typeof(Rigidbody2D)) || compType == typeof(Rigidbody2D)) continue;
                if (compType.IsSubclassOf(typeof(Collider)) || compType == typeof(Collider)) continue;
                if (compType.IsSubclassOf(typeof(Collider2D)) || compType == typeof(Collider2D)) continue;
               // EditorGUILayout.Space();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(compType.ToString(), EditorStyles.boldLabel);

                var compTypeString = compType.AssemblyQualifiedName;
                var prevTypeIncluded = coherenceSync.GetScriptToggle(compTypeString);
                bool compTypeIncluded = EditorGUILayout.Toggle( prevTypeIncluded ?? false );

                if (compTypeIncluded != prevTypeIncluded)
                {
                    anyChangesMade = true;
                    coherenceSync.SetScriptToggle(compTypeString, compTypeIncluded);
                }
                
                EditorGUILayout.EndHorizontal();
                
                if (!compTypeIncluded) continue;
                
                const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
                MemberInfo[] members = compType.GetFields(bindingFlags).Cast<MemberInfo>()
                    .Concat(compType.GetProperties(bindingFlags)).ToArray();
                
                foreach (var variable in members)
                {
                    if (compType == typeof(UnityEngine.Transform))
                    {
                        if (variable.Name != "rotation" && variable.Name != "position") continue;
                    }
                    
                    bool doProceed = true;
                    // Remove those inherited from MonoBehaviour (de-clutter)
                    foreach (var monoVariable in monoMembers)
                    {
                        if (variable.Name == monoVariable.Name)
                        {
                            doProceed = false;
                            break;
                        }
                    }

                    if (!doProceed) continue;

                    Type fieldType = GetUnderlyingType(variable);
                    
                    if (!IsTypeSupported(fieldType)) continue;
                    
                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.indentLevel++;
                    try
                    {
                        var varString = compTypeString + CoherenceSync.KeyDelimiter + variable.Name;

                        EditorGUILayout.LabelField($"{variable.Name} [{fieldType}]");
                        var prevVarIncluded = coherenceSync.GetFieldToggle(varString);
                        bool varIncluded = EditorGUILayout.Toggle( prevVarIncluded ?? false );

                        if (varIncluded != prevVarIncluded)
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
                    EditorGUILayout.EndHorizontal();
                }
            }

            if (anyChangesMade)
            {
                Undo.RecordObject(target, "Changed selected scripts");
                EditorUtility.SetDirty(target);
            }
        }
    }
}