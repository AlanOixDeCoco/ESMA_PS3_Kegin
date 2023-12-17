using UnityEngine;
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
    [SerializeField] private RectTransform _draggedIngredientImage;
    [SerializeField] private Inventory _dryInventory, _shelvesInventory, _fridgeInventory;

    private CursorStates _cursorState = CursorStates.Interact;
    private IngredientSO _draggedIngredientSO;
    private Inventory _draggedInventoryOrigin;
    private Droppable _droppable;
    private bool _canDrop;

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
                Debug.LogWarning("Unknown cursor type!");
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
        
        var hitPoint = hit.point;
        transform.position = hitPoint;

        if(hit.collider.TryGetComponent<Interactive>(out var interactiveComponent))
            interactiveComponent.Interact();
    }

    private void Drag()
    {
        // Check if a touch is being registered
        if (Input.touchCount == 0)
        {
            // drop item if it can be dropped
            if (_canDrop) _droppable.Drop(_draggedIngredientSO, _draggedInventoryOrigin);
            else
            {
                _draggedInventoryOrigin.RemoveIngredient(_draggedIngredientSO);
                switch (_draggedIngredientSO.Storage)
                {
                    case StorageTypes.Dry:
                        _draggedInventoryOrigin = _dryInventory;
                        break;
                    case StorageTypes.Shelf:
                        _draggedInventoryOrigin = _shelvesInventory;
                        break;
                    case StorageTypes.Cold:
                        _draggedInventoryOrigin = _fridgeInventory;
                        break;
                }
                _draggedInventoryOrigin.AddIngredient(_draggedIngredientSO);
            }

            SetInteractState();
            return;
        }

        var touchPos = Input.GetTouch(0).position;

        Ray ray = _camera.ScreenPointToRay(touchPos);
        if (Physics.Raycast(ray, out var hit))
        {
            Vector3 hitPoint = hit.point;
            transform.position = hitPoint;

            _canDrop = hit.collider.TryGetComponent<Droppable>(out _droppable);
        }

        _draggedIngredientImage.position = touchPos;

        _droppable.TryGetComponent<Inventory>(out var inventoryComponent);
        _canDrop = _canDrop && !inventoryComponent.Ingredients.Contains(_draggedIngredientSO);
        _draggedIngredientImage.GetComponent<Draggable>().SetDroppableHint(_canDrop);
    }

    public void SetInteractState()
    {
        _draggedIngredientImage.gameObject.SetActive(false);
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
        _draggedIngredientImage.Find("img_Ingredient").GetComponent<Image>().sprite = ingredientSO.Sprite;
        _draggedIngredientImage.position = Input.GetTouch(0).position;
        _draggedIngredientImage.gameObject.SetActive(true);
    }
}
