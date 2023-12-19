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

        public int Currency = 0;

        public void AddCurrency(int amount)
        {
            Currency += amount;
        }


        public bool TryRemoveCurrency(int amount)
        {
            if (Currency - amount >= 0)
            {
                Currency -= amount;
                return true;
            }

            return false;
        }
    }
}
