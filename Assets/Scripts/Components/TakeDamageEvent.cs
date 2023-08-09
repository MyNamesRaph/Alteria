using Unity.Entities;

/// <summary>
/// Événement de clique de la souris
/// Une instance seule de ce composant doit exister
/// Est activé lorsque l'action de cliquer de la souris est effectuée et devrait être désactivée lorsque celle-ci est traitée.
/// </summary>
public struct TakeDamageEvent : IComponentData
{
    public int DamageToTake;
}
