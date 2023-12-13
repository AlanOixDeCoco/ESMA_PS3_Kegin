using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragesFiller : MonoBehaviour
{
    [SerializeField] private Inventory shelves, fridge, locker;

    [SerializeField] private List<IngredientSO> _ingredients = new List<IngredientSO>();
    [SerializeField] private List<int> _ingredientsQuantities = new List<int>();

    private void OnValidate()
    {
        while(_ingredientsQuantities.Count < _ingredients.Count)
        {
            if (_ingredientsQuantities.Count > 0) _ingredientsQuantities.Add(_ingredientsQuantities[_ingredientsQuantities.Count - 1]);
            else _ingredientsQuantities.Add(1);
        }
        while (_ingredientsQuantities.Count > _ingredients.Count)
        {
            _ingredientsQuantities.RemoveAt(_ingredientsQuantities.Count - 1);
        }
    }

    private void Start()
    {
        foreach(var ingredient in _ingredients)
        {
            switch (ingredient.Storage)
            {
                case StorageTypes.dry:
                    for(int i = 0; i < _ingredientsQuantities[_ingredients.IndexOf(ingredient)]; i++)
                    {
                        locker.AddIngredient(ingredient);
                    }
                    break;
                case StorageTypes.cold:
                    for (int i = 0; i < _ingredientsQuantities[_ingredients.IndexOf(ingredient)]; i++)
                    {
                        fridge.AddIngredient(ingredient);
                    }
                    break;
                case StorageTypes.shelf:
                    for (int i = 0; i < _ingredientsQuantities[_ingredients.IndexOf(ingredient)]; i++)
                    {
                        shelves.AddIngredient(ingredient);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
