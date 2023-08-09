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
    /// <param name="isSelected"> Si vrai, sera affich� comme selectionn�, sinon sera affich� comme non s�lectionn�</param>
    public void SetSelected(bool isSelected = true)
    {
        selected.SetActive(isSelected);
        unselected.SetActive(!isSelected);
    }
}
