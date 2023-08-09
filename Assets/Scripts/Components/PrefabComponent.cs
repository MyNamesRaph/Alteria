using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// Un préfab d'entité à instancier
/// </summary>
public struct PrefabComponent : IComponentData
{
    public Entity Prefab;
}