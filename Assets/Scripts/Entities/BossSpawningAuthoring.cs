using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;

/// <summary>
/// Cr�ateur de l'entit� de g�n�ration du boss
/// </summary>
[BurstCompile]
class BossSpawningAuthoring : MonoBehaviour
{
    public GameObject Prefab;
}


[BurstCompile]
class BossSpawningBaker : Baker<BossSpawningAuthoring>
{
    /// <summary>
    /// Attachement des composants de g�n�ration du boss
    /// </summary>
    public override void Bake(BossSpawningAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new PrefabComponent
        {
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
        });
        AddComponent(entity, new IsBossSpawnerTag());
    }
}
