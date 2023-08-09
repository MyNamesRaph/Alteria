using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Système permettant de gérer la caméra principale de la scène
/// </summary>
public partial class CameraSystem : SystemBase
{
    /// <summary>
    /// Vérification de l'initialisation du joueur
    /// </summary>
    protected override void OnCreate()
    {
        RequireForUpdate<IsPlayerTag>();
    }

    /// <summary>
    /// Pointe la caméra sur la position du joueur à chaque frame
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
