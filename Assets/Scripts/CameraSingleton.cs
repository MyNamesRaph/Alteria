using UnityEngine;
using Unity.Mathematics;

/// <summary>
/// Singleton permettant d'acc�der � la cam�ra depuis un syst�me
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
