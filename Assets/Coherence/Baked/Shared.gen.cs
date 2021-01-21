


#region Shared
// -----------------------------------
//  Shared.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal
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

	public const uint InternalTransferable = 5;

	public const uint InternalGenericPrefabReference = 6;

	public const uint InternalGenericScale = 7;

	public const uint InternalGenericFieldInt0 = 8;

	public const uint InternalGenericFieldInt1 = 9;

	public const uint InternalGenericFieldInt2 = 10;

	public const uint InternalGenericFieldInt3 = 11;

	public const uint InternalGenericFieldInt4 = 12;

	public const uint InternalGenericFieldInt5 = 13;

	public const uint InternalGenericFieldInt6 = 14;

	public const uint InternalGenericFieldInt7 = 15;

	public const uint InternalGenericFieldInt8 = 16;

	public const uint InternalGenericFieldInt9 = 17;

	public const uint InternalGenericFieldFloat0 = 18;

	public const uint InternalGenericFieldFloat1 = 19;

	public const uint InternalGenericFieldFloat2 = 20;

	public const uint InternalGenericFieldFloat3 = 21;

	public const uint InternalGenericFieldFloat4 = 22;

	public const uint InternalGenericFieldFloat5 = 23;

	public const uint InternalGenericFieldFloat6 = 24;

	public const uint InternalGenericFieldFloat7 = 25;

	public const uint InternalGenericFieldFloat8 = 26;

	public const uint InternalGenericFieldFloat9 = 27;

	public const uint InternalGenericFieldVector0 = 28;

	public const uint InternalGenericFieldVector1 = 29;

	public const uint InternalGenericFieldVector2 = 30;

	public const uint InternalGenericFieldVector3 = 31;

	public const uint InternalGenericFieldString0 = 32;

	public const uint InternalGenericFieldString1 = 33;

	public const uint InternalGenericFieldString2 = 34;

	public const uint InternalGenericFieldString4 = 35;

	public const uint InternalGenericFieldQuaternion0 = 36;

	public const uint InternalAuthorityTransfer = 0;

	public const uint InternalGenericCommand = 1;

}


enum TypeEnums
{

	InternalWorldPosition = 0,

	InternalWorldOrientation = 1,

	InternalLocalUser = 2,

	InternalWorldPositionQuery = 3,

	InternalSessionBased = 4,

	InternalTransferable = 5,

	InternalGenericPrefabReference = 6,

	InternalGenericScale = 7,

	InternalGenericFieldInt0 = 8,

	InternalGenericFieldInt1 = 9,

	InternalGenericFieldInt2 = 10,

	InternalGenericFieldInt3 = 11,

	InternalGenericFieldInt4 = 12,

	InternalGenericFieldInt5 = 13,

	InternalGenericFieldInt6 = 14,

	InternalGenericFieldInt7 = 15,

	InternalGenericFieldInt8 = 16,

	InternalGenericFieldInt9 = 17,

	InternalGenericFieldFloat0 = 18,

	InternalGenericFieldFloat1 = 19,

	InternalGenericFieldFloat2 = 20,

	InternalGenericFieldFloat3 = 21,

	InternalGenericFieldFloat4 = 22,

	InternalGenericFieldFloat5 = 23,

	InternalGenericFieldFloat6 = 24,

	InternalGenericFieldFloat7 = 25,

	InternalGenericFieldFloat8 = 26,

	InternalGenericFieldFloat9 = 27,

	InternalGenericFieldVector0 = 28,

	InternalGenericFieldVector1 = 29,

	InternalGenericFieldVector2 = 30,

	InternalGenericFieldVector3 = 31,

	InternalGenericFieldString0 = 32,

	InternalGenericFieldString1 = 33,

	InternalGenericFieldString2 = 34,

	InternalGenericFieldString4 = 35,

	InternalGenericFieldQuaternion0 = 36,

	InternalAuthorityTransfer = 0,

	InternalGenericCommand = 1,

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
