using Unity.Entities;
using Unity.Burst;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;
using Unity.Collections;

/// <summary>
/// Système de détection des collisions des projectiles
/// </summary>
[BurstCompile]
[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateAfter(typeof(PhysicsSystemGroup))]
public partial struct HandleProjectileCollisionSystem : ISystem
{
    /// <summary>
    /// Vérifier si un projectile fait une collision 
    /// </summary>
    /// <param name="state"></param>
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {  
        var ecb = GetEntityCommandBuffer(ref state);
        var projectileLookup = SystemAPI.GetComponentLookup<IsProjectileTag>(true);
        var playerLookup = SystemAPI.GetComponentLookup<IsPlayerTag>(true);
        var monstersLookup = SystemAPI.GetComponentLookup<IsMonsterTag>(true);
        var damageLookup = SystemAPI.GetComponentLookup<DamageComponent>(true);

        var job = new ProjectileTriggerEventsJob
        {
            Ecb = ecb,
            ProjectileLookup = projectileLookup,
            PlayerLookup = playerLookup,
            MonsterLookup = monstersLookup,
            DamageLookup = damageLookup,
        };
        job.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency).Complete();

    }

    /// <summary>
    /// Créer un EntityCommandBuffer à partir du state
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    [BurstCompile]
    private EntityCommandBuffer GetEntityCommandBuffer(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        return ecb;
    }

    /// <summary>
    /// Tâche de gestion des collisions de projectiles
    /// </summary>
    [BurstCompile]
    public partial struct ProjectileTriggerEventsJob : ITriggerEventsJob
    {
        public EntityCommandBuffer Ecb;
        [ReadOnly] public ComponentLookup<IsProjectileTag> ProjectileLookup;
        [ReadOnly] public ComponentLookup<IsPlayerTag> PlayerLookup;
        [ReadOnly] public ComponentLookup<IsMonsterTag> MonsterLookup;
        [ReadOnly] public ComponentLookup<DamageComponent> DamageLookup;

        public void Execute(TriggerEvent trigger)
        {
            if (PlayerLookup.HasComponent(trigger.EntityA)
                || PlayerLookup.HasComponent(trigger.EntityB))
                return; // Ignorer les collisions avec le joueur


            if (ProjectileLookup.HasComponent(trigger.EntityA))
            {
                if (ProjectileLookup.HasComponent(trigger.EntityB)) return;// Ignorer les collisions entre projectiles
                //Le projectile est l'entité A
                Ecb.DestroyEntity(trigger.EntityA);
                
                // Si l'autre entité est un monstre, lui faire des dégats
                if (MonsterLookup.HasComponent(trigger.EntityB))
                {
                    var damage = DamageLookup.GetRefRO(trigger.EntityA).ValueRO.Value;
                    Ecb.SetComponent(trigger.EntityB,new TakeDamageEvent { DamageToTake = damage });
                }
            }
            else if (ProjectileLookup.HasComponent(trigger.EntityB))
            {
                if (ProjectileLookup.HasComponent(trigger.EntityA)) return;// Ignorer les collisions entre projectiles
                //Le projectile est l'entité B
                Ecb.DestroyEntity(trigger.EntityB);

                // Si l'autre entité est un monstre, lui faire des dégats
                if (MonsterLookup.HasComponent(trigger.EntityA))
                {
                    var damage = DamageLookup.GetRefRO(trigger.EntityB).ValueRO.Value;
                    Ecb.SetComponent(trigger.EntityA, new TakeDamageEvent { DamageToTake = damage });
                }
            }
            // Ignorer les collisions qui n'incluent pas de projectile
        }
    }
}


