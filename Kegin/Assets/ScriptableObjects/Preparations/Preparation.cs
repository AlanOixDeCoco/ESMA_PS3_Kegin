using ScriptableObjects.Ingredients;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects.Preparations
{
    public enum ToolTypes
    {
        Pan,
        Pot,
        Oven
    }

    [CreateAssetMenu(fileName = "preparation_", menuName = "Ingredient/Preparation")]
    public class PreparationSO : ScriptableObject
    {
        [SerializeField] private ToolTypes _tool;
        [SerializeField] private IngredientSO[] _ingredientsSOs;
        [FormerlySerializedAs("_result")] [SerializeField] private IngredientSO _resultIngredientSO;

        public ToolTypes Tool => _tool;
        public IngredientSO[] Ingredients => _ingredientsSOs;
        public IngredientSO ResultIngredientSO => _resultIngredientSO;
    }
}