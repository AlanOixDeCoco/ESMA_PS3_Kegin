using UnityEngine;
using UnityEngine.Events;

public class Interactive : MonoBehaviour
{
    [SerializeField] private bool _interactable = true;

    [SerializeField] private UnityEvent<Transform> _onInteract;

    public bool Interactable
    {
        get => _interactable;
        set => _interactable = value;
    }

    public void Interact()
    {
        if (Interactable) _onInteract.Invoke(transform);
    }
}
