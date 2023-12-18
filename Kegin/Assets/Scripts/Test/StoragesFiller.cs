using System.Collections.Generic;
using Managers;
using ScriptableObjects.Ingredients;
using UnityEngine;

namespace Test
{
    public class StoragesFiller : MonoBehaviour
    {
        [SerializeField] private Inventory _shelves, _fridge, _locker;
        [SerializeField] private UIManager _uiManager;

        [SerializeField] private List<IngredientSO> _ingredientsSOs = new();
        [SerializeField] private List<int> _ingredientsQuantities = new();

        private void OnValidate()
        {
            while(_ingredientsQuantities.Count < _ingredientsSOs.Count)
            {
                _ingredientsQuantities.Add(_ingredientsQuantities.Count > 0
                    ? _ingredientsQuantities[^1]
                    : 1);
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
                    case StorageTypes.Dry:
                        for(int i = 0; i < _ingredientsQuantities[_ingredientsSOs.IndexOf(ingredientSO)]; i++)
                        {
                            _locker.AddIngredient(ingredientSO);
                        }
                        break;
                    case StorageTypes.Cold:
                        for (int i = 0; i < _ingredientsQuantities[_ingredientsSOs.IndexOf(ingredientSO)]; i++)
                        {
                            _fridge.AddIngredient(ingredientSO);
                        }
                        break;
                    case StorageTypes.Shelf:
                        for (int i = 0; i < _ingredientsQuantities[_ingredientsSOs.IndexOf(ingredientSO)]; i++)
                        {
                            _shelves.AddIngredient(ingredientSO);
                        }
                        break;
                }
            }
            _uiManager.Setup();
        }
    }
}
