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
    [SerializeField] private IngredientSO[] _ingredients;
    [SerializeField] private IngredientSO _result;

    public ToolTypes Tool { get => _tool; private set => _tool = value; }
    public IngredientSO[] Ingredients { get => _ingredients; private set => _ingredients = value; }
    public IngredientSO Result { get => _result; private set => _result = value; }
}
