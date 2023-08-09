using Unity.Entities;
using Unity.Physics.Systems;
using Unity.Burst;


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

    [BurstCompile]
    private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        return ecb.AsParallelWriter();
    }
}
