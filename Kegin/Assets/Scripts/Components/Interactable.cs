using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool _interactable = true;

    [SerializeField] private UnityEvent<Transform> _onInteract;

    public void Interact()
    {
        if (_interactable) _onInteract.Invoke(transform);
    }
}
