using Kitchen;
using UnityEngine;
using UnityEngine.UI;

public class ToolUI : MonoBehaviour
{
    [SerializeField] private Button _cookButton;
    
    private InventoryUI _inventoryUI;

    private void Start()
    {
        _inventoryUI = GetComponent<InventoryUI>();
    }

    public void CheckCookAbility()
    {
        _cookButton.interactable = _inventoryUI.OpenedInventory.Ingredients.Count > 1;
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
