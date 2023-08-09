using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton permettant d'accéder au sprite du joueur depuis un système
/// </summary>
public class PlayerAnimationSingleton : MonoBehaviour
{
    public static PlayerAnimationSingleton Instance;

    public SpriteRenderer spriteRenderer;

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
