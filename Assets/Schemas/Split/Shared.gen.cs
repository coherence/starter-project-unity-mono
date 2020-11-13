


#region Shared
// -----------------------------------
//  Shared.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.Schema
{
    using System;
    using System.Collections.Generic;
	using Replication.Client.Unity.Ecs;


	

static class TypeIds
{
    // Note: The indexes of components/commands/events *should* start over from 0

	public const uint InternalWorldPosition = 0;

	public const uint InternalWorldOrientation = 1;

	public const uint InternalLocalUser = 2;

	public const uint InternalWorldPositionQuery = 3;

	public const uint InternalSessionBased = 4;

	public const uint InternalGenericPrefabReference = 5;

	public const uint InternalGenericScale = 6;

	public const uint InternalGenericFieldInt0 = 7;

	public const uint InternalGenericFieldInt1 = 8;

	public const uint InternalGenericFieldInt2 = 9;

	public const uint InternalGenericFieldInt3 = 10;

	public const uint InternalGenericFieldInt4 = 11;

	public const uint InternalGenericFieldInt5 = 12;

	public const uint InternalGenericFieldInt6 = 13;

	public const uint InternalGenericFieldInt7 = 14;

	public const uint InternalGenericFieldInt8 = 15;

	public const uint InternalGenericFieldInt9 = 16;

	public const uint InternalGenericFieldFloat0 = 17;

	public const uint InternalGenericFieldFloat1 = 18;

	public const uint InternalGenericFieldFloat2 = 19;

	public const uint InternalGenericFieldFloat3 = 20;

	public const uint InternalGenericFieldFloat4 = 21;

	public const uint InternalGenericFieldFloat5 = 22;

	public const uint InternalGenericFieldFloat6 = 23;

	public const uint InternalGenericFieldFloat7 = 24;

	public const uint InternalGenericFieldFloat8 = 25;

	public const uint InternalGenericFieldFloat9 = 26;

	public const uint InternalGenericFieldVector0 = 27;

	public const uint InternalGenericFieldVector1 = 28;

	public const uint InternalGenericFieldVector2 = 29;

	public const uint InternalGenericFieldVector3 = 30;

	public const uint InternalGenericFieldString0 = 31;

	public const uint InternalGenericFieldString1 = 32;

	public const uint InternalGenericFieldString2 = 33;

	public const uint InternalGenericFieldString4 = 34;

	public const uint InternalNPCBehaviour = 35;

	public const uint InternalRotationBehaviour = 36;

	public const uint InternalShowNameAndState = 37;

	public const uint InternalPlayerBehaviour = 38;

	public const uint InternalBullet = 39;

	public const uint InternalColorizeBehaviour = 40;

	public const uint InternalController = 41;

	public const uint InternalGenericCommand = 0;

}


enum TypeEnums
{

	InternalWorldPosition = 0,

	InternalWorldOrientation = 1,

	InternalLocalUser = 2,

	InternalWorldPositionQuery = 3,

	InternalSessionBased = 4,

	InternalGenericPrefabReference = 5,

	InternalGenericScale = 6,

	InternalGenericFieldInt0 = 7,

	InternalGenericFieldInt1 = 8,

	InternalGenericFieldInt2 = 9,

	InternalGenericFieldInt3 = 10,

	InternalGenericFieldInt4 = 11,

	InternalGenericFieldInt5 = 12,

	InternalGenericFieldInt6 = 13,

	InternalGenericFieldInt7 = 14,

	InternalGenericFieldInt8 = 15,

	InternalGenericFieldInt9 = 16,

	InternalGenericFieldFloat0 = 17,

	InternalGenericFieldFloat1 = 18,

	InternalGenericFieldFloat2 = 19,

	InternalGenericFieldFloat3 = 20,

	InternalGenericFieldFloat4 = 21,

	InternalGenericFieldFloat5 = 22,

	InternalGenericFieldFloat6 = 23,

	InternalGenericFieldFloat7 = 24,

	InternalGenericFieldFloat8 = 25,

	InternalGenericFieldFloat9 = 26,

	InternalGenericFieldVector0 = 27,

	InternalGenericFieldVector1 = 28,

	InternalGenericFieldVector2 = 29,

	InternalGenericFieldVector3 = 30,

	InternalGenericFieldString0 = 31,

	InternalGenericFieldString1 = 32,

	InternalGenericFieldString2 = 33,

	InternalGenericFieldString4 = 34,

	InternalNPCBehaviour = 35,

	InternalRotationBehaviour = 36,

	InternalShowNameAndState = 37,

	InternalPlayerBehaviour = 38,

	InternalBullet = 39,

	InternalColorizeBehaviour = 40,

	InternalController = 41,

	InternalGenericCommand = 0,

}


internal static class InternalGlobalLookups
{
	internal readonly static Dictionary<Type, TypeEnums> GlobalTypeToEnumLookup = new Dictionary<Type, TypeEnums>();

	internal static TypeEnums Lookup<T>()
	{
		return GlobalTypeToEnumLookup[typeof(T)];
	}

	internal static void Register<T>(TypeEnums e)
	{
		GlobalTypeToEnumLookup.Add(typeof(T), e);
	}
}

internal static class GlobalLookups
{
	internal readonly static Dictionary<System.Type, TypeEnums> GlobalTypeToEnumLookup =
		new Dictionary<System.Type, TypeEnums>();

	internal static TypeEnums Lookup<T>()
	{
		return GlobalTypeToEnumLookup[typeof(T)];
	}

	internal static void Register<T>(TypeEnums e)
	{
		if (!GlobalTypeToEnumLookup.ContainsKey(typeof(T))) {
			GlobalTypeToEnumLookup.Add(typeof(T), e);
		}
	}
}

internal static class GlobalTypeIdLookups
{
	internal readonly static Dictionary<System.Type, uint> GlobalTypeToEnumLookup =
		new Dictionary<System.Type, uint>();

	internal static uint Lookup<T>()
	{
		return GlobalTypeToEnumLookup[typeof(T)];
	}

	internal static (uint, bool) LookupUsingType(System.Type t)
	{
		var foundIt = GlobalTypeToEnumLookup.TryGetValue(t, out var value);
		return !foundIt ? ((uint, bool)) (0, foundIt) : (value, true);
	}

	internal static void Register<T>(uint e)
	{
		if (!GlobalTypeToEnumLookup.ContainsKey(typeof(T))) {
			GlobalTypeToEnumLookup.Add(typeof(T), e);
		}
	}
}

class GlobalTypeIdLookupsWrapper : ITypeIdLookups
{
	public (uint, bool) LookupUsingType(Type t)
	{
		return GlobalTypeIdLookups.LookupUsingType(t);
	}
}

static class RleConstants
{
	public const uint EndOfComponentArray = 255;
	public const uint EndOfComponentIndex = 65535;
}




} // end of namespace


// ------------------ end of Shared.cs -----------------
#endregion
