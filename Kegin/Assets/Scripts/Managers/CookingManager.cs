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

        private readonly Dictionary<IngredientSO, List<PreparationSO>> _ingredientsRelations = new();

        private SaveManager _saveManager;
        
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

        private bool TryGetPreparation(in List<IngredientSO> ingredientSOs, out PreparationSO preparationSO)
        {
            foreach (var ingredient in ingredientSOs)
            {
                if(!_ingredientsRelations.ContainsKey(ingredient)) continue;
                foreach (var preparation in _ingredientsRelations[ingredient])
                {
                    // If the number of ingredients does not match --> Skip
                    if(preparation.Ingredients.Length != ingredientSOs.Count) continue;
                    
                    // If the ingredients do not match --> Skip

                    bool ingredientsMatch = false;
                    foreach (var preparationIngredient in preparation.Ingredients)
                    {
                        ingredientsMatch = ingredientSOs.Contains(preparationIngredient);
                    }
                    if (!ingredientsMatch) continue;
                    
                    // If we haven't skip yet the prep is possible (same num of ingredients & same ingredients)
                    preparationSO = preparation;
                    return true;
                }
            }
            // We didn't find a possible preparation
            preparationSO = null;
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
                
                // Then return the preparaion result
                return preparationSO.ResultIngredientSO;
            }

            // If there were no preparation found
            return _badResultIngredientSO;
        }
    }
}