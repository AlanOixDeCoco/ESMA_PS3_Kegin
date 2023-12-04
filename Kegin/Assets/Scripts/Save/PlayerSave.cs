using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave
{
    // List of ingredients (not sorted by storage type)
    public Dictionary<IngredientSO, int> _ingredients;

    // List of unlocked preparations
    public List<PreparationSO> _preparations;
}
