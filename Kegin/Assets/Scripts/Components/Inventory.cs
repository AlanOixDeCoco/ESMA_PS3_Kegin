using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Private exposed variables
    [SerializeField] private List<IngredientSO> _ingredients;
    [SerializeField] private Dictionary<IngredientSO, int> _ingredientsQuantities;
    [SerializeField] private bool _stack = true;

    // Properties
    public List<IngredientSO> Ingredients { get => _ingredients; private set => _ingredients = value; }
    public Dictionary<IngredientSO, int> IngredientsQuantities { get => _ingredientsQuantities; private set => _ingredientsQuantities = value; }

    // Methods
    public void AddIngredient(IngredientSO ingredient)
    {
        if (_stack)
        {
            if (_ingredients.Contains(ingredient)) _ingredientsQuantities[ingredient]++;
            else
            {
                _ingredients.Add(ingredient);
                _ingredientsQuantities.Add(ingredient, 1);
            }
        }
        else
        {
            _ingredients.Add(ingredient);
        }
    }

    public void RemoveIngredient(IngredientSO ingredient)
    {
        if (_stack)
        {
            _ingredientsQuantities[ingredient]--;
            if (_ingredientsQuantities[ingredient] == 0)
            {
                _ingredientsQuantities.Remove(ingredient);
                _ingredients.Remove(ingredient);
            }
        }
        else
        {
            _ingredients.Remove(ingredient);
        }
    }
}
