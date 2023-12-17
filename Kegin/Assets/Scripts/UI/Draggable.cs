using System.Collections;
using ScriptableObjects.Ingredients;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _dragConfirmDelay = .5f;

    public UnityEvent<IngredientSO> _onBeginDrag;

    private IngredientSO _ingredientSO;
    private Image _draggableImage;
    private bool _isPointerDown;

    private void Start()
    {
        _draggableImage = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
        StartCoroutine(ConfirmDrag());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false;
    }

    private IEnumerator ConfirmDrag()
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
        _draggableImage.color = droppable ? Color.green : Color.red;
    }
}
