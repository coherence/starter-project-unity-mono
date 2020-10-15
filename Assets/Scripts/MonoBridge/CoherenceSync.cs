namespace Coherence.MonoBridge
{
    using Coherence.Generated.FirstProject;
    using Coherence.Replication.Client.Unity.Ecs;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Transforms;
    using UnityEngine;

    public class CoherenceSync : MonoBehaviour
    {
        [NonSerialized] public const string KeyDelimiter = "*";
        [NonSerialized] protected string schemaNamespace = "Coherence.Generated.FirstProject.";

        public enum SynchronizedPrefabOptions
        {
            Self = 0,
            Other = 1
        }

        [SerializeField]
        protected SynchronizedPrefabOptions selectedSynchronizedPrefabOption = SynchronizedPrefabOptions.Self;

        public SynchronizedPrefabOptions SelectedSynchronizedPrefabOption
        {
            get => selectedSynchronizedPrefabOption;
            set => selectedSynchronizedPrefabOption = value;
        }

        [SerializeField] public string remoteVersionPrefabName = null;

        [SerializeField] public bool usingReflection = true;

        protected EntityManager entityManager;

        protected CoherenceBootstrap coherenceBootstrap;

        public bool isSimulated = true;

        protected Entity entity;

        protected bool ecsEntitySet = false;

        public Entity LinkedEntity
        {
            set
            {
                entity = value;
                if (entity != Entity.Null)
                {
                    ecsEntitySet = true;
                }
            }
            get => entity;
        }

        // Unfortunately Unity won't serialize Hash tables so we're doing this with double lists :/
        [SerializeField]
        private List<string> scriptTogglesKeys = new List<string>();

        [SerializeField]
        private List<bool> scriptTogglesValues = new List<bool>();

        [SerializeField]
        private List<string> enabledScriptTogglesKeys = new List<string>();

        [SerializeField]
        private List<bool> enabledScriptTogglesValues = new List<bool>();

        [SerializeField]
        private List<string> fieldTogglesKeys = new List<string>();

        [SerializeField]
        private List<bool> fieldTogglesValues = new List<bool>();

        [SerializeField]
        private List<string> fieldTypesKeys = new List<string>();

        [SerializeField]
        private List<string> fieldTypesValues = new List<string>();

        [SerializeField]
        private List<string> fieldLinksKeys = new List<string>();

        [SerializeField]
        private List<string> fieldLinksValues = new List<string>();

        [SerializeField] private int genericFieldCounter_Vector = 0;

        [SerializeField] private int genericFieldCounter_Float = 0;

        [SerializeField] private int genericFieldCounter_Int = 0;

        [SerializeField] private int genericFieldCounter_String = 0;

        [SerializeField] private int genericFieldCounter_Quaternion = 0;

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

        protected void Awake()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            coherenceBootstrap = FindObjectOfType<CoherenceBootstrap>();

            if (coherenceBootstrap != null)
            {
                schemaNamespace = coherenceBootstrap.schemaNamespace;
            }
        }

        public void SpawnFromNetwork(Entity linkedEntity, string name)
        {
            isSimulated = false;
            LinkedEntity = linkedEntity;
            gameObject.name = name;

            EnableAndDisableScripts();
        }

        protected void EnableAndDisableScripts()
        {
            for (int i = 0; i < enabledScriptTogglesKeys.Count; i++)
            {
                var script = enabledScriptTogglesKeys[i];
                var en = enabledScriptTogglesValues[i];

                var scriptType = Type.GetType(script);

                if (scriptType != null)
                {
                    Component cmp = gameObject.GetComponent(scriptType);
                    try
                    {
                        if (cmp != null)
                        {
                            ((Behaviour)cmp).enabled = en;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Log(cmp.ToString() + " " + e.Message);
                    }
                }
            }
        }

        public string GetDebugData()
        {
            var debugData = string.Empty;

            debugData += $"({fieldLinksValues.Count}) ";
            if (fieldLinksKeys.Count > 0)
            {
                debugData += $"({fieldLinksKeys[0]}) ";
            }

            if (fieldLinksValues.Count > 0)
            {
                debugData += $"({fieldLinksValues[0]}) ";
            }

            return debugData;
        }

        protected IEnumerator Start()
        {
            yield return null;
            if (entity == Entity.Null && isSimulated)
            {
                entity = entityManager.CreateEntity(typeof(Translation), typeof(Rotation), typeof(CoherenceSessionComponent), typeof(CoherenceSimulateComponent), typeof(Player), typeof(GenericCommand));

                foreach (var t in fieldLinksValues)
                {
                    string val = t;
                    if (val == null)
                    {
                        continue;
                    }

                    Type type = Type.GetType(schemaNamespace + val);
                    if (type == null)
                    {
                        Debug.LogWarning($"Type {t} could not be found.");
                        continue;
                    }
                    _ = entityManager.AddComponent(entity, type);
                }

                entityManager.SetComponentData(entity, new Player()
                {
                    prefab = new FixedString64(remoteVersionPrefabName)
                });
            }
        }

        protected void ReceiveNetworkCommands()
        {
            if (entity == Entity.Null)
            {
                return;
            }

            DynamicBuffer<GenericCommand> buffer = entityManager.GetBuffer<GenericCommand>(entity);

            if (buffer.Length > 0)
            {
                Debug.LogWarning("Buffer size " + buffer.Length);
            }

            foreach (GenericCommand cmd in buffer)
            {
                ProcessGenericNetworkCommand(cmd.name.ToString(), cmd.paramInt1, cmd.paramInt2, cmd.paramInt3, cmd.paramInt4,
                    cmd.paramFloat1, cmd.paramFloat2, cmd.paramFloat3, cmd.paramFloat4, cmd.paramString.ToString());
            }

            buffer.Clear();
        }

        public class GenericNetworkCommandArgs : EventArgs
        {
            public string Name { get; set; }
            public string ParamString { get; set; }

            public int ParamInt1 { get; set; }
            public int ParamInt2 { get; set; }
            public int ParamInt3 { get; set; }
            public int ParamInt4 { get; set; }

            public float ParamFloat1 { get; set; }
            public float ParamFloat2 { get; set; }
            public float ParamFloat3 { get; set; }
            public float ParamFloat4 { get; set; }

            public override string ToString()
            {
                return $"{Name} {ParamInt1} {ParamInt2} {ParamInt3} {ParamInt4} {ParamFloat1} {ParamFloat2} {ParamFloat3} {ParamFloat4} {ParamString}";
            }
        }

        public delegate void NetworkCommandHandler(object sender, GenericNetworkCommandArgs e);
        public event NetworkCommandHandler NetworkCommandReceived;
        
        protected void ProcessGenericNetworkCommand(string name, int paramInt1, int paramInt2, int paramInt3, int paramInt4, float paramFloat1, float paramFloat2, float paramFloat3, float paramFloat4, string paramString)
        {
            GenericNetworkCommandArgs args = new GenericNetworkCommandArgs()
            {
                Name = name,
                ParamInt1 = paramInt1,
                ParamInt2 = paramInt2,
                ParamInt3 = paramInt3,
                ParamInt4 = paramInt4,
                ParamFloat1 = paramFloat1,
                ParamFloat2 = paramFloat2,
                ParamFloat3 = paramFloat3,
                ParamFloat4 = paramFloat4,
                ParamString = paramString
            };

            NetworkCommandReceived?.Invoke(this, args);

            gameObject.SendMessage("NetworkCommand", args, SendMessageOptions.DontRequireReceiver);
        }

        public void SendNetworkCommand(CoherenceSync sender, int paramInt1)
        {
            SendNetworkCommand(sender, null, paramInt1);
        }

        public void SendNetworkCommand(CoherenceSync sender, string cmdName, int paramInt1 = 0, int paramInt2 = 0, int paramInt3 = 0, int paramInt4 = 0, float paramFloat1 = 0, float paramFloat2 = 0, float paramFloat3 = 0, float paramFloat4 = 0, string paramString = null)
        {
            if (entity == Entity.Null)
            {
                return;
            }

            const int maxLen = 32;
            cmdName = cmdName?.Substring(0, cmdName.Length > maxLen ? maxLen : cmdName.Length);

            paramString = paramString?.Substring(0, paramString.Length > maxLen ? maxLen : paramString.Length);

            GenericCommandRequest cmd = new GenericCommandRequest
            {
                name = cmdName,
                paramInt1 = paramInt1,
                paramInt2 = paramInt2,
                paramInt3 = paramInt3,
                paramInt4 = paramInt4,

                paramFloat1 = paramFloat1,
                paramFloat2 = paramFloat2,
                paramFloat3 = paramFloat3,
                paramFloat4 = paramFloat4,

                paramString = paramString,
            };

            if (entityManager.HasComponent<GenericCommandRequest>(entity))
            {
                var cmdRequestBuffer = entityManager.GetBuffer<GenericCommandRequest>(entity);
                _ = cmdRequestBuffer.Add(cmd);
            }
            else
            {
                ProcessGenericNetworkCommand(cmd.name.ToString(), cmd.paramInt1, cmd.paramInt2, cmd.paramInt3,
                    cmd.paramInt4, cmd.paramFloat1, cmd.paramFloat2, cmd.paramFloat3, cmd.paramFloat4,
                    cmd.paramString.ToString());
            }
        }

        protected bool CmpType(Type type, Type a)
        {
            return type != null && a != null && (type == a || type.IsSubclassOf(a));
        }

        private string TypeToGenericNetworkedFieldName(Type type)
        {
            if (CmpType(type, typeof(Vector3)))
            {
                return "Vector";
            }

            if (CmpType(type, typeof(float)))
            {
                return "Float";
            }

            if (CmpType(type, typeof(int)))
            {
                return "Int";
            }

            if (CmpType(type, typeof(uint)))
            {
                return "Int";
            }

            if (CmpType(type, typeof(string)))
            {
                return "String";
            }

            if (CmpType(type, typeof(Quaternion)))
            {
                return "Quaternion";
            }

            return null;
        }

        private string GetNextAvailableGenericNetworkedField(Type type)
        {
            if (CmpType(type, typeof(Vector3)))
            {
                return "GenericFieldVector" + genericFieldCounter_Vector++;
            }

            if (CmpType(type, typeof(float)))
            {
                return "GenericFieldFloat" + genericFieldCounter_Float++;
            }

            if (CmpType(type, typeof(int)) || CmpType(type, typeof(uint)))
            {
                return "GenericFieldInt" + genericFieldCounter_Int++;
            }

            if (CmpType(type, typeof(string)))
            {
                return "GenericFieldString" + genericFieldCounter_String++;
            }

            if (CmpType(type, typeof(Quaternion)))
            {
                return "GenericFieldQuaternion" + genericFieldCounter_Quaternion++;
            }

            return null;
        }

        #region ListManipulation
        public void SetListValue(List<string> keyList, List<bool> valList, string key, bool val)
        {
            for (int i = 0; i < keyList.Count; i++)
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

        private void SetListValue(List<string> keyList, List<string> valList, string key, string val)
        {
            for (int i = 0; i < keyList.Count; i++)
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

        private void SetListValue(List<string> keyList, List<string> valList, string key, Type val)
        {
            for (int i = 0; i < keyList.Count; i++)
            {
                if (keyList[i] == key)
                {
                    valList[i] = val.AssemblyQualifiedName;
                    return;
                }
            }

            keyList.Add(key);
            valList.Add(val.ToString());
        }

        private bool? GetListValue(List<string> keyList, List<bool> valList, string key)
        {
            for (int i = 0; i < keyList.Count; i++)
            {
                if (keyList[i] == key)
                {
                    return valList[i];
                }
            }

            return null;
        }

        private string GetListValue(List<string> keyList, List<string> valList, string key)
        {
            for (int i = 0; i < keyList.Count; i++)
            {
                if (keyList[i] == key)
                {
                    return valList[i];
                }
            }

            return null;
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
            SetListValue(fieldTypesKeys, fieldTypesValues, key, val);

            if (GetListValue(fieldLinksKeys, fieldLinksValues, key) == null)
            {
                SetListValue(fieldLinksKeys, fieldLinksValues, key, GetNextAvailableGenericNetworkedField(val));
            }
        }

        public void SetScriptToggle(string key, bool val)
        {
            SetListValue(scriptTogglesKeys, scriptTogglesValues, key, val);
        }

        private Type GetComponentFromKey(string key)
        {
            int i = key.IndexOf(KeyDelimiter, StringComparison.Ordinal);
            string cmp = key.Substring(0, i);

            Type type = Type.GetType(cmp);
            return type;
        }

        private string GetFieldNameFromKey(string key)
        {
            int i = key.IndexOf(KeyDelimiter, StringComparison.Ordinal);
            string cmp = key.Substring(i + 1);
            return cmp;
        }
        
        #endregion

        private void Update()
        {
            if (!isSimulated && !EcsEntityExists())
            {
                if (ecsEntitySet)
                {
                    Destroy(gameObject);
                }
                return;
            }

            if (Application.isPlaying && entity != Entity.Null)
            {
                ReceiveNetworkCommands();

                if (usingReflection)
                {
                    SyncEcsWithReflection();    
                }
            }
        }

        private bool EcsEntityExists()
        {
            return !(entity == Entity.Null || !entityManager.HasComponent<Translation>(entity));
        }

        private void SyncEcsWithReflection()
        {
            for (var i = 0; i < fieldTogglesKeys.Count; i++)
            {
                bool on = fieldTogglesValues[i];
                if (!on)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(fieldLinksValues[i]))
                {
                    var script = GetComponentFromKey(fieldLinksKeys[i]);

                    string fieldTypeString = GetFieldType(fieldLinksKeys[i]);
                    Type networkedType = Type.GetType(schemaNamespace + fieldLinksValues[i]);
                    Type fieldType = Type.GetType(fieldTypeString);

                    if (fieldType == null)
                    {
                        if (fieldTypeString.Contains("Vector3"))
                        {
                            fieldType = typeof(Vector3);
                            networkedType = typeof(Translation);
                        }

                        if (fieldTypeString.Contains("Quaternion"))
                        {
                            fieldType = typeof(Quaternion);
                            networkedType = typeof(Rotation);
                        }
                    }

                    if (script == null)
                    {
                        script = typeof(Transform);
                    }

                    string fieldName = GetFieldNameFromKey(fieldLinksKeys[i]);

                    Component cmp = script == null
                        ? gameObject.GetComponent(typeof(Transform))
                        : gameObject.GetComponent(script);

                    FieldInfo field = script.GetField(fieldName);
                    PropertyInfo property = script.GetProperty(fieldName);

                    object currentMonoValue;

                    if (field != null)
                    {
                        currentMonoValue = field.GetValue(cmp);
                    }
                    else
                    {
                        // Debug.LogWarning($"[{script}] [{fieldTypeString}] [{property}] [{cmp}] [{fieldName}]");
                        currentMonoValue = property.GetValue(cmp);
                    }

                    if (isSimulated)
                    {
                        object inst = Activator.CreateInstance(networkedType);

                        MethodInfo method = typeof(EntityManager).GetMethod("SetComponentData");
                        MethodInfo generic = method.MakeGenericMethod(networkedType);

                        if (CmpType(fieldType, typeof(Vector3)))
                        {
                            FieldInfo fieldX = networkedType.GetField("Value");
                            Vector3 val = (Vector3) currentMonoValue;
                            fieldX.SetValue(inst, new float3(val.x, val.y, val.z));
                        }

                        if (CmpType(fieldType, typeof(float)))
                        {
                            FieldInfo f = networkedType.GetField("number");
                            float val = (float) currentMonoValue;
                            f.SetValue(inst, val);
                        }

                        if (CmpType(fieldType, typeof(int)) || CmpType(script, typeof(uint)))
                        {
                            FieldInfo f = networkedType.GetField("number");
                            int val = (int) currentMonoValue;
                            f.SetValue(inst, val);
                        }

                        if (CmpType(fieldType, typeof(string)))
                        {
                            FieldInfo f = networkedType.GetField("name");
                            FixedString64 val = (string) currentMonoValue;
                            f.SetValue(inst, val);
                        }

                        if (CmpType(fieldType, typeof(Quaternion)))
                        {
                            FieldInfo fieldX = networkedType.GetField("Value");
                            Quaternion val = (Quaternion) currentMonoValue;
                            fieldX.SetValue(inst, new quaternion(val.x, val.y, val.z, val.w));
                        }

                        try
                        {
                            _ = generic.Invoke(entityManager, new object[] {entity, inst});
                        }
                        catch (Exception e)
                        {
                            Debug.LogWarning($"{entity} {inst} {e.Message}");
                            throw e;
                        }
                    }
                    else
                    {
                        MethodInfo method = typeof(EntityManager).GetMethod("GetComponentData");
                        MethodInfo generic = method.MakeGenericMethod(networkedType);

                        object inst = generic.Invoke(entityManager, new object[] {entity});

                        if (CmpType(fieldType, typeof(Vector3)))
                        {
                            FieldInfo fx = networkedType.GetField("Value");
                            float3 vfx = (float3) fx.GetValue(inst);

                            if (field != null)
                            {
                                field.SetValue(cmp, new Vector3(vfx.x, vfx.y, vfx.z));
                            }
                            else if (property != null)
                            {
                                property.SetValue(cmp, new Vector3(vfx.x, vfx.y, vfx.z));
                            }
                        }

                        if (CmpType(fieldType, typeof(float)))
                        {
                            FieldInfo fx = networkedType.GetField("number");
                            float vfx = (float) fx.GetValue(inst);

                            if (field != null)
                            {
                                field.SetValue(cmp, vfx);
                            }
                            else if (property != null)
                            {
                                property.SetValue(cmp, vfx);
                            }
                        }

                        if (CmpType(fieldType, typeof(int)) || CmpType(script, typeof(uint)))
                        {
                            FieldInfo fx = networkedType.GetField("number");
                            int vfx = (int) fx.GetValue(inst);

                            if (field != null)
                            {
                                field.SetValue(cmp, vfx);
                            }
                            else if (property != null)
                            {
                                property.SetValue(cmp, vfx);
                            }
                        }

                        if (CmpType(fieldType, typeof(string)))
                        {
                            FieldInfo fx = networkedType.GetField("name");
                            FixedString64 vfx = (FixedString64) fx.GetValue(inst);

                            if (field != null)
                            {
                                field.SetValue(cmp, vfx.ToString());
                            }
                            else if (property != null)
                            {
                                property.SetValue(cmp, vfx.ToString());
                            }
                        }

                        if (CmpType(fieldType, typeof(Quaternion)))
                        {
                            FieldInfo fx = networkedType.GetField("Value");
                            quaternion quat = (quaternion) fx.GetValue(inst);

                            if (field != null)
                            {
                                field.SetValue(cmp, new Quaternion(quat.value.x, quat.value.y, quat.value.z, quat.value.w));
                            }
                            else if (property != null)
                            {
                                property.SetValue(cmp, new Quaternion(quat.value.x, quat.value.y, quat.value.z, quat.value.w));
                            }
                        }
                    }
                }
            }
        }
    }
}