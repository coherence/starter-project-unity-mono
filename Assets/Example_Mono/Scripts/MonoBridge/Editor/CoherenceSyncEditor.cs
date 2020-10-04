using System;
using System.Collections;
using System.Linq;
using Coherence.MonoBridge;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Reflection;

namespace Coherence.MonoBridge
{
    [CustomEditor(typeof(CoherenceSync))]
    [CanEditMultipleObjects]
    public class CoherenceSyncEditor : UnityEditor.Editor
    {
        [SerializeField]
        private Hashtable typeToggles;
        
        [SerializeField]
        private Hashtable componentToggles;

        private Type[] supportedTypes =
            {typeof(Vector3), typeof(Quaternion), typeof(float), typeof(int), typeof(uint), typeof(Enum)};

        private Texture2D texture;
        
        void OnEnable()
        {
            typeToggles = new Hashtable();
            componentToggles = new Hashtable();
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
            serializedObject.Update ();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();
            GUI.DrawTexture(new Rect(20, 5, 150, 42), texture, ScaleMode.ScaleToFit);
            
            for(int i=0; i<8; i++)EditorGUILayout.Space();
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Select the components you want to sync over the network:");

            EditorGUI.indentLevel++;
            CycleThroughPublicVariables();
            EditorGUI.indentLevel--;
            
            EditorGUILayout.Space();

            Color prevColor = GUI.color;
            GUI.color = Color.red;
            EditorGUILayout.LabelField("Warning! Using reflection (slow). Bake network components for more performance.");
            GUI.color = prevColor;
            GUILayout.Button("Bake network components");
            
            serializedObject.ApplyModifiedProperties ();
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
                EditorGUILayout.Space();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(compType.ToString(), EditorStyles.boldLabel);
                
                var compTypeString = compType.ToString();
                bool compTypeIncluded = EditorGUILayout.Toggle( typeToggles[compTypeString] != null && (bool)typeToggles[compTypeString]);

                typeToggles[compTypeString] = compTypeIncluded;

                EditorGUILayout.EndHorizontal();
                
                if (!compTypeIncluded) continue;
                
                const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
                MemberInfo[] members = compType.GetFields(bindingFlags).Cast<MemberInfo>()
                    .Concat(compType.GetProperties(bindingFlags)).ToArray();
                
                foreach (var variable in members)
                {
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
                        var varString = compTypeString + "." + variable;

                        EditorGUILayout.LabelField($"{variable.Name} [{fieldType}]");
                        bool varIncluded = EditorGUILayout.Toggle( componentToggles[varString] != null && (bool)componentToggles[varString]);
                        componentToggles[varString] = varIncluded;
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }

                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }
}