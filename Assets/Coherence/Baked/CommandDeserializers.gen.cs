


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
					var data = new AuthorityTransfer();
					messageDeserializers.AuthorityTransfer(bitStream, ref data);

					if (mgr.Exists(entity))
					{
						var hasRequestBuffer = mgr.HasComponent<AuthorityTransfer>(entity);
						if (!hasRequestBuffer)
						{
							mgr.AddBuffer<AuthorityTransfer>(entity);
						}
						var buffer = mgr.GetBuffer<AuthorityTransfer>(entity);					
						buffer.Add(data);
					}
					break;
				}

				case TypeIds.InternalGenericCommand:
				{
					var data = new GenericCommand();
					messageDeserializers.GenericCommand(bitStream, ref data);

					if (mgr.Exists(entity))
					{
						var hasRequestBuffer = mgr.HasComponent<GenericCommand>(entity);
						if (!hasRequestBuffer)
						{
							mgr.AddBuffer<GenericCommand>(entity);
						}
						var buffer = mgr.GetBuffer<GenericCommand>(entity);					
						buffer.Add(data);
					}
					break;
				}

				case TypeIds.InternalPlayer_Controller_Foo:
				{
					var data = new Player_Controller_Foo();
					messageDeserializers.Player_Controller_Foo(bitStream, ref data);

					if (mgr.Exists(entity))
					{
						var hasRequestBuffer = mgr.HasComponent<Player_Controller_Foo>(entity);
						if (!hasRequestBuffer)
						{
							mgr.AddBuffer<Player_Controller_Foo>(entity);
						}
						var buffer = mgr.GetBuffer<Player_Controller_Foo>(entity);					
						buffer.Add(data);
					}
					break;
				}

			}

		}
	}
}


// ------------------ end of CommandDeserializers.cs -----------------
#endregion
