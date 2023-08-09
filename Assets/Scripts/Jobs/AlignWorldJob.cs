using Unity.Burst;
using Unity.Jobs;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// Aligne toutes les entitées de blocks sur la grille du monde
/// </summary>
[BurstCompile]
public partial struct AlignWorldJob : IJobEntity
{
    [ReadOnly] public float2 WorldSize;
    public float2 Pos;
    [ReadOnly] public float2 Offset;

    public void Execute(ref LocalTransform transform,in IsBlockTag tag)
    {
        if (Pos.y < WorldSize.y)
        {
            transform.Position = new float3(Offset.x + Pos.x++, Offset.y + Pos.y, 0);
            if (Pos.x > WorldSize.x-1)
            {
                Pos.y++;
                Pos.x = 0;
            }
        }
    }
}
