using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private IngredientSO[] _ingredientsSOs;

    public void Setup()
    {
        _inventoryUI.Setup(_ingredientsSOs);
    }
}
