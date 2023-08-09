using Unity.Entities;

/// <summary>
/// Événement de saut du joueur
/// Une instance seule de ce composant doit exister
/// Est activé lorsque l'action de saut est effectuée et devrait être désactivée lorsque celle-ci est traitée.
/// </summary>
public struct PlayerJumpEvent : IComponentData,IEnableableComponent
{
    // Player should jump when component is active.
}
