using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{
    [SerializeField] private float _hfov = 50f;

    private void Start()
    {
        GetComponent<Camera>().fieldOfView = Camera.HorizontalToVerticalFieldOfView(_hfov, GetComponent<Camera>().aspect);
    }
}
