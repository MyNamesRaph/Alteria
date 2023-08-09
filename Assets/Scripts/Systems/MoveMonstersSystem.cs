using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// Système de déplacement des monstres
/// </summary>
[BurstCompile]
[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct MoveMonstersSystem : ISystem
{
    /// <summary>
    /// Mettre à jour la position des monstres à chaque frame
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
