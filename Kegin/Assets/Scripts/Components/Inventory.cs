using System.Collections.Generic;
using ScriptableObjects.Ingredients;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Private exposed variables
    [SerializeField] private string _name = "Inventory";
    [SerializeField] private bool _stack = true;

    // Private variables
    private readonly List<IngredientSO> _ingredientsSOs = new();
    private readonly Dictionary<IngredientSO, int> _ingredientsQuantities = new();

    // Properties
    public List<IngredientSO> Ingredients => _ingredientsSOs;
    public Dictionary<IngredientSO, int> IngredientsQuantities => _ingredientsQuantities;
    public bool Stack => _stack;
    public string Name => _name;

    // Methods
    public bool AddIngredient(IngredientSO ingredientSO)
    {
        if (Stack)
        {
            if (_ingredientsSOs.Contains(ingredientSO)) _ingredientsQuantities[ingredientSO]++;
            else
            {
                _ingredientsSOs.Add(ingredientSO);
                _ingredientsQuantities.Add(ingredientSO, 1);
            }
        }
        else
        {
            if (_ingredientsSOs.Contains(ingredientSO)) return false;
            _ingredientsSOs.Add(ingredientSO);
        }
        return true;
    }

    public void RemoveIngredient(IngredientSO ingredientSO)
    {
        if (Stack)
        {
            _ingredientsQuantities[ingredientSO]--;
            if (_ingredientsQuantities[ingredientSO] == 0)
            {
                _ingredientsQuantities.Remove(ingredientSO);
                _ingredientsSOs.Remove(ingredientSO);
            }
        }
        else
        {
            _ingredientsSOs.Remove(ingredientSO);
        }
    }
}
