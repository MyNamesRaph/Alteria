using UnityEngine;
using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;

/// <summary>
/// Créateur d'entitités de projectile
/// </summary>
[BurstCompile]
class ProjectileAuthoring : MonoBehaviour
{
    public float MoveSpeed;
    public int Damage;
}

[BurstCompile]
class ProjectileBaker : Baker<ProjectileAuthoring>
{

    /// <summary>
    /// Attachement des composants du projectile
    /// </summary>
    public override void Bake(ProjectileAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new IsProjectileTag { });
        AddComponent(entity, new MoveSpeedComponent { Speed = authoring.MoveSpeed });
        AddComponent(entity, new DirectionComponent { Value = float2.zero });
        AddComponent(entity, new DamageComponent { Value = authoring.Damage });
    }
}
