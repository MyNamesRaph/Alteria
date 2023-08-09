using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;

/// <summary>
/// Cr�ateur de l'entit� de g�n�ration du monde
/// </summary>
[BurstCompile]
class WorldGenAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float2 WorldSize;
}


[BurstCompile]
class WorldGenBaker : Baker<WorldGenAuthoring>
{
    /// <summary>
    /// Attachement des composants de g�n�ration du monde
    /// </summary>
    public override void Bake(WorldGenAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new PrefabComponent
        {
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
        });
        AddComponent(entity, new WorldSizeComponent
        {
            Size = authoring.WorldSize
        });
    }
}
