using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// La position de la souris sur l'�cran
/// Une instance seule de ce composant doit exister
/// </summary>
public struct DirectionComponent : IComponentData
{
    public float2 Value;
}
