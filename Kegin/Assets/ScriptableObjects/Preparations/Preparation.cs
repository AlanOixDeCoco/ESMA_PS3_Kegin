using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolTypes
{
    pan,
    pot,
    oven
}

[CreateAssetMenu(fileName = "preparation_", menuName = "Ingredient/Preparation")]
public class PreparationSO : ScriptableObject
{
    [SerializeField] private ToolTypes _tool;
    [SerializeField] private IngredientSO[] _ingredientsSOs;
    [SerializeField] private IngredientSO _result;

    public ToolTypes Tool { get => _tool; }
    public IngredientSO[] Ingredients { get => _ingredientsSOs; }
    public IngredientSO Result { get => _result; }
}
