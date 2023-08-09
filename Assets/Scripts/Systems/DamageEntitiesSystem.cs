using Unity.Entities;
using Unity.Physics.Systems;
using Unity.Burst;

/// <summary>
/// Système d'application des dégats aux entités
/// </summary>
[BurstCompile]
public partial struct DamageEntitiesSystem : ISystem
{
    /// <summary>
    /// Appliquer les dégats au entitées
    /// </summary>
    /// <param name="state"></param>
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        new DamageEntitiesJob
        {

        }.Run();
    }
}
