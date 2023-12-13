using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragesFiller : MonoBehaviour
{
    [SerializeField] private Inventory _shelves, _fridge, _locker;
    [SerializeField] private UIManager _uiManager;

    [SerializeField] private List<IngredientSO> _ingredientsSOs = new List<IngredientSO>();
    [SerializeField] private List<int> _ingredientsQuantities = new List<int>();

    private void OnValidate()
    {
        while(_ingredientsQuantities.Count < _ingredientsSOs.Count)
        {
            if (_ingredientsQuantities.Count > 0) _ingredientsQuantities.Add(_ingredientsQuantities[_ingredientsQuantities.Count - 1]);
            else _ingredientsQuantities.Add(1);
        }
        while (_ingredientsQuantities.Count > _ingredientsSOs.Count)
        {
            _ingredientsQuantities.RemoveAt(_ingredientsQuantities.Count - 1);
        }
    }

    private void Start()
    {
        foreach (var ingredientSO in _ingredientsSOs)
        {
            switch (ingredientSO.Storage)
            {
                case StorageTypes.dry:
                    for(int i = 0; i < _ingredientsQuantities[_ingredientsSOs.IndexOf(ingredientSO)]; i++)
                    {
                        _locker.AddIngredient(ingredientSO);
                    }
                    break;
                case StorageTypes.cold:
                    for (int i = 0; i < _ingredientsQuantities[_ingredientsSOs.IndexOf(ingredientSO)]; i++)
                    {
                        _fridge.AddIngredient(ingredientSO);
                    }
                    break;
                case StorageTypes.shelf:
                    for (int i = 0; i < _ingredientsQuantities[_ingredientsSOs.IndexOf(ingredientSO)]; i++)
                    {
                        _shelves.AddIngredient(ingredientSO);
                    }
                    break;
                default:
                    break;
            }
        }

        _uiManager.Setup();
    }
}
