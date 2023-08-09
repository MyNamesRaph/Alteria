using Unity.Entities;

/// <summary>
/// �v�nement de clique de la souris
/// Une instance seule de ce composant doit exister
/// Est activ� lorsque l'action de cliquer de la souris est effectu�e et devrait �tre d�sactiv�e lorsque celle-ci est trait�e.
/// </summary>
public struct TakeDamageEvent : IComponentData
{
    public int DamageToTake;
}
