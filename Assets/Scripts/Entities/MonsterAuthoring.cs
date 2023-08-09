using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;

/// <summary>
/// Cr�ateur d'entit� de monstre
/// </summary>
[BurstCompile]
class MonsterAuthoring : MonoBehaviour
{
    public float MoveSpeed;
    public int Health;
}


[BurstCompile]
class MonsterBaker : Baker<MonsterAuthoring>
{
    /// <summary>
    /// Attachement des composants du monstre
    /// </summary>
    public override void Bake(MonsterAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new IsMonsterTag());
        AddComponent(entity, new HealthComponent { Value = authoring.Health});

        // Statistiques
        AddComponent(entity, new MoveSpeedComponent { Speed = authoring.MoveSpeed});

        // �v�nements
        AddComponent(entity, new TakeDamageEvent { DamageToTake = 0 });
    }
}
