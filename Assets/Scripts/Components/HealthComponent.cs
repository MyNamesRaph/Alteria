using Unity.Entities;

/// <summary>
/// Points de vie de l'entité
/// </summary>
public struct HealthComponent : IComponentData
{
    public int Value;
}
