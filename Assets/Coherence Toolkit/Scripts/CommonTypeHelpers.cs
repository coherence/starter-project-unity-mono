namespace Coherence.MonoBridge
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;

    public class TypeHelpers
    {
        public static bool SkipThisType(Type compType)
        {
            return
                compType.IsSubclassOf(typeof(Renderer)) || compType == typeof(Renderer) ||
                compType.IsSubclassOf(typeof(MeshFilter)) || compType == typeof(MeshFilter) ||
                compType.IsSubclassOf(typeof(CoherenceSync)) || compType == typeof(CoherenceSync) ||
                compType.IsSubclassOf(typeof(Collider)) || compType == typeof(Collider) ||
                compType.IsSubclassOf(typeof(Rigidbody)) || compType == typeof(Rigidbody) ||
                compType.IsSubclassOf(typeof(Rigidbody2D)) || compType == typeof(Rigidbody2D) ||
                compType.IsSubclassOf(typeof(Collider)) || compType == typeof(Collider) ||
                compType.IsSubclassOf(typeof(Collider2D)) || compType == typeof(Collider2D);
        }

        public static Type GetUnderlyingType(MemberInfo member)
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

        private readonly static Type[] supportedTypes = {
            typeof(Vector3),
            typeof(Quaternion),
            typeof(float),
            typeof(int),
            typeof(uint),
            typeof(string),
            typeof(bool),
            typeof(Boolean)
        };

        public static bool IsTypeSupported(Type type)
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

        private static bool IsMethodTypeSupported(MethodInfo methodInfo)
        {
            var returnType = methodInfo.ReturnType;

            if (returnType != typeof(void))
            {
                return false;
            }

            foreach(var parameterInfo in methodInfo.GetParameters())
            {
                if(!IsTypeSupported(parameterInfo.ParameterType))
                {
                    return false;
                }
            }

            return true;
        }

        public static string MethodArgsAsString(MethodInfo methodInfo)
        {
            var paramStrings = new List<string>();
            foreach(var parameter in methodInfo.GetParameters())
            {
                paramStrings.Add(NiceLooking(parameter.ParameterType));
            }
            var joinedParamStrings = String.Join(", ", paramStrings);
            return $"({joinedParamStrings})";
        }

        public static string MethodAsString(MethodInfo methodInfo)
        {
            return methodInfo.ReturnType.Name + methodInfo.Name + MethodArgsAsString(methodInfo);
        }

        private static HashSet<Type> classesThatHideTheirMethods = new HashSet<Type>()
        {
            typeof(MonoBehaviour),
            typeof(Behaviour),
            typeof(System.Object),
            typeof(UnityEngine.Object),
            typeof(Component)
        };

        public static bool ShowThisMethod(MethodInfo methodInfo)
        {
            return
                methodInfo.IsPublic &&
                TypeHelpers.IsMethodTypeSupported(methodInfo) &&
                !classesThatHideTheirMethods.Contains(methodInfo.DeclaringType);
        }

        public static MemberInfo[] Members(Type type)
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            return type.GetFields(bindingFlags).Cast<MemberInfo>().Concat(type.GetProperties(bindingFlags)).ToArray();
        }

        public static MethodInfo[] Methods(Type type)
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            return type.GetMethods(bindingFlags).Cast<MethodInfo>().ToArray();
        }

        public static MemberInfo[] MonoMembers {
            get {
                var monoBehaviourType = typeof(MonoBehaviour);
                const BindingFlags monoBindingFlags = BindingFlags.Public | BindingFlags.Instance;
                var monoMembers = monoBehaviourType.GetFields(monoBindingFlags).Cast<MemberInfo>()
                                           .Concat(monoBehaviourType.GetProperties(monoBindingFlags)).ToArray();
                return monoMembers;
            }
        }

        public static bool IsMonoMember(Type type)
        {
            foreach (MemberInfo monoVariable in TypeHelpers.MonoMembers)
            {
                if (type == monoVariable.GetType())
                {
                    return true;
                }
            }
            return false;
        }

        public static string ToSchemaType(Type type)
        {
            if(type == typeof(int)) { return "Int"; }
            else if(type == typeof(uint)) { return "Int"; }
            else if(type == typeof(float)) { return "Float"; }
            else if(type == typeof(bool)) { return "Bool"; }
            else if(type == typeof(Boolean)) { return "Bool"; }
            else if(type == typeof(string)) { return "String64"; }
            else if(type == typeof(Vector3)) { return "Vector3"; }
            else if(type == typeof(Quaternion)) { return "Quaternion"; }
            else {
                throw new Exception($"Can't convert type {type} to schema member type.");
            }
        }

        public static string NiceLooking(Type type)
        {
            if(type == typeof(int)) { return "int"; }
            else if(type == typeof(uint)) { return "uint"; }
            else if(type == typeof(float)) { return "float"; }
            else if(type == typeof(bool)) { return "bool"; }
            else if(type == typeof(Boolean)) { return "bool"; }
            else if(type == typeof(string)) { return "string"; }
            else if(type == typeof(Vector3)) { return "Vector3"; }
            else if(type == typeof(Quaternion)) { return "Quaternion"; }
            else {
                throw new Exception($"Can't convert type {type} to nice looking type.");
            }
        }
    }
}
