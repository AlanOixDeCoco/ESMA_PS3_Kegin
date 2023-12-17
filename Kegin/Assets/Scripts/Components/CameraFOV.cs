using UnityEngine;
using UnityEngine.Serialization;

public class CameraFOV : MonoBehaviour
{
    [FormerlySerializedAs("_hfov")] [SerializeField] private float _hFOV = 50f;

    private void Start()
    {
        GetComponent<Camera>().fieldOfView = Camera.HorizontalToVerticalFieldOfView(_hFOV, GetComponent<Camera>().aspect);
    }
}
