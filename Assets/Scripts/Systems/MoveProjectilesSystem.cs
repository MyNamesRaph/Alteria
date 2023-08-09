using Unity.Entities;
using Unity.Physics.Systems;
using Unity.Burst;

/// <summary>
/// Syst�me de d�placement des projectiles
/// </summary>
[BurstCompile]
[UpdateBefore(typeof(PhysicsSystemGroup))]
public partial struct MoveProjectilesSystem : ISystem
{
    /// <summary>
    /// Mettre � jour la position des projectiles � chaque frame
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
    /// Cr�er un EntityCommandBuffer � partir du state
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
