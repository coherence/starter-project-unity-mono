using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

// This script needs to be on a GameObject with a ConvertToEntity component
// set to "Convert and Inject Game Object", or nothing will happen!

public class PrefabHolder : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    public GameObject playerPrefab;
    public Entity playerPrefabEntity;

    public GameObject otherPlayerPrefab;
    public Entity otherPlayerPrefabEntity;

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(playerPrefab);
        referencedPrefabs.Add(otherPlayerPrefab);
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        playerPrefabEntity = conversionSystem.GetPrimaryEntity(playerPrefab);
        otherPlayerPrefabEntity = conversionSystem.GetPrimaryEntity(otherPlayerPrefab);
    }

    public static PrefabHolder Get()
    {
        return FindObjectOfType<PrefabHolder>();
    }
}
