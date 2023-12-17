using System.Collections.Generic;

public class PlayerSave
{
    // List of ingredients (not sorted by storage type)
    public Dictionary<IngredientSO, int> Ingredients;

    // List of unlocked preparations
    public List<PreparationSO> Preparations;
}
