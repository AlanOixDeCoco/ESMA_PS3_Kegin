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
    [SerializeField] private Texture2D _texture;
    [SerializeField] private string _name;
    [SerializeField] private StorageTypes _storage;
    [SerializeField] private bool _finalProduct = false;

    public Texture2D Texture { get => _texture; private set => _texture = value; }
    public string Name { get => _name; private set => _name = value; }
    public StorageTypes Storage { get => _storage; private set => _storage = value; }
    public bool FinalProduct { get => _finalProduct; private set => _finalProduct = value; }
}
