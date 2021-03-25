


#region CommandDeserializers
// -----------------------------------
//  CommandDeserializers.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal
{
	using global::Coherence.Generated;
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

				case TypeIds.InternalAuthorityTransfer:
				{
					var hasRequestBuffer = mgr.HasComponent<AuthorityTransfer>(entity);
					if (!hasRequestBuffer)
					{
						mgr.AddBuffer<AuthorityTransfer>(entity);
					}
					var buffer = mgr.GetBuffer<AuthorityTransfer>(entity);
					var data = new AuthorityTransfer();
					messageDeserializers.AuthorityTransfer(bitStream, ref data);
					buffer.Add(data);
					break;
				}

				case TypeIds.InternalInputClientCommand:
				{
					var hasRequestBuffer = mgr.HasComponent<InputClientCommand>(entity);
					if (!hasRequestBuffer)
					{
						mgr.AddBuffer<InputClientCommand>(entity);
					}
					var buffer = mgr.GetBuffer<InputClientCommand>(entity);
					var data = new InputClientCommand();
					messageDeserializers.InputClientCommand(bitStream, ref data);
					buffer.Add(data);
					break;
				}

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
