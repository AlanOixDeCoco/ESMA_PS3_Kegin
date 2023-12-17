using UnityEngine;
using UnityEngine.Events;

public class Interactive : MonoBehaviour
{
    [SerializeField] private bool _interactable = true;

    [SerializeField] private UnityEvent<Transform> _onInteract;

    public void Interact()
    {
        if (_interactable) _onInteract.Invoke(transform);
    }
}
