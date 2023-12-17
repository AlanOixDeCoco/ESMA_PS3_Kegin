using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _dragConfirmDelay = .5f;

    public UnityEvent<IngredientSO> _onBeginDrag;

    private IngredientSO _ingredientSO;

    private bool _isPointerDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
        StartCoroutine(ConfirmDrag());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false;
    }

    public IEnumerator ConfirmDrag()
    {
        yield return new WaitForSeconds(_dragConfirmDelay);
        if(_isPointerDown)
        {
            _onBeginDrag.Invoke(_ingredientSO);
        }
    }

    public void Setup(IngredientSO ingredientSO)
    {
        _ingredientSO = ingredientSO;
    }

    public void SetDroppableHint(bool droppable)
    {
        GetComponent<Image>().color = droppable ? Color.green : Color.red;
    }
}
