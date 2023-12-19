using System;
using System.Collections.Generic;
using ScriptableObjects.Ingredients;
using ScriptableObjects.Preparations;
using UnityEngine;

namespace Managers
{
    public class CookingManager : MonoBehaviour
    {
        [SerializeField] private PreparationSO[] _preparationsSOs;
        [SerializeField] private IngredientSO _badResultIngredientSO;
        [SerializeField] private BookUI _bookUI;

        private readonly Dictionary<IngredientSO, List<PreparationSO>> _ingredientsRelations = new();

        private SaveManager _saveManager;

        public PreparationSO[] PreparationsSOs => _preparationsSOs;

        private void Start()
        {
            _saveManager = GetComponent<SaveManager>();
            
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
        }

        private bool TryGetPreparation(in List<IngredientSO> inIngredientSOs, out PreparationSO outPreparationSO)
        {
            // New algorithm -->
            // 1 - Get the list of all the possible preparations based on the number of ingredients --> If none : return fail
            var possiblePreparationsSOs = new List<PreparationSO>();
            foreach (var preparationSO in _preparationsSOs)
            {
                if(preparationSO.Ingredients.Length == inIngredientSOs.Count) possiblePreparationsSOs.Add(preparationSO);
            }
            
            if(possiblePreparationsSOs.Count == 0)
            {
                // We didn't find a possible preparation
                outPreparationSO = null;
                return false;
            }
            
            // 2 - Remove every preparation that doesn't contain one of the ingredients
            foreach (var possiblePreparationSO in possiblePreparationsSOs)
            {
                bool isPreparationPossible = true;
                foreach (var ingredientSO in inIngredientSOs)
                {
                    isPreparationPossible &= Array.IndexOf(possiblePreparationSO.Ingredients, ingredientSO) != -1;
                }
                if (!isPreparationPossible) continue;
                
                outPreparationSO = possiblePreparationSO;
                return true;
            }
                
            // We didn't find a possible preparation
            outPreparationSO = null;
            return false;
        }

        public IngredientSO GetPreparationResult(in List<IngredientSO> ingredientSOs)
        {
            if (TryGetPreparation(ingredientSOs, out var preparationSO))
            {
                // --> If already unlocked
                if(_saveManager.PlayerSave.UnlockedPreparationsSOs.Contains(preparationSO)) 
                    return preparationSO.ResultIngredientSO; // Then return the preparaion result immediately
                
                // --> If not unlocked already
                _saveManager.PlayerSave.UnlockedPreparationsSOs.Add(preparationSO);
                _saveManager.Save();
                _bookUI.DisplayNewPreparation(); // Open book UI to see new preparation
                
                // Then return the preparaion result
                return preparationSO.ResultIngredientSO;
            }

            // If there were no preparation found
            return _badResultIngredientSO;
        }
    }
}