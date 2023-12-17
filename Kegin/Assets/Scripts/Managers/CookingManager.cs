using System.Collections.Generic;
using ScriptableObjects.Ingredients;
using ScriptableObjects.Preparations;
using UnityEngine;

namespace Managers
{
    public class CookingManager : MonoBehaviour
    {
        [SerializeField] private PreparationSO[] _preparationsSOs;

        private readonly Dictionary<IngredientSO, List<PreparationSO>> _ingredientsRelations = new();

        private void Start()
        {
            foreach (var preparationSO in _preparationsSOs)
            {
                foreach(var ingredientSO in preparationSO.Ingredients)
                {
                    // If this ingredient doesn't already has this entry we create the list first
                    if (!_ingredientsRelations.ContainsKey(ingredientSO)) _ingredientsRelations.Add(ingredientSO, new List<PreparationSO>());
                    
                    // Then in both cases we add the preparation
                    _ingredientsRelations[ingredientSO].Add(preparationSO);
                }
            }
            Debug.Log(_ingredientsRelations.Count.ToString());
        }
    }
}
