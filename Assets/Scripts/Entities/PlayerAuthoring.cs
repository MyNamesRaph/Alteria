using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;

/// <summary>
/// Créateur de l'entité Joueur
/// </summary>
[BurstCompile]
class PlayerAuthoring : MonoBehaviour
{
    public float MoveSpeed;
    public float JumpForce;
    public int Health;

    public GameObject ProjectilePrefab;
}


[BurstCompile]
class PlayerBaker : Baker<PlayerAuthoring>
{
    /// <summary>
    /// Attachement des composants du joueurs
    /// </summary>
    public override void Bake(PlayerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new IsPlayerTag());
        
        // Statistiques
        AddComponent(entity, new MoveSpeedComponent { Speed = authoring.MoveSpeed});
        AddComponent(entity, new JumpForceComponent { Force = authoring.JumpForce });
        AddComponent(entity, new HealthComponent { Value = authoring.Health });

        // Attaque
        AddComponent(entity, new PrefabComponent
        { 
            Prefab = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic),
        });
        // Actions
        AddComponent(entity, new PlayerMovementInputComponent());
        AddComponent(entity, new MouseInputComponent());

        AddComponent(entity, new PlayerJumpEvent());
        SetComponentEnabled<PlayerJumpEvent>(entity,false);

        AddComponent(entity, new MouseClickEvent());
        SetComponentEnabled<MouseClickEvent>(entity, false);

        // Autres événements
        AddComponent(entity, new TakeDamageEvent { DamageToTake = 0 });
    }
}
