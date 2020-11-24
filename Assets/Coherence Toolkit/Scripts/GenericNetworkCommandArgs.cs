using System;
using System.Collections.Generic;
using System.Reflection;
using Coherence.Generated.FirstProject;
using UnityEngine;

namespace Coherence.MonoBridge
{
    public class GenericNetworkCommandArgs : EventArgs
    {
        public string Name { get; set; }

        public int ParamInt1 { get; set; }
        public int ParamInt2 { get; set; }
        public int ParamInt3 { get; set; }
        public int ParamInt4 { get; set; }

        public float ParamFloat1 { get; set; }
        public float ParamFloat2 { get; set; }
        public float ParamFloat3 { get; set; }
        public float ParamFloat4 { get; set; }

        public string ParamString { get; set; }

        public override string ToString()
        {
            return
                $"{Name} {ParamInt1} {ParamInt2} {ParamInt3} {ParamInt4} {ParamFloat1} {ParamFloat2} {ParamFloat3} {ParamFloat4} {ParamString}";
        }

        public static GenericNetworkCommandArgs FromGenericCommand(GenericCommand command)
        {
            return new GenericNetworkCommandArgs
            {
                Name = command.name.ToString(),

                ParamInt1 = command.paramInt1,
                ParamInt2 = command.paramInt2,
                ParamInt3 = command.paramInt3,
                ParamInt4 = command.paramInt4,

                ParamFloat1 = command.paramFloat1,
                ParamFloat2 = command.paramFloat2,
                ParamFloat3 = command.paramFloat3,
                ParamFloat4 = command.paramFloat4,

                ParamString = command.paramString.ToString(),
            };
        }

        public static GenericNetworkCommandArgs FromObjects(string commandName, object[] args)
        {
            var (paramInt, paramFloat, paramString) = TypeHelpers.ExtractTypedArraysFromObjects(args);

            var genericCommand = new GenericNetworkCommandArgs
            {
                Name = String.IsNullOrEmpty(commandName) ? "" : commandName,

                ParamInt1 = paramInt[0],
                ParamInt2 = paramInt[1],
                ParamInt3 = paramInt[2],
                ParamInt4 = paramInt[3],

                ParamFloat1 = paramFloat[0],
                ParamFloat2 = paramFloat[1],
                ParamFloat3 = paramFloat[2],
                ParamFloat4 = paramFloat[3],

                ParamString = String.IsNullOrEmpty(paramString[0]) ? "" : paramString[0],
            };

            return genericCommand;
        }

        public object[] ArgListForMethod(MethodInfo method)
        {
            var intParams = new int[] { this.ParamInt1, this.ParamInt2, this.ParamInt3, this.ParamInt4 };
            var floatParams = new float[] { this.ParamFloat1, this.ParamFloat2, this.ParamFloat3, this.ParamFloat4 };
            var stringParams = new string[] { this.ParamString };

            var methodArgs = new List<object>();
            var intIndex = 0;
            var floatIndex = 0;
            var stringIndex = 0;

            foreach(var parameter in method.GetParameters())
            {
                if(parameter.ParameterType == typeof(int)) {
                    methodArgs.Add(intParams[intIndex++]);
                }
                else if(parameter.ParameterType == typeof(float)) {
                    methodArgs.Add(floatParams[floatIndex++]);
                }
                else if(parameter.ParameterType == typeof(string)) {
                    methodArgs.Add(stringParams[stringIndex++]);
                }
                else {
                    Debug.LogError("Command can't call method with argument of type '{parameter.ParameterType}'.");
                    return null;
                }
            }

            return methodArgs.ToArray();
        }
    }

}
