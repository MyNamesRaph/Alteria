using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

/// <summary>
/// Singleton permettant de gérer l'inventaire
/// </summary>
public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public InventorySlot[] slots;
    public int selectedSlot { get; private set; } = 0;

    public TextMeshProUGUI itemLabel;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SelectSlot(selectedSlot);
    }

    /// <summary>
    /// Selectionne l'emplacement d'inventaire
    /// </summary>
    /// <param name="index">L'index à partir de 0 de l'emplacement à sélectionner</param>
    public void SelectSlot(int index)
    {
        if (index >= slots.Length) throw new ArgumentException("The slot index " + index + " does not exist!");

        foreach (InventorySlot slot in slots)
        {
            slot.SetSelected(false);
        }
        slots[index].SetSelected(true);
        selectedSlot = index;

        itemLabel.text = slots[index].item.Name;
    }
}
