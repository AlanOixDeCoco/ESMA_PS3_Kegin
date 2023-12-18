using ScriptableObjects.Ingredients;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Inventory))]
public class Droppable : MonoBehaviour
{
    [SerializeField] private UnityEvent<Transform> _onDropConfirmed, _onDropImpossible;

    private Inventory _inventory;

    private void Start()
    {
        _inventory = GetComponent<Inventory>();
    }

    public void Drop(IngredientSO ingredientSO, Inventory originInventory)
    {
        if (_inventory.AddIngredient(ingredientSO))
        {
            originInventory.RemoveIngredient(ingredientSO);
            _onDropConfirmed.Invoke(transform);
        }
        else _onDropImpossible.Invoke(transform);
    }
}
