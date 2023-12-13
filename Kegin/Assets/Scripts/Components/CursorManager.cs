using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
}

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private CursorStates _cursorState = CursorStates.interact;
    private CursorDrag _drag = null;

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

        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;
            transform.position = hitPoint;

            var interactable = hit.collider.GetComponentInParent<Interactable>();
            if (interactable != null) interactable.Interact();
        }
    }
}
