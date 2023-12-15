using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StorageTypes
{
    cold,
    dry,
    shelf
}

[CreateAssetMenu(fileName = "ingredient_", menuName = "Ingredient/Ingredient")]
public class IngredientSO : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private StorageTypes _storage;
    [SerializeField] private bool _saleable = false;

    public Sprite Sprite { get => _sprite; }
    public string Name { get => _name; }
    public StorageTypes Storage { get => _storage; }
    public bool Saleable { get => _saleable; }
}
