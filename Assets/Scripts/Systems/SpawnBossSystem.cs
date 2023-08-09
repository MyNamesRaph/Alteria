using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;


/// <summary>
/// Système pour spawner le boss
/// </summary>
[BurstCompile]
public partial struct SpawnBossSystem : ISystem
{
    bool spawned;


    /// <summary>
    /// Faire apparaitre le boss lorsque tous les monstres sont tués
    /// </summary>
    /// <param name="state"></param>
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (spawned) return;

        var query = SystemAPI.QueryBuilder().WithAll<IsMonsterTag>().Build();

        if (query.IsEmpty)
        {
            foreach (var (_,prefab) in SystemAPI.Query<RefRO<IsBossSpawnerTag>, RefRO<PrefabComponent>>())
            {
                var boss = state.EntityManager.Instantiate(prefab.ValueRO.Prefab);
                state.EntityManager.SetComponentData(boss, LocalTransform.FromPosition(new(0, 50, 0)));
            }

            spawned = true;

        }

    }
}
