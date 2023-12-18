using System.Collections.Generic;
using ScriptableObjects.Ingredients;
using ScriptableObjects.Preparations;

namespace Save
{
    public class PlayerSave
    {
        // List of inventory ingredients (not sorted by storage type)
        public Dictionary<IngredientSO, int> InventoryIngredients = new();

        // List of discovered preparations
        public List<PreparationSO> UnlockedPreparationsSOs = new();
    }
}
