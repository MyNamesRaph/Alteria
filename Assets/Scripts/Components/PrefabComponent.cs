using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// Un pr�fab d'entit� � instancier
/// </summary>
public struct PrefabComponent : IComponentData
{
    public Entity Prefab;
}