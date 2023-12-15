using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    private Inventory _inventory;

    private void Start()
    {
        _inventory = GetComponent<Inventory>();
    }
}
