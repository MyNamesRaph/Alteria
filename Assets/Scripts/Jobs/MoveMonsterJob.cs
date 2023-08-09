using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using Unity.Mathematics;
using Unity.Jobs;
using UnityEngine;


/// <summary>
/// Déplace les monstres dans la direction du joueur selon leur vitesse de déplacement
/// </summary>
[BurstCompile]
public partial struct MoveMonsterJob : IJobEntity
{
    public float DeltaTime;
    public float3 PlayerPosition;

    [BurstCompile]
    public void Execute(ref LocalTransform transform, in MoveSpeedComponent speed, in IsMonsterTag _)
    {
        Vector3 direction = transform.Position - PlayerPosition;
        direction.Normalize();

        float3 movement = direction * speed.Speed * DeltaTime;
        transform.Position -= movement;
    }
   
}
