using Unity.Entities;

/// <summary>
/// �v�nement de saut du joueur
/// Une instance seule de ce composant doit exister
/// Est activ� lorsque l'action de saut est effectu�e et devrait �tre d�sactiv�e lorsque celle-ci est trait�e.
/// </summary>
public struct PlayerJumpEvent : IComponentData,IEnableableComponent
{
    // Player should jump when component is active.
}
