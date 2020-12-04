


#region Shared
// -----------------------------------
//  Shared.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.IntegrationTests
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

}


enum TypeEnums
{

	InternalWorldPosition = 0,

	InternalWorldOrientation = 1,

	InternalLocalUser = 2,

	InternalWorldPositionQuery = 3,

	InternalSessionBased = 4,

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
