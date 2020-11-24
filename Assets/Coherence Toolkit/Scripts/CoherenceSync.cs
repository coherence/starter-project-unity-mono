namespace Coherence.MonoBridge
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using Coherence.Generated.FirstProject;
    using Coherence.Replication.Client.Unity.Ecs;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Transforms;
    using UnityEngine;

    public class CoherenceSync : MonoBehaviour
    {
        public static List<CoherenceSync> instances = new List<CoherenceSync>();

        public delegate void NetworkCommandHandler(object sender, GenericNetworkCommandArgs e);

        public event NetworkCommandHandler NetworkCommandReceived;

        public enum SynchronizedPrefabOptions
        {
            This = 0,
            Other = 1
        }

        protected const int MaxNetworkStringLength = 32;

        [NonSerialized] public const string KeyDelimiter = "*";

        private CoherenceMonoBridge coherenceMonoBridge;

        private bool ecsEntitySet;

        private Entity entity;

        private EntityManager entityManager;

        public delegate void GenericCommandRequestDelegate(object[] args);

        Dictionary<string, GenericCommandRequestDelegate> commandRequestDelegates =
            new Dictionary<string, GenericCommandRequestDelegate>();

        // Unfortunately Unity won't serialize Hash tables so we're doing this with double lists :/

        [SerializeField] private List<string> enabledScriptTogglesKeys = new List<string>();
        [SerializeField] private List<bool> enabledScriptTogglesValues = new List<bool>();

        [SerializeField] private List<string> fieldLinksKeys = new List<string>();
        [SerializeField] private List<string> fieldLinksValues = new List<string>();

        [SerializeField] private List<string> fieldTogglesKeys = new List<string>();
        [SerializeField] private List<bool> fieldTogglesValues = new List<bool>();

        [SerializeField] private List<string> fieldTypesKeys = new List<string>();
        [SerializeField] private List<string> fieldTypesValues = new List<string>();

        [SerializeField] private int genericFieldCounter_Float;
        [SerializeField] private int genericFieldCounter_Int;
        [SerializeField] private int genericFieldCounter_Quaternion;
        [SerializeField] private int genericFieldCounter_String;
        [SerializeField] private int genericFieldCounter_Vector;

        [SerializeField] private List<string> scriptTogglesKeys = new List<string>();
        [SerializeField] private List<bool> scriptTogglesValues = new List<bool>();

        [NonSerialized] public bool isSimulated = true;

        [SerializeField] public string remoteVersionPrefabName;
        [NonSerialized] private string schemaNamespace = "Coherence.Generated.FirstProject.";

        [SerializeField]
        protected SynchronizedPrefabOptions selectedSynchronizedPrefabOption = SynchronizedPrefabOptions.This;

        [SerializeField]
        public bool usingReflection = true;

        public SynchronizedPrefabOptions SelectedSynchronizedPrefabOption
        {
            get => selectedSynchronizedPrefabOption;
            set => selectedSynchronizedPrefabOption = value;
        }

        public Entity LinkedEntity
        {
            set
            {
                entity = value;
                if (entity != Entity.Null) ecsEntitySet = true;
            }
            get => entity;
        }

        public void Reset()
        {
            genericFieldCounter_Float = 0;
            genericFieldCounter_Int = 0;
            genericFieldCounter_Quaternion = 0;
            genericFieldCounter_String = 0;
            genericFieldCounter_Vector = 0;
            remoteVersionPrefabName = null;
            enabledScriptTogglesKeys = new List<string>();
            enabledScriptTogglesValues = new List<bool>();
            scriptTogglesKeys = new List<string>();
            scriptTogglesValues = new List<bool>();
            fieldTogglesKeys = new List<string>();
            fieldTogglesValues = new List<bool>();
            fieldTypesKeys = new List<string>();
            fieldTypesValues = new List<string>();
            fieldLinksKeys = new List<string>();
            fieldLinksValues = new List<string>();
        }

        private void OnEnable()
        {
            instances.Add(this);
        }

        private void OnDisable()
        {
            _ = instances.Remove(this);
        }

        protected IEnumerator Start()
        {
            if (entity != Entity.Null || !isSimulated) yield break;

            CreateECSRepresentation();
        }

        private void CreateECSRepresentation()
        {
            entity = entityManager.CreateEntity();
            entityManager.AddComponent<GenericPrefabReference>(entity);

            if (usingReflection)
            {
                InitializeComponents();
            }

            entityManager.SetComponentData(entity, new GenericPrefabReference
            {
                prefab = new FixedString64(remoteVersionPrefabName)
            });
        }

        private void InitializeComponents()
        {
            entityManager.AddComponentData<Translation>(entity, new Translation { Value = transform.position });
            entityManager.AddComponentData<Rotation>(entity, new Rotation { Value = transform.rotation });
            entityManager.AddComponentData<GenericScale>(entity, new GenericScale { Value = transform.localScale });

            entityManager.AddComponent<SessionBased>(entity);
            entityManager.AddComponent<Simulated>(entity);
            entityManager.AddComponent<GenericCommand>(entity);

            foreach (var t in fieldLinksValues)
            {
                var val = t;
                if (val == null) continue;

                var type = Type.GetType(schemaNamespace + val);
                if (type == null)
                {
                    Debug.LogWarning($"Type {t} could not be found.");
                    continue;
                }

                _ = entityManager.AddComponent(entity, type);
            }
        }

        protected void Awake()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            coherenceMonoBridge = FindObjectOfType<CoherenceMonoBridge>();

            if (coherenceMonoBridge != null) schemaNamespace = coherenceMonoBridge.schemaNamespace;

            Coherence.Network.OnConnected += OnConnected;
            Coherence.Network.OnDisconnected += OnDisconnected;
        }

        private void OnConnected()
        {
            if (entity == Entity.Null)
            {
                CreateECSRepresentation();
            }
        }

        private void OnDisconnected()
        {
            entity = Entity.Null;
        }

        protected void OnDestroy()
        {
            Coherence.Network.OnConnected -= OnConnected;
            Coherence.Network.OnDisconnected -= OnDisconnected;

        }

        public void SpawnFromNetwork(Entity linkedEntity, string name)
        {
            isSimulated = false;
            LinkedEntity = linkedEntity;
            gameObject.name = name;

            EnableAndDisableScripts();
        }

        private void EnableAndDisableScripts()
        {
            for (var i = 0; i < enabledScriptTogglesKeys.Count; i++)
            {
                var script = enabledScriptTogglesKeys[i];
                var en = enabledScriptTogglesValues[i];

                var scriptType = Type.GetType(script);

                if (scriptType == null) continue;

                var cmp = gameObject.GetComponent(scriptType);

                try
                {
                    if (cmp != null && cmp is Behaviour)
                    {
                        ((Behaviour)cmp).enabled = en;
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("EnableAndDisableScripts failed: " + cmp + " " + e.Message);
                }
            }
        }

        public string GetDebugData()
        {
            var debugData = string.Empty;

            debugData += $"({fieldLinksValues.Count}) ";
            if (fieldLinksKeys.Count > 0) debugData += $"({fieldLinksKeys[0]}) ";

            if (fieldLinksValues.Count > 0) debugData += $"({fieldLinksValues[0]}) ";

            return debugData;
        }

        public void SendCommand(CoherenceSync sender, string commandName, params object[] args)
        {
            if (entity == Entity.Null)
            {
                Debug.LogError($"entity is null for game object '{name}'");
                return;
            }

            if (entityManager.HasComponent<GenericCommandRequest>(entity))
            {
                // Remote Entity
                if(usingReflection)
                {
                    var commandRequest = GenericCommandRequestFromObjects(commandName, args);
                    var cmdRequestBuffer = entityManager.GetBuffer<GenericCommandRequest>(entity);
                    _ = cmdRequestBuffer.Add(commandRequest);
                }
                else
                {
                    commandRequestDelegates[commandName](args);
                }
            }
            else {
                // Local Entity
                var command = GenericNetworkCommandArgs.FromObjects(commandName, args);
                ProcessGenericNetworkCommand(commandName, command);
            }
        }

        private void ReceiveGenericNetworkCommands()
        {
            if (entity == Entity.Null) return;

            var buffer = entityManager.GetBuffer<GenericCommand>(entity);

            foreach (var genericCommand in buffer)
            {
                var commandName = genericCommand.name.ToString();
                ProcessGenericNetworkCommand(commandName, GenericNetworkCommandArgs.FromGenericCommand(genericCommand));
            }

            buffer.Clear();
        }

        private Dictionary<string, (MethodInfo, Component)> commandNameMemoization =
            new Dictionary<string, (MethodInfo, Component)>();

        private void ProcessGenericNetworkCommand(string commandName, GenericNetworkCommandArgs genericNetworkCommandArgs)
        {
            NetworkCommandReceived?.Invoke(this, genericNetworkCommandArgs);

            MethodInfo method;
            Component receiver;

            if(commandNameMemoization.TryGetValue(commandName, out (MethodInfo, Component) memoized))
            {
                method = memoized.Item1;
                receiver = memoized.Item2;
            }
            else
            {
                (method, receiver) = TypeHelpers.GetMethodAndReciever(gameObject, commandName);
                commandNameMemoization[commandName] = (method, receiver);
            }

            var methodArgs = genericNetworkCommandArgs.ArgListForMethod(method);
            method.Invoke(receiver, methodArgs);
        }

        public void AddCommandRequestDelegate(string name, GenericCommandRequestDelegate genericCommandRequestDelegate)
        {
            commandRequestDelegates[name] = genericCommandRequestDelegate;
        }

        private static GenericCommandRequest GenericCommandRequestFromObjects(string commandName, object[] args)
        {
            var (paramInt, paramFloat, paramString) = TypeHelpers.ExtractTypedArraysFromObjects(args);

            var genericCommandRequest = new GenericCommandRequest
            {
                name = String.IsNullOrEmpty(commandName) ? "" : commandName,

                paramInt1 = paramInt[0],
                paramInt2 = paramInt[1],
                paramInt3 = paramInt[2],
                paramInt4 = paramInt[3],

                paramFloat1 = paramFloat[0],
                paramFloat2 = paramFloat[1],
                paramFloat3 = paramFloat[2],
                paramFloat4 = paramFloat[3],

                paramString = String.IsNullOrEmpty(paramString[0]) ? "" : paramString[0],
            };

            return genericCommandRequest;
        }

        private bool CmpType(Type type, Type a)
        {
            return type != null && a != null && (type == a || type.IsSubclassOf(a));
        }

        private string TypeToGenericNetworkedFieldName(Type type)
        {
            if (CmpType(type, typeof(Vector3))) return "Vector";

            if (CmpType(type, typeof(float))) return "Float";

            if (CmpType(type, typeof(int))) return "Int";

            if (CmpType(type, typeof(uint))) return "Int";

            if (CmpType(type, typeof(bool))) return "Int";

            if (CmpType(type, typeof(string))) return "String";

            if (CmpType(type, typeof(Quaternion))) return "Quaternion";

            return null;
        }

        private string GetNextAvailableGenericNetworkedField(Type type)
        {
            if (CmpType(type, typeof(Vector3))) return "GenericFieldVector" + genericFieldCounter_Vector++;

            if (CmpType(type, typeof(float))) return "GenericFieldFloat" + genericFieldCounter_Float++;

            if (CmpType(type, typeof(int)) || CmpType(type, typeof(uint)) || CmpType(type, typeof(bool)) || CmpType(type, typeof(Boolean)))
                return "GenericFieldInt" + genericFieldCounter_Int++;

            if (CmpType(type, typeof(string))) return "GenericFieldString" + genericFieldCounter_String++;

            if (CmpType(type, typeof(Quaternion))) return "GenericFieldQuaternion" + genericFieldCounter_Quaternion++;

            return null;
        }

        protected void Update()
        {
            if (!isSimulated && !EcsEntityExists())
            {
                if (ecsEntitySet)
                {
                    coherenceMonoBridge.DestroyLocalObject(this);
                }

                return;
            }

            if (Application.isPlaying && entity != Entity.Null)
            {
                if (usingReflection)
                {
                    if (coherenceMonoBridge.isConnected)
                    {
                        ReceiveGenericNetworkCommands();
                        SyncEcsWithReflection();
                    }
                }
            }
        }

        // Note: must be public
        public bool EcsEntityExists()
        {
            return entity != Entity.Null && entityManager.HasComponent<GenericPrefabReference>(entity);
        }

        private object GetAnimatorValue(Animator an, string _name)
        {
            foreach (var param in an.parameters)
            {
                if (param.name == _name)
                {
                    switch (param.type)
                    {
                        case AnimatorControllerParameterType.Bool:
                            return an.GetBool(_name) ? 1 : 0;
                        case AnimatorControllerParameterType.Float:
                            return an.GetFloat(_name);
                        case AnimatorControllerParameterType.Int:
                            return an.GetInteger(_name);
                    }
                }
            }

            return null;
        }

        private void SetAnimatorValue(Animator an, string _name, object val)
        {
            foreach (var param in an.parameters)
            {
                if (param.name == _name)
                {
                    Debug.Log("SetAnimatorValue " + _name + " " + param.type + " " + val);

                    switch (param.type)
                    {
                        case AnimatorControllerParameterType.Bool:
                            int v = (int)val;
                            bool b = v == 1;

                            an.SetBool(_name, b);
                            break;
                        case AnimatorControllerParameterType.Float:
                            an.SetFloat(_name, (float)val);
                            break;
                        case AnimatorControllerParameterType.Int:
                            an.SetInteger(_name, (int)val);
                            break;
                    }
                }
            }
        }

        public uint CountSyncFieldsTurnedOn()
        {
            uint num = 0;
            for (var i = 0; i < fieldTogglesKeys.Count; i++)
            {
                if (fieldTogglesValues[i])
                {
                    num++;
                }
            }

            return num;
        }

        public void TestAndFixSyncReferenceData()
        {
            for (var i = 0; i < fieldTogglesKeys.Count; i++)
            {
                var on = fieldTogglesValues[i];

                if (!on) continue;

                var linkVal = fieldLinksValues[i];

                if (!string.IsNullOrEmpty(linkVal))
                {
                    var linkKey = fieldLinksKeys[i];
                    var script = GetComponentFromKey(linkKey);
                    var toggleKey = fieldTogglesKeys[i];

                    var networkedType = GetFieldInformation(linkKey, linkVal, toggleKey, out var fieldType, ref script, out var fieldName, out var cmp, out var field, out var property);

                    object currentMonoValue = null;

                    try
                    {
                        if (field != null)
                        {
                            currentMonoValue = field.GetValue(cmp);
                        }
                        else
                        {
                            currentMonoValue = CmpType(script, typeof(Animator)) ? GetAnimatorValue((Animator) cmp, fieldName) : property.GetValue(cmp);
                        }
                    }
                    catch (Exception)
                    {
                        Debug.Log($"DEBUG: Turning sync field off due to lost reference (renamed source field?): {fieldTogglesKeys[i]}");
                        fieldTogglesValues[i] = false;
                    }
                }
            }
        }

        private void SyncEcsWithReflection()
        {
            if(fieldTogglesKeys.Count != fieldTogglesValues.Count ||
               fieldLinksKeys.Count != fieldTogglesValues.Count ||
               fieldLinksValues.Count != fieldTogglesValues.Count)
            {
                Debug.LogError($"Count mismatch! fieldTogglesKeys.Count = {fieldTogglesKeys.Count}, fieldTogglesValues.Count = {fieldTogglesValues.Count}, fieldLinksKeys.Count = {fieldLinksKeys.Count}, fieldLinksValues.Count = {fieldLinksValues.Count}");
                return;
            }

            for (var i = 0; i < fieldTogglesKeys.Count; i++)
            {
                var on = fieldTogglesValues[i];

                if (!on) continue;

                var linkVal = fieldLinksValues[i];

                if (!string.IsNullOrEmpty(linkVal))
                {
                    var linkKey = fieldLinksKeys[i];
                    var script = GetComponentFromKey(linkKey);
                    var toggleKey = fieldTogglesKeys[i];

                    var networkedType = GetFieldInformation(linkKey, linkVal, toggleKey, out var fieldType, ref script, out var fieldName, out var cmp, out var field, out var property);

                    object currentMonoValue = null;

                    try
                    {
                        if (field != null)
                        {
                            currentMonoValue = field.GetValue(cmp);
                        }
                        else
                        {
                            currentMonoValue = CmpType(script, typeof(Animator)) ? GetAnimatorValue((Animator) cmp, fieldName) : property.GetValue(cmp);
                        }
                    }
                    catch (Exception)
                    {
                        Debug.LogWarning($"SyncEcsWithReflection: Failed to sync, hence turning field off (can happen when renaming referenced fields): {fieldTogglesKeys[i]}");
                        fieldTogglesValues[i] = false;
                        continue;
                    }

                    if (isSimulated)
                    {
                        ReflectionSyncMonoToECSNetwork(networkedType, fieldType, currentMonoValue, script);
                    }
                    else
                    {
                        ReflectionSyncECSNetworkToMono(networkedType, fieldType, fieldName, field, cmp, property, script);
                    }
                }
            }
        }

        private Type GetFieldInformation(string linkKey, string linkVal, string toggleKey, out Type fieldType, ref Type script,
            out string fieldName, out Component cmp, out FieldInfo field, out PropertyInfo property)
        {
            var fieldTypeString = GetFieldType(linkKey);
            var networkedType = Type.GetType(schemaNamespace + linkVal);
            fieldType = Type.GetType(fieldTypeString);

            if (fieldType == null)
            {
                if (toggleKey.Contains("position"))
                {
                    fieldType = typeof(Vector3);
                    networkedType = typeof(Translation);
                }

                if (toggleKey.Contains("rotation"))
                {
                    fieldType = typeof(Quaternion);
                    networkedType = typeof(Rotation);
                }

                if (toggleKey.Contains("localScale"))
                {
                    fieldType = typeof(Vector3);
                    networkedType = typeof(GenericScale);
                }
            }

            if (script == null) script = typeof(Transform);

            fieldName = GetFieldNameFromKey(linkKey);

            cmp = script == null
                ? gameObject.GetComponent(typeof(Transform))
                : gameObject.GetComponent(script);

            field = script.GetField(fieldName);
            property = script.GetProperty(fieldName);
            return networkedType;
        }

        private void SetMonoValue(FieldInfo field, PropertyInfo property, object cmp, object val)
        {
            if (field != null)
                field.SetValue(cmp, val);
            else if (property != null) property.SetValue(cmp, val);
        }

        private void ReflectionSyncECSNetworkToMono(Type networkedType, Type fieldType, string fieldName, FieldInfo field, Component cmp,
            PropertyInfo property, Type script)
        {
            var method = typeof(EntityManager).GetMethod("GetComponentData");
            var generic = method.MakeGenericMethod(networkedType);

            var inst = generic.Invoke(entityManager, new object[] { entity });

            if (CmpType(script, typeof(Animator)))
            {
                var fx = networkedType.GetField("number");
                var vfx = fx.GetValue(inst);
                SetAnimatorValue((Animator)cmp, fieldName, vfx);
                return;
            }

            if (CmpType(fieldType, typeof(Vector3)))
            {
                var fx = networkedType.GetField("Value");
                var vfx = (float3)fx.GetValue(inst);

                SetMonoValue(field, property, cmp, new Vector3(vfx.x, vfx.y, vfx.z));
            }

            if (CmpType(fieldType, typeof(float)))
            {
                var fx = networkedType.GetField("number");
                var vfx = (float)fx.GetValue(inst);

                SetMonoValue(field, property, cmp, vfx);
            }

            if (CmpType(fieldType, typeof(int)) || CmpType(fieldType, typeof(uint)) || CmpType(fieldType, typeof(bool)) || CmpType(fieldType, typeof(Boolean)))
            {
                var fx = networkedType.GetField("number");
                var vfx = (int)fx.GetValue(inst);

                SetMonoValue(field, property, cmp, vfx);
            }

            if (CmpType(fieldType, typeof(string)))
            {
                var fx = networkedType.GetField("name");
                var vfx = (FixedString64)fx.GetValue(inst);

                SetMonoValue(field, property, cmp, vfx.ToString());
            }

            if (CmpType(fieldType, typeof(Quaternion)))
            {
                var fx = networkedType.GetField("Value");
                var quat = (quaternion)fx.GetValue(inst);

                SetMonoValue(field, property, cmp, new Quaternion(quat.value.x, quat.value.y, quat.value.z, quat.value.w));
            }
        }

        private void ReflectionSyncMonoToECSNetwork(Type networkedType, Type fieldType, object currentMonoValue, Type script)
        {
            var inst = Activator.CreateInstance(networkedType);

            var method = typeof(EntityManager).GetMethod("SetComponentData");
            var generic = method.MakeGenericMethod(networkedType);

            if (CmpType(fieldType, typeof(Vector3)))
            {
                var fieldX = networkedType.GetField("Value");
                var val = (Vector3) currentMonoValue;
                fieldX.SetValue(inst, new float3(val.x, val.y, val.z));
            }

            if (CmpType(fieldType, typeof(float)))
            {
                var f = networkedType.GetField("number");
                var val = (float) currentMonoValue;
                f.SetValue(inst, val);
            }

            if (CmpType(fieldType, typeof(int)) || CmpType(fieldType, typeof(uint)) ||
                CmpType(fieldType, typeof(bool)) || CmpType(fieldType, typeof(Boolean)))
            {
                var f = networkedType.GetField("number");
                var val = (int) currentMonoValue;
                f.SetValue(inst, val);
            }

            if (CmpType(fieldType, typeof(string)))
            {
                var f = networkedType.GetField("name");
                FixedString64 val = (string) currentMonoValue;
                f.SetValue(inst, val);
            }

            if (CmpType(fieldType, typeof(Quaternion)))
            {
                var fieldX = networkedType.GetField("Value");
                var val = (Quaternion) currentMonoValue;
                fieldX.SetValue(inst, new quaternion(val.x, val.y, val.z, val.w));
            }

            try
            {
                _ = generic.Invoke(entityManager, new[] { entity, inst });
            }
            catch (Exception e)
            {
                Debug.LogWarning($"{entity} {inst} {e.Message}");
                throw e;
            }
        }

        #region ListManipulation

        private static void SetListValue<T>(List<string> keyList, List<T> valList, string key, T val)
        {
            for (var i = 0; i < keyList.Count; i++)
            {
                if (keyList[i] == key)
                {
                    valList[i] = val;
                    return;
                }
            }

            keyList.Add(key);
            valList.Add(val);
        }

        private T GetListValue<T>(List<string> keyList, List<T> valList, string key)
        {
            for (var i = 0; i < keyList.Count; i++)
            {
                if (keyList[i] == key)
                {
                    return valList[i];
                }
            }

            return default(T);
        }

        public bool? GetFieldToggle(string key)
        {
            return GetListValue(fieldTogglesKeys, fieldTogglesValues, key);
        }

        public bool? GetScriptToggle(string key)
        {
            return GetListValue(scriptTogglesKeys, scriptTogglesValues, key);
        }

        public bool? GetEnabledScriptToggle(string key)
        {
            return GetListValue(enabledScriptTogglesKeys, enabledScriptTogglesValues, key);
        }

        public void SetEnabledScriptToggle(string key, bool val)
        {
            SetListValue(enabledScriptTogglesKeys, enabledScriptTogglesValues, key, val);
        }

        private string GetFieldType(string key)
        {
            return GetListValue(fieldTypesKeys, fieldTypesValues, key).Replace("UnityEngine.", "");
        }

        public void SetFieldToggle(string key, bool val)
        {
            SetListValue(fieldTogglesKeys, fieldTogglesValues, key, val);
        }

        public void ToggleFieldSync(string key, Type val, bool on)
        {
            SetListValue(fieldTypesKeys, fieldTypesValues, key, val.AssemblyQualifiedName);

            if (GetListValue(fieldLinksKeys, fieldLinksValues, key) == null)
            {
                SetListValue(fieldLinksKeys, fieldLinksValues, key, GetNextAvailableGenericNetworkedField(val));
            }
        }

        public void ToggleFieldSync(string key, MethodInfo val, bool on)
        {
            SetListValue(fieldTypesKeys, fieldTypesValues, key, TypeHelpers.MethodAsString(val));

            if (GetListValue(fieldLinksKeys, fieldLinksValues, key) == null)
            {
                SetListValue(fieldLinksKeys, fieldLinksValues, key, TypeHelpers.NamespacedMethodName(val));
            }
        }

        public void SetScriptToggle(string key, bool val)
        {
            SetListValue(scriptTogglesKeys, scriptTogglesValues, key, val);
        }

        private Type GetComponentFromKey(string key)
        {
            var i = key.IndexOf(KeyDelimiter, StringComparison.Ordinal);
            var cmp = key.Substring(0, i);

            var type = Type.GetType(cmp);
            return type;
        }

        private string GetFieldNameFromKey(string key)
        {
            var i = key.IndexOf(KeyDelimiter, StringComparison.Ordinal);
            var cmp = key.Substring(i + 1);
            return cmp;
        }

        #endregion
    }
}
