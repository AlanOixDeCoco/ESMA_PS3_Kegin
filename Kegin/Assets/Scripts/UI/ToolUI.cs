using Kitchen;
using UnityEngine;

public class ToolUI : MonoBehaviour
{
    private InventoryUI _inventoryUI;

    private void Start()
    {
        _inventoryUI = GetComponent<InventoryUI>();
    }

    public void OnCookButtonPressed()
    {
        _inventoryUI.OpenedInventory.GetComponent<Tool>().StartCooking(this);
    }

    public void CloseUI()
    {
        _inventoryUI.CloseInventoryUI();
    }
}
