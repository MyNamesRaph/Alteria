using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton permettant d'acc�der au sprite du joueur � l'int�rieur d'un syst�me
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
