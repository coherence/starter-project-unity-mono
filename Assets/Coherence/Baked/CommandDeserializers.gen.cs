


#region CommandDeserializers
// -----------------------------------
//  CommandDeserializers.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.Toolkit
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

			switch (commandTypeID)
			{

				case TypeIds.InternalGenericCommand:
				{
					var hasRequestBuffer = mgr.HasComponent<GenericCommand>(entity);
					if (!hasRequestBuffer)
					{
						mgr.AddBuffer<GenericCommand>(entity);
					}
					var buffer = mgr.GetBuffer<GenericCommand>(entity);
					var data = new GenericCommand();
					messageDeserializers.GenericCommand(bitStream, ref data);
					buffer.Add(data);
					break;
				}

			}

		}
	}
}


// ------------------ end of CommandDeserializers.cs -----------------
#endregion
