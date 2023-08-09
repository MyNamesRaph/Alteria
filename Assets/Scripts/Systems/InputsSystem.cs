using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Syst�me permettant de g�rer les actions de l'utilisateur
/// </summary>
[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class InputsSystem : SystemBase
{
    private Inputs inputs;
    private Entity playerEntity;

    /// <summary>
    /// V�rification de l'initialisation de la sc�ne
    /// </summary>
    protected override void OnCreate()
    {
        RequireForUpdate<IsPlayerTag>();
        RequireForUpdate<PlayerMovementInputComponent>();
        RequireForUpdate<MouseInputComponent>();

        inputs = new Inputs();
    }

    /// <summary>
    /// Initialisation des actions et �v�nements
    /// </summary>
    protected override void OnStartRunning()
    {
        inputs.Enable();
        playerEntity = SystemAPI.GetSingletonEntity<IsPlayerTag>();

        inputs.Player.Jump.performed += OnPlayerJump;
        inputs.Player.Select.performed += OnMouseClick;
        inputs.Player.SelectSlot.performed += OnSelectSlot;
    }

    /// <summary>
    /// Mets � jour les composants de stockage des valeurs d'actions � chaque frame
    /// </summary>
    protected override void OnUpdate()
    {
        Vector2 movementInput = inputs.Player.Movement.ReadValue<Vector2>();
        Vector2 mouseInput = inputs.Player.Mouse.ReadValue<Vector2>();

        SystemAPI.SetSingleton(new PlayerMovementInputComponent { Value = movementInput });
        SystemAPI.SetSingleton(new MouseInputComponent { Value = mouseInput });
    }

    /// <summary>
    /// D�sactivation des actions et des �v�nements
    /// </summary>
    protected override void OnStopRunning()
    {
        inputs.Disable();

        inputs.Player.Jump.performed -= OnPlayerJump;
        inputs.Player.Select.performed -= OnMouseClick;
        inputs.Player.SelectSlot.performed -= OnSelectSlot;

    }

    /// <summary>
    /// Active le composant d'�v�nement de saut du joueur
    /// Appell� par l'action Player.Jump
    /// </summary>
    /// <param name="obj"></param>
    private void OnPlayerJump(InputAction.CallbackContext obj)
    {
        if (!SystemAPI.Exists(playerEntity)) return;

        SystemAPI.SetComponentEnabled<PlayerJumpEvent>(playerEntity, true);
    }

    /// <summary>
    /// Active le composant d'�v�nement de clique de la souris
    /// Appell� par l'action Player.Select
    /// </summary>
    /// <param name="obj"></param>
    private void OnMouseClick(InputAction.CallbackContext obj)
    {
        if (!SystemAPI.Exists(playerEntity)) return;

        SystemAPI.SetComponentEnabled<MouseClickEvent>(playerEntity, true);
    }

    /// <summary>
    /// Met � jour l'emplacement d'inventaire s�lectionn�
    /// Appell� par l'action Player.SelectSlot
    /// </summary>
    /// <param name="obj"></param>
    private void OnSelectSlot(InputAction.CallbackContext obj)
    {
        Inventory.Instance.SelectSlot((int)obj.ReadValue<float>());
    }
}
