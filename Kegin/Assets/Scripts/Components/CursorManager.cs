using Cinemachine;
using ScriptableObjects.Ingredients;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum CursorStates
{
    Interact,
    UI,
    Drag
}

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineBrain _cameraBrain;
    [FormerlySerializedAs("_draggedIngredientImage")] [SerializeField] private RectTransform _draggedIngredientRectTransform;
    [SerializeField] private Inventory _dryInventory, _shelvesInventory, _fridgeInventory;

    [SerializeField] private UnityEvent _onInteractState, _onUIState, _onDragState;
    
    private CursorStates _cursorState = CursorStates.Interact;
    private IngredientSO _draggedIngredientSO;
    private Inventory _draggedInventoryOrigin;
    private Droppable _droppable;
    private bool _canDrop;
    private Draggable _draggedIngredientDraggable;

    private void Start()
    {
        _draggedIngredientDraggable = _draggedIngredientRectTransform.GetComponent<Draggable>();
    }

    private void Update()
    {
        switch (_cursorState)
        {
            case CursorStates.Interact:
                Interact();
                break;
            case CursorStates.UI:
                break;
            case CursorStates.Drag:
                Drag();
                break;
        }
    }

    private void Interact()
    {
        // Check if the camera is blending
        if (_cameraBrain.IsBlending) return;
        
        // Check if a touch is being registered
        if (Input.touchCount == 0) return;

        var touchPos = Input.GetTouch(0).position;

        var ray = _camera.ScreenPointToRay(touchPos);
        
        if (!Physics.Raycast(ray, out var hit)) return;

        if(hit.collider.TryGetComponent<Interactive>(out var interactiveComponent))
            interactiveComponent.Interact();
    }

    private void Drag()
    {
        // If no touch is registered (we were registering one in the previous state)
        if (Input.touchCount == 0)
        {
            // drop item if it can be dropped
            if (_canDrop) _droppable.Drop(_draggedIngredientSO, _draggedInventoryOrigin);
            else
            {
                _draggedInventoryOrigin.RemoveIngredient(_draggedIngredientSO);
                _draggedInventoryOrigin = _draggedIngredientSO.Storage switch
                {
                    StorageTypes.Dry => _dryInventory,
                    StorageTypes.Shelf => _shelvesInventory,
                    StorageTypes.Cold => _fridgeInventory,
                    _ => _draggedInventoryOrigin
                };
                _draggedInventoryOrigin.AddIngredient(_draggedIngredientSO);
            }

            SetInteractState();
            return;
        }

        var touchPos = Input.GetTouch(0).position;
        _draggedIngredientRectTransform.position = touchPos;

        var ray = _camera.ScreenPointToRay(touchPos);
        if (!Physics.Raycast(ray, out var hit))
        {
            _canDrop = false;
        }
        else
        {
            if(hit.collider.TryGetComponent<Interactive>(out var interactiveComponent))
                if(interactiveComponent.ActiveDuringDrag) interactiveComponent.Interact();
            
            _canDrop = hit.collider.TryGetComponent(out _droppable);
            if (_canDrop)
            {
                _droppable.TryGetComponent<Inventory>(out var inventoryComponent);
                _canDrop = !inventoryComponent.Ingredients.Contains(_draggedIngredientSO);
                _canDrop  &= interactiveComponent.Interactable;
                
                if(interactiveComponent.ActiveDuringDrag) interactiveComponent.Interact();
            }
        }
        
        _draggedIngredientDraggable.SetDroppableHint(_canDrop);
    }

    public void SetInteractState()
    {
        _draggedIngredientRectTransform.gameObject.SetActive(false);
        _cursorState = CursorStates.Interact;
        _onInteractState.Invoke();
    }
    public void SetUIState()
    {
        _cursorState = CursorStates.UI;
        _onUIState.Invoke();
    }
    public void SetDragState(IngredientSO ingredientSO, Inventory inventory)
    {
        _cursorState = CursorStates.Drag;
        _draggedIngredientSO = ingredientSO;
        _draggedInventoryOrigin = inventory;
        _draggedIngredientDraggable._onBeginDrag.Invoke(ingredientSO);
        _draggedIngredientRectTransform.Find("img_Ingredient").GetComponent<Image>().sprite = ingredientSO.Sprite;
        _draggedIngredientRectTransform.Find("text_Name").GetComponent<TextMeshProUGUI>().text = ingredientSO.Name;
        _draggedIngredientRectTransform.position = Input.GetTouch(0).position;
        _draggedIngredientRectTransform.gameObject.SetActive(true);
        _onDragState.Invoke();
    }
}
