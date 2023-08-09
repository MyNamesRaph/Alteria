using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// Syst�me de d�placement des monstres
/// </summary>
[BurstCompile]
[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct MoveMonstersSystem : ISystem
{
    /// <summary>
    /// Mettre � jour la position des monstres � chaque frame
    /// </summary>
    /// <param name="state"></param>
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        var player = SystemAPI.GetSingletonEntity<IsPlayerTag>();
        var playerPos = SystemAPI.GetComponent<LocalTransform>(player).Position;

        new MoveMonsterJob
        {
            DeltaTime = deltaTime,
            PlayerPosition = playerPos,
        }.Schedule();
    }
}
