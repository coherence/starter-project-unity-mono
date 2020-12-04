


#region CommandDeserializers
// -----------------------------------
//  CommandDeserializers.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.IntegrationTests
{
	using global::Coherence.Generated.FirstProject;
	using Coherence.Log;
	using Unity.Entities;
	using Replication.Client.Unity.Ecs;
	using Coherence.Replication.Unity;

	public class PerformCommands : IPerformCommand
	{
        private MessageDeserializers messageDeserializers;

         public PerformCommands(UnityMapper mapper)
         {
             messageDeserializers = new MessageDeserializers(mapper);
         }

		public void PerformCommand(EntityManager mgr, Entity entity, uint commandTypeID, Coherence.Replication.Protocol.Definition.IInBitStream bitStream)
		{

		}
	}
}


// ------------------ end of CommandDeserializers.cs -----------------
#endregion
