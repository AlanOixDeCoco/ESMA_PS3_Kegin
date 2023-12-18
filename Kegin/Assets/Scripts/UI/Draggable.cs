using ScriptableObjects.Ingredients;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image _draggableImage;

    public UnityEvent<IngredientSO> _onBeginDrag;

    private IngredientSO _ingredientSO;

    public void OnPointerDown(PointerEventData eventData)
    {
        _onBeginDrag.Invoke(_ingredientSO);
    }

    public void Setup(IngredientSO ingredientSO)
    {
        _ingredientSO = ingredientSO;
    }

    public void SetDroppableHint(bool droppable)
    {
        _draggableImage.color = droppable ? Color.white : Color.red;
    }
}
