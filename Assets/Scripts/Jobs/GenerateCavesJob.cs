using Unity.Burst;
using Unity.Jobs;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine.Jobs;
using UnityEngine;

/// <summary>
/// Désactive les blocks du monde faisant partie d'une grotte
/// </summary>
public partial struct GenerateCavesJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter Ecb;
    [ReadOnly] public int Seed;
    [ReadOnly] public float2 WorldSize;
    public float2 Pos;
    public NativeArray<Entity> Blocks;

    public void Execute([ChunkIndexInQuery] int chunkIndex,ref LocalTransform transform,in IsBlockTag tag)
    {
        Noise.Instance.SetSeed(Seed);
        Noise.Instance.FastNoiseLite.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
        if (Pos.y < WorldSize.y)
        {
            float value = Noise.Instance.FastNoiseLite.GetNoise(Pos.x, Pos.y);;
            if (value > 0)
            {
                Ecb.AddComponent(chunkIndex, Blocks[(int)(Pos.y * WorldSize.x + Pos.x)], new ComponentType(typeof(Disabled)));
            }
            Pos.x++;
            if (Pos.x > WorldSize.x-1)
            {
                Pos.y++;
                Pos.x = 0;
            }

        }
    }
   
}
