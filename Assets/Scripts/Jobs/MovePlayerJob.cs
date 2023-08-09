using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using Unity.Mathematics;
using Unity.Jobs;


/// <summary>
/// Applique une force de d�placement sur le joueur selon la direction de d�placement et sa vitesse de d�placement
/// </summary>
[BurstCompile]
public partial struct MovePlayerJob : IJobEntity
{
    public float DeltaTime;

    [BurstCompile]
    public void Execute(ref PhysicsVelocity velocity, in MoveSpeedComponent moveSpeed, in PlayerMovementInputComponent input)
    {
        velocity.Linear = new float3(input.Value.x * moveSpeed.Speed, velocity.Linear.y, 0);
    }
   
}
