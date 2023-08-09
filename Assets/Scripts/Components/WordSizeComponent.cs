using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// La grandeur d'un monde
/// </summary>
public struct WorldSizeComponent : IComponentData
{
    public float2 Size;
}