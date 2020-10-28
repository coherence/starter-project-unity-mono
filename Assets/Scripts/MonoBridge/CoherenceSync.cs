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

namespace Coherence.MonoBridge
{
    public class CoherenceSync : MonoBehaviour
    {
        public delegate void NetworkCommandHandler(object sender, GenericNetworkCommandArgs e);

        public event NetworkCommandHandler NetworkCommandReceived;

        public enum SynchronizedPrefabOptions
        {
            This = 0,
            Other = 1
        }

        protected const int maxNetworkStringLength = 32;

        [NonSerialized] public const string KeyDelimiter = "*";

        private CoherenceBootstrap coherenceBootstrap;

        private bool ecsEntitySet;

        [SerializeField] private List<string> enabledScriptTogglesKeys = new List<string>();

        [SerializeField] private List<bool> enabledScriptTogglesValues = new List<bool>();

        protected Entity entity;

        protected EntityManager entityManager;

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

        public bool isSimulated = true;

        [SerializeField] public string remoteVersionPrefabName;
        [NonSerialized] private string schemaNamespace = "Coherence.Generated.FirstProject.";

        // Unfortunately Unity won't serialize Hash tables so we're doing this with double lists :/
        [SerializeField] private List<string> scriptTogglesKeys = new List<string>();

        [SerializeField] private List<bool> scriptTogglesValues = new List<bool>();

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

        protected IEnumerator Start()
        {
            yield return null;
            if (entity != Entity.Null || !isSimulated) yield break;

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

        protected void InitializeComponents()
        {
            entityManager.AddComponent<Translation>(entity);
            entityManager.AddComponent<Rotation>(entity);
            entityManager.AddComponent<CoherenceSessionComponent>(entity);
            entityManager.AddComponent<CoherenceSimulateComponent>(entity);
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

            coherenceBootstrap = FindObjectOfType<CoherenceBootstrap>();

            if (coherenceBootstrap != null) schemaNamespace = coherenceBootstrap.schemaNamespace;
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
                
                Debug.Log("Script type: " + scriptType);
                
                try
                {
                    if (cmp != null && cmp is Behaviour)
                    {
                        //Debug.Log("Changing state: " + en);
                        ((Behaviour) cmp).enabled = en;
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("EnableAndDisableScripts failed// " + cmp + " " + e.Message);
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

        private void ReceiveGenericNetworkCommands()
        {
            if (entity == Entity.Null) return;

            var buffer = entityManager.GetBuffer<GenericCommand>(entity);

            foreach (var cmd in buffer)
                ProcessGenericNetworkCommand(cmd.name.ToString(), cmd.paramInt1, cmd.paramInt2, cmd.paramInt3,
                    cmd.paramInt4, cmd.paramFloat1, cmd.paramFloat2, cmd.paramFloat3, cmd.paramFloat4, cmd.paramString.ToString());

            buffer.Clear();
        }

        private void ProcessGenericNetworkCommand(string name, int paramInt1, int paramInt2, int paramInt3,
            int paramInt4, float paramFloat1, float paramFloat2, float paramFloat3, float paramFloat4,
            string paramString)
        {
            var args = new GenericNetworkCommandArgs
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

        public void SendNetworkCommand(CoherenceSync sender, string cmdName, int paramInt1 = 0, int paramInt2 = 0,
            int paramInt3 = 0, int paramInt4 = 0, float paramFloat1 = 0, float paramFloat2 = 0, float paramFloat3 = 0,
            float paramFloat4 = 0, string paramString = null)
        {
            if (entity == Entity.Null) return;

            cmdName = TrimString64(cmdName);

            paramString = TrimString64(paramString);

            var cmd = new GenericCommandRequest
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

                paramString = paramString
            };

            if (entityManager.HasComponent<GenericCommandRequest>(entity))
            {
                var cmdRequestBuffer = entityManager.GetBuffer<GenericCommandRequest>(entity);
                _ = cmdRequestBuffer.Add(cmd);
            }
            else
            {
                ProcessGenericNetworkCommand(cmd.name.ToString(), cmd.paramInt1, cmd.paramInt2, cmd.paramInt3, cmd.paramInt4, cmd.paramFloat1, cmd.paramFloat2, cmd.paramFloat3, cmd.paramFloat4, cmd.paramString.ToString());
            }
        }

        public static string TrimString64(string cmdName)
        {
            if (cmdName == null) return cmdName;
            
            return cmdName.Length > maxNetworkStringLength ? cmdName.Substring(0, maxNetworkStringLength) : cmdName;
        }

        protected bool CmpType(Type type, Type a)
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
            if (Input.GetKeyDown(KeyCode.M))
            {
                coherenceBootstrap.SpawnEntity(entity);
            }
            
            if (!isSimulated && !EcsEntityExists())
            {
                if (ecsEntitySet) Destroy(gameObject);

                return;
            }

            if (Application.isPlaying && entity != Entity.Null)
            {
                if (usingReflection)
                {
                    ReceiveGenericNetworkCommands();
                    SyncEcsWithReflection();
                }
            }
        }

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
                            int v = (int) val;
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
        
        private void SyncEcsWithReflection()
        {
            for (var i = 0; i < fieldTogglesKeys.Count; i++)
            {
                var on = fieldTogglesValues[i];

                if (((string) fieldTogglesKeys[i]).Contains("Moving"))
                {
                    int bla = 3;
                    bla = 2;
                }
                
                if (!on) continue;

                if (!string.IsNullOrEmpty(fieldLinksValues[i]))
                {
                    var script = GetComponentFromKey(fieldLinksKeys[i]);

                    var fieldTypeString = GetFieldType(fieldLinksKeys[i]);
                    var networkedType = Type.GetType(schemaNamespace + fieldLinksValues[i]);
                    var fieldType = Type.GetType(fieldTypeString);

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

                    if (script == null) script = typeof(Transform);

                    var fieldName = GetFieldNameFromKey(fieldLinksKeys[i]);

                    var cmp = script == null
                        ? gameObject.GetComponent(typeof(Transform))
                        : gameObject.GetComponent(script);

                    var field = script.GetField(fieldName);
                    var property = script.GetProperty(fieldName);

                    object currentMonoValue;

                    
                    if (field != null)
                        currentMonoValue = field.GetValue(cmp);
                    else
                    {
                        if (CmpType(script, typeof(Animator)))
                        {
                            currentMonoValue = GetAnimatorValue((Animator)cmp, fieldName);
                        }
                        else
                        {
                            currentMonoValue = property.GetValue(cmp);
                        }
                    }

                    if (isSimulated)
                    {
                        SyncReflectionMonoToNetwork(networkedType, fieldType, currentMonoValue, script);
                    }
                    else
                    {
                        SyncReflectionNetworkEcsToMono(networkedType, fieldType, fieldName, field, cmp, property, script);
                    }
                }
            }
        }

        private void SetMonoValue(FieldInfo field, PropertyInfo property, object cmp, object val)
        {
            if (field != null)
                field.SetValue(cmp, val);
            else if (property != null) property.SetValue(cmp, val);
        }
        
        private void SyncReflectionNetworkEcsToMono(Type networkedType, Type fieldType, string fieldName, FieldInfo field, Component cmp,
            PropertyInfo property, Type script)
        {
            var method = typeof(EntityManager).GetMethod("GetComponentData");
            var generic = method.MakeGenericMethod(networkedType);

            var inst = generic.Invoke(entityManager, new object[] {entity});
            
            if(CmpType(script, typeof(Animator)))
            {
                var fx = networkedType.GetField("number");
                var vfx = fx.GetValue(inst);
                SetAnimatorValue((Animator)cmp, fieldName,  vfx);
                return;
            }
            
            if (CmpType(fieldType, typeof(Vector3)))
            {
                var fx = networkedType.GetField("Value");
                var vfx = (float3) fx.GetValue(inst);

                SetMonoValue(field, property, cmp, new Vector3(vfx.x, vfx.y, vfx.z));
            }

            if (CmpType(fieldType, typeof(float)))
            {
                var fx = networkedType.GetField("number");
                var vfx = (float) fx.GetValue(inst);

                SetMonoValue(field, property, cmp, vfx);
            }

            if (CmpType(fieldType, typeof(int)) || CmpType(fieldType, typeof(uint))  || CmpType(fieldType, typeof(bool)) || CmpType(fieldType, typeof(Boolean)))
            {
                var fx = networkedType.GetField("number");
                var vfx = (int) fx.GetValue(inst);

                SetMonoValue(field, property, cmp, vfx);
            }

            if (CmpType(fieldType, typeof(string)))
            {
                var fx = networkedType.GetField("name");
                var vfx = (FixedString64) fx.GetValue(inst);

                SetMonoValue(field, property, cmp, vfx.ToString());
            }

            if (CmpType(fieldType, typeof(Quaternion)))
            {
                var fx = networkedType.GetField("Value");
                var quat = (quaternion) fx.GetValue(inst);

                SetMonoValue(field, property, cmp, new Quaternion(quat.value.x, quat.value.y, quat.value.z, quat.value.w));
            }
        }

        private void SyncReflectionMonoToNetwork(Type networkedType, Type fieldType, object currentMonoValue, Type script)
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

            if (CmpType(fieldType, typeof(int)) || CmpType(fieldType, typeof(uint)) || CmpType(fieldType, typeof(bool)) || CmpType(fieldType, typeof(Boolean)))
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
                _ = generic.Invoke(entityManager, new[] {entity, inst});
            }
            catch (Exception e)
            {
                Debug.LogWarning($"{entity} {inst} {e.Message}");
                throw e;
            }
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
                return
                    $"{Name} {ParamInt1} {ParamInt2} {ParamInt3} {ParamInt4} {ParamFloat1} {ParamFloat2} {ParamFloat3} {ParamFloat4} {ParamString}";
            }
        }

        #region ListManipulation

        public void SetListValue(List<string> keyList, List<bool> valList, string key, bool val)
        {
            for (var i = 0; i < keyList.Count; i++)
                if (keyList[i] == key)
                {
                    valList[i] = val;
                    return;
                }

            keyList.Add(key);
            valList.Add(val);
        }

        private void SetListValue(List<string> keyList, List<string> valList, string key, string val)
        {
            for (var i = 0; i < keyList.Count; i++)
                if (keyList[i] == key)
                {
                    valList[i] = val;
                    return;
                }

            keyList.Add(key);
            valList.Add(val);
        }

        private void SetListValue(List<string> keyList, List<string> valList, string key, Type val)
        {
            for (var i = 0; i < keyList.Count; i++)
                if (keyList[i] == key)
                {
                    valList[i] = val.AssemblyQualifiedName;
                    return;
                }

            keyList.Add(key);
            valList.Add(val.ToString());
        }

        private bool? GetListValue(List<string> keyList, List<bool> valList, string key)
        {
            for (var i = 0; i < keyList.Count; i++)
                if (keyList[i] == key)
                    return valList[i];

            return null;
        }

        private string GetListValue(List<string> keyList, List<string> valList, string key)
        {
            for (var i = 0; i < keyList.Count; i++)
                if (keyList[i] == key)
                    return valList[i];

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
                SetListValue(fieldLinksKeys, fieldLinksValues, key, GetNextAvailableGenericNetworkedField(val));
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