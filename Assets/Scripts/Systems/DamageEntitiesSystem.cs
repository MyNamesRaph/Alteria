using Unity.Entities;
using Unity.Physics.Systems;
using Unity.Burst;

/// <summary>
/// Syst�me d'application des d�gats aux entit�s
/// </summary>
[BurstCompile]
public partial struct DamageEntitiesSystem : ISystem
{
    /// <summary>
    /// Appliquer les d�gats au entit�es
    /// </summary>
    /// <param name="state"></param>
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        new DamageEntitiesJob
        {

        }.Run();
    }
}
