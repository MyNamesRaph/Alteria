using Unity.Entities;
using Unity.Mathematics;


/// <summary>
/// Vecteur de direction des d�placements effectu�s par l'utilisateur
/// Une instance seule de ce composant doit exister
/// </summary>
public struct PlayerMovementInputComponent : IComponentData
{
    public float2 Value;
}
