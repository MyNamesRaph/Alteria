using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;

/// <summary>
/// Créateur d'entitités de block
/// </summary>
[BurstCompile]
class BlockAuthoring : MonoBehaviour
{

}

[BurstCompile]
class BlockBaker : Baker<BlockAuthoring>
{

    /// <summary>
    /// Attachement des composants du block
    /// </summary>
    public override void Bake(BlockAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new IsBlockTag {});
    }
}
