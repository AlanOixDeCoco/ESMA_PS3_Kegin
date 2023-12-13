using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum CursorStates
{
    interact,
    ui,
    drag
}

public class CursorDrag
{
    public IngredientSO Ingredient { get; set; }
    public Inventory Inventory { get; set; }

    public CursorDrag(IngredientSO ingredient, Inventory inventory)
    {
        Ingredient = ingredient;
        Inventory = inventory;
    }
}

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _draggedIngredientImage;
    [SerializeField] private Inventory _dryInventory, _shelvesInventory, _fridgeInventory;

    private CursorStates _cursorState = CursorStates.interact;
    private IngredientSO _draggedIngredientSO = null;
    private Inventory _draggedInventoryOrigin = null;
    private Droppable _droppable = null;

    private void Update()
    {
        switch (_cursorState)
        {
            case CursorStates.interact:
                Interact();
                break;
            case CursorStates.ui:
                break;
            case CursorStates.drag:
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

        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(touchPos);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;
            transform.position = hitPoint;

            var interactable = hit.collider.GetComponentInParent<Interactable>();
            if (interactable != null) interactable.Interact();
        }
    }

    private void Drag()
    {
        // Check if a touch is being registered
        if (Input.touchCount == 0)
        {
            // drop item if it can be dropped
            if (_droppable != null)
            {
                _droppable.Drop(_draggedIngredientSO, _draggedInventoryOrigin);
            }
            else
            {
                _draggedInventoryOrigin.RemoveIngredient(_draggedIngredientSO);
                switch (_draggedIngredientSO.Storage)
                {
                    case StorageTypes.dry:
                        _draggedInventoryOrigin = _dryInventory;
                        break;
                    case StorageTypes.shelf:
                        _draggedInventoryOrigin = _shelvesInventory;
                        break;
                    case StorageTypes.cold:
                        _draggedInventoryOrigin = _fridgeInventory;
                        break;
                }
                _draggedInventoryOrigin.AddIngredient(_draggedIngredientSO);
            }

            SetInteractState();
            return;
        }

        var touchPos = Input.GetTouch(0).position;

        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(touchPos);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;
            transform.position = hitPoint;

            _droppable = hit.collider.GetComponentInParent<Droppable>();
        }
        else
        {
            _droppable = null;
        }

        _draggedIngredientImage.position = touchPos;

        if (_droppable == null)
        {
            _draggedIngredientImage.GetComponent<Draggable>().SetDroppableHint(false);
        }
        else
        {
            bool containsIngredient = _droppable.GetComponent<Inventory>().Ingredients.Contains(_draggedIngredientSO);
            _draggedIngredientImage.GetComponent<Draggable>().SetDroppableHint(!containsIngredient);
        }
    }

    public void SetInteractState()
    {
        _draggedIngredientImage.gameObject.SetActive(false);
        _cursorState = CursorStates.interact;
    }
    public void SetUIState()
    {
        _cursorState = CursorStates.ui;
    }
    public void SetDragState(IngredientSO ingredientSO, Inventory inventory)
    {
        _cursorState = CursorStates.drag;
        _draggedIngredientSO = ingredientSO;
        _draggedInventoryOrigin = inventory;
        _draggedIngredientImage.Find("img_Ingredient").GetComponent<Image>().sprite = ingredientSO.Sprite;
        _draggedIngredientImage.gameObject.SetActive(true);
    }
}
