


#region InputDeserializers
// -----------------------------------
//  InputDeserializers.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal
{
	using global::Coherence.Generated;
	using Coherence.Log;
	using Unity.Entities;
	using Replication.Client.Unity.Ecs;
	using Coherence.Replication.Unity;

	public class PerformInputs : IPerformInput
	{
        private MessageDeserializers messageDeserializers;

         public PerformInputs(UnityMapper mapper)
         {
             messageDeserializers = new MessageDeserializers(mapper);
         }

		public void PerformInput(EntityManager mgr, Entity entity, uint inputTypeID, Coherence.Replication.Protocol.Definition.IInBitStream bitStream)
		{
		}
	}
}


// ------------------ end of InputDeserializers.cs -----------------
#endregion
