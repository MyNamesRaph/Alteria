using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Syst�me permettant de g�rer la cam�ra principale de la sc�ne
/// </summary>
public partial class CameraSystem : SystemBase
{
    /// <summary>
    /// V�rification de l'initialisation du joueur
    /// </summary>
    protected override void OnCreate()
    {
        RequireForUpdate<IsPlayerTag>();
    }

    /// <summary>
    /// Pointe la cam�ra sur la position du joueur � chaque frame
    /// </summary>
    protected override void OnUpdate()
    {
        if (CameraSingleton.Instance == null) {
            Debug.Log("No camera");
            return;
        }

        var player = SystemAPI.GetSingletonEntity<IsPlayerTag>();
        var playerPosition = SystemAPI.GetComponent<LocalToWorld>(player).Position;

        var newPosition = CameraSingleton.Instance.cameraOffset + playerPosition;
        CameraSingleton.Instance.transform.position = newPosition;
        
    }
}
