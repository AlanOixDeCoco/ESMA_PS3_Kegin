using System.Collections.Generic;
using ScriptableObjects.Ingredients;
using ScriptableObjects.Preparations;

namespace Save
{
    public class PlayerSave
    {
        // List of inventory ingredients (not sorted by storage type)
        public readonly Dictionary<IngredientSO, int> InventoryIngredients = new();

        // List of discovered preparations
        public readonly List<PreparationSO> UnlockedPreparationsSOs = new();

        public int Currency { get; private set; }

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
