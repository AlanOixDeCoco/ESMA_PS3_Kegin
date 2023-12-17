using UnityEngine;

namespace ScriptableObjects.Ingredients
{
    public enum StorageTypes
    {
        Cold,
        Dry,
        Shelf
    }

    [CreateAssetMenu(fileName = "ingredient_", menuName = "Ingredient/Ingredient")]
    public class IngredientSO : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _name;
        [SerializeField] private StorageTypes _storage;
        [SerializeField] private bool _saleable;

        public Sprite Sprite => _sprite;
        public string Name => _name;
        public StorageTypes Storage => _storage;
        public bool Saleable => _saleable;
    }
}