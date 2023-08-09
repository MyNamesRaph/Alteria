using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Un emplacement d'inventaire
/// </summary>
public class InventorySlot : MonoBehaviour
{
    public GameObject selected;
    public GameObject unselected;
    public Item item;


    /// <summary>
    /// Changer l'affichage de selection
    /// </summary>
    /// <param name="isSelected"> Si vrai, sera affiché comme selectionné, sinon sera affiché comme non sélectionné</param>
    public void SetSelected(bool isSelected = true)
    {
        selected.SetActive(isSelected);
        unselected.SetActive(!isSelected);
    }
}
