using Unity.Entities;
using Unity.Physics.Systems;
using Unity.Burst;
using Unity.Collections;


/// <summary>
/// Système pour tuer les monstres
/// </summary>
[BurstCompile]
public partial struct KillEntitiesSystem : ISystem
{
    /// <summary>
    /// Tuer les entitées avec 0 ou moins de points de vies
    /// </summary>
    /// <param name="state"></param>
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var query = SystemAPI.QueryBuilder().WithAll<HealthComponent>().Build();
        var entityArray = query.ToEntityArray(Allocator.Temp);


        foreach (var entity in entityArray)
        {
            var healthLookup = SystemAPI.GetComponentLookup<HealthComponent>();
            var hp = healthLookup.GetRefRW(entity);

            if (hp.ValueRO.Value <= 0)
            {
                state.EntityManager.DestroyEntity(entity);
            }
        }

        
    }
}
