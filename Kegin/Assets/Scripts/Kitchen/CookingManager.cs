using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    [SerializeField] private PreparationSO[] _preparationsSOs;

    private Dictionary<IngredientSO, PreparationSO> _ingredientsRelations;

    private void Start()
    {
        foreach(var preparationSO in _preparationsSOs)
        {
            
        }
    }
}
