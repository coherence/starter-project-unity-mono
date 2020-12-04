


#region SyncComponent
// -----------------------------------
//  SyncComponent.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.IntegrationTests
{
    using Unity.Entities;
    using Unity.Transforms;
    using global::Coherence.Generated.FirstProject;


    public struct WorldPosition_Sync : IComponentData
    {
        public Translation lastSentData;
        public uint resendMask;
        public uint howImportantAreYou;
        public uint accumulatedPriority;
        public long deletedAtTime;
        public bool hasBeenSerialized;
        public bool deleteHasBeenSerialized;
        public bool hasReceivedConstructor;
    }


    public struct WorldOrientation_Sync : IComponentData
    {
        public Rotation lastSentData;
        public uint resendMask;
        public uint howImportantAreYou;
        public uint accumulatedPriority;
        public long deletedAtTime;
        public bool hasBeenSerialized;
        public bool deleteHasBeenSerialized;
        public bool hasReceivedConstructor;
    }


    public struct LocalUser_Sync : IComponentData
    {
        public LocalUser lastSentData;
        public uint resendMask;
        public uint howImportantAreYou;
        public uint accumulatedPriority;
        public long deletedAtTime;
        public bool hasBeenSerialized;
        public bool deleteHasBeenSerialized;
        public bool hasReceivedConstructor;
    }


    public struct WorldPositionQuery_Sync : IComponentData
    {
        public WorldPositionQuery lastSentData;
        public uint resendMask;
        public uint howImportantAreYou;
        public uint accumulatedPriority;
        public long deletedAtTime;
        public bool hasBeenSerialized;
        public bool deleteHasBeenSerialized;
        public bool hasReceivedConstructor;
    }


    public struct SessionBased_Sync : IComponentData
    {
        public SessionBased lastSentData;
        public uint resendMask;
        public uint howImportantAreYou;
        public uint accumulatedPriority;
        public long deletedAtTime;
        public bool hasBeenSerialized;
        public bool deleteHasBeenSerialized;
        public bool hasReceivedConstructor;
    }


}


// ------------------ end of SyncComponent.cs -----------------
#endregion
