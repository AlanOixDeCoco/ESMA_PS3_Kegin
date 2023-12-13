using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PanelComponent))]
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform _ingredientsContainer;
    [SerializeField] private GameObject _ingredientPrefab;

    private void Awake()
    {
        ClearInventoryUI();
    }

    public void OpenInventoryUI(Transform inventoryTransform)
    {
        ClearInventoryUI();

        var inventory = inventoryTransform.GetComponent<Inventory>();

        foreach(var ingredient in inventory.Ingredients)
        {
            var ingredientBtn = Instantiate(_ingredientPrefab, _ingredientsContainer);
            ingredientBtn.name = $"btn_Ingredient ({ingredient.Name})";
        }

        GetComponent<PanelComponent>().OpenPanel();
    }

    public async void CloseInventoryUI()
    {
        await GetComponent<PanelComponent>().ClosePanel();
        ClearInventoryUI();
    }

    private void ClearInventoryUI()
    {
        foreach (Transform child in _ingredientsContainer)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}
