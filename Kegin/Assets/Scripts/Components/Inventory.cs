using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Private exposed variables
    [SerializeField] private string _name = "Inventory";
    [SerializeField] private bool _stack = true;

    // Private variables
    private List<IngredientSO> _ingredientsSOs = new List<IngredientSO>();
    private Dictionary<IngredientSO, int> _ingredientsQuantities = new Dictionary<IngredientSO, int>();

    // Properties
    public List<IngredientSO> Ingredients { get => _ingredientsSOs; private set => _ingredientsSOs = value; }
    public Dictionary<IngredientSO, int> IngredientsQuantities { get => _ingredientsQuantities; private set => _ingredientsQuantities = value; }
    public bool Stack { get => _stack; private set => _stack = value; }
    public string Name { get => _name; }

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
