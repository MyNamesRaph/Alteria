using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// Direction de déplacement de l'entité
/// </summary>
public struct DirectionComponent : IComponentData
{
    public float2 Value;
}
