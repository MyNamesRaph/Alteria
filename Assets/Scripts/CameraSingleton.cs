using UnityEngine;
using Unity.Mathematics;

/// <summary>
/// Singleton permettant d'accéder à la caméra depuis un système
/// </summary>
public class CameraSingleton : MonoBehaviour
{
    public static CameraSingleton Instance;
    public float3 cameraOffset = new(0,0,-20);

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
