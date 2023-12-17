using ScriptableObjects.Ingredients;
using UnityEngine;

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
        [SerializeField] private IngredientSO _result;

        public ToolTypes Tool => _tool;
        public IngredientSO[] Ingredients => _ingredientsSOs;
        public IngredientSO Result => _result;
    }
}