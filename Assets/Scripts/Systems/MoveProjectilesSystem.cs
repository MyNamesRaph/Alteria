using Unity.Entities;
using Unity.Physics.Systems;
using Unity.Burst;

/// <summary>
/// Système de déplacement des projectiles
/// </summary>
[BurstCompile]
[UpdateBefore(typeof(PhysicsSystemGroup))]
public partial struct MoveProjectilesSystem : ISystem
{
    /// <summary>
    /// Mettre à jour la position des projectiles à chaque frame
    /// </summary>
    /// <param name="state"></param>
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        new MoveProjectileJob
        {
            DeltaTime = deltaTime
        }.Schedule();
    }

    /// <summary>
    /// Créer un EntityCommandBuffer à partir du state
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    [BurstCompile]
    private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        return ecb.AsParallelWriter();
    }
}
