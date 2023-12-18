using ScriptableObjects.Ingredients;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("Inventory UI")]
        [SerializeField] private InventoryUI[] _inventoryUIs;
        [SerializeField] private IngredientSO[] _ingredientsSOs;

        public void Setup()
        {
            foreach (var inventoryUI in _inventoryUIs)
            {
                inventoryUI.Setup(_ingredientsSOs);
            }
        }
    }
}
