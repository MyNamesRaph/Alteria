using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// Système de génération du monde
/// </summary>
[BurstCompile]
public partial struct GenerateWorldSystem : ISystem
{
    float2 WorldSize;
    bool generated;

    /// <summary>
    /// Générer le monde
    /// </summary>
    /// <param name="state"></param>
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (generated) return;
        
        var blockQuery = SystemAPI.QueryBuilder()
            .WithAll<IsBlockTag>()
            .WithOptions(EntityQueryOptions.IncludeDisabledEntities)
            .Build();
        if (blockQuery.IsEmpty)
        {
            // Instancier toutes les entités du monde afin d'éviter les changements structurels lors de la génération
            foreach (var (prefab, size) in
            SystemAPI.Query<
                RefRO<PrefabComponent>,
                RefRO<WorldSizeComponent>
                >())
            {
                InstantiateAllBlocks(ref state, prefab, size);
                WorldSize = size.ValueRO.Size;
            }
        }
        else
        {
            // Générer le monde
            NativeArray<Entity> blocks = blockQuery.ToEntityArray(Allocator.Persistent);

            (float halfXSize, float halfYSize) = (WorldSize.x, WorldSize.y);
            float2 originOffset = (halfYSize - halfXSize);

            var job = new AlignWorldJob
            {
                WorldSize = WorldSize,
                Pos = float2.zero,
                Offset = originOffset
            };

            job.Schedule();
            generated = true;

            EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state);

            new GenerateCavesJob
            {
                Ecb = ecb,
                Seed = UnityEngine.Random.Range(0,int.MaxValue),
                Blocks = blocks,
                WorldSize = WorldSize,
                Pos = float2.zero
            }.Schedule();
        }


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

    /// <summary>
    /// Instantier tous les blocks nécéssaires à la construction du monde
    /// </summary>
    /// <param name="state"></param>
    /// <param name="prefab">Préfab de block à instancier</param>
    /// <param name="size">Grandeur du monde</param>
    [BurstCompile]
    private void InstantiateAllBlocks(
        ref SystemState state,
        RefRO<PrefabComponent> prefab,
        RefRO<WorldSizeComponent> size
        )
    {
        float2 worldSize = size.ValueRO.Size;
        int numEntities = (int)(worldSize.x * worldSize.y);
        NativeArray<Entity> entities = state.EntityManager.Instantiate(prefab.ValueRO.Prefab, numEntities, Allocator.Temp);
    }

    
}
