using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{

    public bool debug;
    [SerializeField] private Camera _camera;
    private Collider _collider;
    private ITouchable _iTouchable;

    public int tc;

    void Update()
    {

        tc = Input.touchCount;

        if(Input.touchCount<=0)
        {
            ChangeITouchable(null);
            return;
        }


        var touchePos = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, _camera.farClipPlane);
        var touchePosInWorld = _camera.ScreenToWorldPoint(touchePos);
        
        if(debug)
        {
            Debug.DrawLine(_camera.transform.position, touchePosInWorld, Color.red);
        }

        if (Physics.Raycast(_camera.transform.position, touchePosInWorld - _camera.transform.position, out var info))
        {
            var touchePosNear = _camera.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, _camera.nearClipPlane+10));
            if(info.collider != _collider)
            {
                _collider = info.collider;
                ChangeITouchable( info.collider.GetComponent<ITouchable>());

                _iTouchable?.OnTouchDown(touchePosNear);
            }
            _iTouchable?.OnTouchStay(touchePosNear);

        }
        else
        {
            ChangeITouchable(null);
        }
    }

    private void ChangeITouchable(ITouchable newValue)
    {
        if(_iTouchable==newValue)
        {
            return;
        }
        _iTouchable?.OnTouchUp();
        _iTouchable = newValue;
        if(newValue == null)
        {
            _collider = null;
        }
    }
}
