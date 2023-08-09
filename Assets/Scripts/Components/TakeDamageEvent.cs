using Unity.Entities;

/// <summary>
/// Événement de prise de dégats
/// La valeur de DamageToTake sera appliqué au HealthComponent de l'entité et remise à zéro.
/// </summary>
public struct TakeDamageEvent : IComponentData
{
    public int DamageToTake;
}
