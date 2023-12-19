using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFOV : MonoBehaviour
{
    [FormerlySerializedAs("_hfov")] [SerializeField] private float _hFOV = 100f;
    [SerializeField] private Camera _realCamera;

    private void Start()
    {
        var virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.m_Lens.FieldOfView = Camera.HorizontalToVerticalFieldOfView(_hFOV, _realCamera.aspect);
    }
}
