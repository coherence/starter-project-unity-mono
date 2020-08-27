using System.Reflection;
using System;
using System.Linq;
using Coherence.Generated.Internal.FirstProject;
using Unity.Entities;
using UnityEngine;

public class CoherenceUtil
{
    public static void ReplaceEntity(EntityManager entityManager, Entity networkEntity, Entity newEntity)
    {
#if UNITY_EDITOR
        entityManager.SetName(newEntity, "Remote Player");
#endif

        //EntityManager.AddComponentData(newEntity, EntityManager.GetComponentData<CoherenceSimulateComponent>(networkEntity));
        CopyComponents(networkEntity, newEntity);

        // Remap
        var mapper = entityManager.World.GetExistingSystem<SyncSendSystem>().Sender.Mapper;
        if (!mapper.ToCoherenceEntityId(networkEntity, out var entityId))
        {
            Debug.LogError("Networked Entity not found in mapper: " + networkEntity); // Should not happen
        }
        mapper.Remove(entityId);
        mapper.Add(entityId, newEntity);

        entityManager.DestroyEntity(networkEntity);

        Debug.Log(string.Format("Replaced networked Entity {0}, {1} with {2}.", "?", networkEntity, newEntity));
    }

    public static void CopyComponents(Entity src, Entity dst)
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var types = entityManager.GetComponentTypes(src);

        foreach (ComponentType t in types)
        {
            //Debug.LogWarning($"TYPE: {t}");
            if (t == typeof(LinkedEntityGroup))
            {
                continue;
            }

            if (!entityManager.HasComponent(dst, t))
            {
                entityManager.AddComponent(dst, t);
            }

            MethodInfo getMethod = t.IsBuffer ? typeof(EntityManager).GetMethod("GetBuffer") : typeof(EntityManager).GetMethod("GetComponentData");
            if (getMethod != null)
            {
                MethodInfo getMethodGeneric = getMethod.MakeGenericMethod(t.GetManagedType());
                object cmp;
                try
                {
                    cmp = getMethodGeneric.Invoke(entityManager, new object[] { src });
                }
                catch (Exception)
                {
                    var setMethod = typeof(EntityManager).GetMethods().Single(
                                                                              m =>
                                                                              m.Name == (t.IsBuffer ? "AddBuffer" : "AddComponent") &&
                                                                              m.GetParameters().Length == 2 &&
                                                                              m.GetParameters()[0].ParameterType == typeof(Entity) &&
                                                                              !m.IsGenericMethod
                                                                              );


                    if (setMethod != null)
                    {
                        setMethod.Invoke(entityManager, new object[] { dst, t });
                    }

                    continue;
                }

                if (cmp != null)
                {
                    if (t.IsBuffer)
                    {
                        MethodInfo setMethod = typeof(EntityManager).GetMethod("AddBuffer");

                        if (setMethod != null)
                        {
                            MethodInfo setMethodGeneric = setMethod.MakeGenericMethod(t.GetManagedType());
                            setMethodGeneric.Invoke(entityManager, new object[] { dst });
                        }
                    }
                    else
                    {
                        var tt = Convert.ChangeType(cmp, t.GetManagedType());

                        MethodInfo setMethod = typeof(EntityManager).GetMethod("SetComponentData");

                        if (setMethod != null)
                        {
                            MethodInfo setMethodGeneric = setMethod.MakeGenericMethod(t.GetManagedType());
                            setMethodGeneric.Invoke(entityManager, new object[] { dst, tt });
                        }
                    }
                }
            }
        }
    }
}
