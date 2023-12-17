using ScriptableObjects.Ingredients;
using UnityEngine;
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
    [FormerlySerializedAs("_draggedIngredientImage")] [SerializeField] private RectTransform _draggedIngredientRectTransform;
    [SerializeField] private Inventory _dryInventory, _shelvesInventory, _fridgeInventory;

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
            default:
                break;
        }
    }

    private void Interact()
    {
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
            _canDrop = hit.collider.TryGetComponent<Droppable>(out _droppable);
            if (_canDrop)
            {
                _droppable.TryGetComponent<Inventory>(out var inventoryComponent);
                _canDrop = !inventoryComponent.Ingredients.Contains(_draggedIngredientSO);
            }
        }
        _draggedIngredientDraggable.SetDroppableHint(_canDrop);
    }

    public void SetInteractState()
    {
        _draggedIngredientRectTransform.gameObject.SetActive(false);
        _cursorState = CursorStates.Interact;
    }
    public void SetUIState()
    {
        _cursorState = CursorStates.UI;
    }
    public void SetDragState(IngredientSO ingredientSO, Inventory inventory)
    {
        _cursorState = CursorStates.Drag;
        _draggedIngredientSO = ingredientSO;
        _draggedInventoryOrigin = inventory;
        _draggedIngredientRectTransform.Find("img_Ingredient").GetComponent<Image>().sprite = ingredientSO.Sprite;
        _draggedIngredientRectTransform.position = Input.GetTouch(0).position;
        _draggedIngredientRectTransform.gameObject.SetActive(true);
    }
}
