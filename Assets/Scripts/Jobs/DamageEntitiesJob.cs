using Unity.Burst;
using Unity.Entities;
using UnityEngine;


/// <summary>
/// Appliquer les dégats aux entitées
/// </summary>
[BurstCompile]
public partial struct DamageEntitiesJob : IJobEntity
{

    [BurstCompile]
    public void Execute(ref TakeDamageEvent damage, ref HealthComponent health)
    {
        if (damage.DamageToTake == 0) return;

        health.Value -= damage.DamageToTake;
        damage.DamageToTake = 0;
    }
   
}
