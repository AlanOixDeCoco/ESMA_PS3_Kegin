using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StorageTypes
{
    cold,
    dry,
    shelf
}

public class Ingredient
{
    private IngredientSO _ingredientSO;
    private Inventory _inventory;

    public IngredientSO IngredientSO { get => _ingredientSO; }
    public Inventory Inventory { get => _inventory; set => _inventory = value; }

    public Ingredient(IngredientSO ingredientSO, Inventory inventory)
    {
        _ingredientSO = ingredientSO;
        _inventory = inventory;
    }
}

[CreateAssetMenu(fileName = "ingredient_", menuName = "Ingredient/Ingredient")]
public class IngredientSO : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private StorageTypes _storage;
    [SerializeField] private bool _finalProduct = false;

    public Sprite Sprite { get => _sprite; private set => _sprite = value; }
    public string Name { get => _name; private set => _name = value; }
    public StorageTypes Storage { get => _storage; private set => _storage = value; }
    public bool FinalProduct { get => _finalProduct; private set => _finalProduct = value; }
}
