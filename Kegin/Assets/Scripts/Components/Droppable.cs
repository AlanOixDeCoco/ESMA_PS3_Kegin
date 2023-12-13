using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Inventory))]
public class Droppable : MonoBehaviour
{
    [SerializeField] private UnityEvent<Transform> _onDropConfirmed, _onDropImpossible;

    private Inventory _inventory;

    public Inventory Inventory { get => _inventory; set => _inventory = value; }

    private void Start()
    {
        Inventory = GetComponent<Inventory>();
    }

    public void Drop(IngredientSO ingredientSO, Inventory originInventory)
    {
        if (Inventory.AddIngredient(ingredientSO))
        {
            originInventory.RemoveIngredient(ingredientSO);
            _onDropConfirmed.Invoke(transform);
        }
        else _onDropImpossible.Invoke(transform);
    }
}
