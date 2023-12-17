using ScriptableObjects.Ingredients;
using UnityEngine;

namespace Managers
{
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
}
