using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using Unity.Mathematics;
using Unity.Jobs;


/// <summary>
/// Déplace les projectiles selon leur direction et leur vitesse de déplacement
/// </summary>
[BurstCompile]
public partial struct MoveProjectileJob : IJobEntity
{
    public float DeltaTime;

    [BurstCompile]
    public void Execute(ref LocalTransform transform, in DirectionComponent direction, in MoveSpeedComponent speed, IsProjectileTag _)
    {
        var movement = direction.Value * speed.Speed * DeltaTime;
        transform.Position -= new float3(movement, 0);
    }
   
}
