using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragManager : MonoBehaviour
{
    // Events
    public UnityEvent<IngredientSO> _startDrag;
    public UnityEvent _endDrag;

    // Properties
    public IngredientSO Ingredient { get; set; }
    public Inventory SourceInventory { get; set; }

    public void StartDrag(IngredientSO ingredient, Inventory source)
    {
        Ingredient = ingredient;
        SourceInventory = source;

        _startDrag.Invoke(Ingredient);
    }
}
