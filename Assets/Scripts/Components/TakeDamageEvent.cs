using Unity.Entities;

/// <summary>
/// �v�nement de prise de d�gats
/// La valeur de DamageToTake sera appliqu� au HealthComponent de l'entit� et remise � z�ro.
/// </summary>
public struct TakeDamageEvent : IComponentData
{
    public int DamageToTake;
}
