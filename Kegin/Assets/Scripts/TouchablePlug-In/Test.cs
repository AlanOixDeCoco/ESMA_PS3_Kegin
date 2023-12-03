using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, ITouchable
{

    public void OnTouchDown(Vector3 touchPosition)
    {
        return;
    }


    public void OnTouchStay(Vector3 touchPosition)
    {
        transform.position = touchPosition;
    }

    public void OnTouchUp()
    {
        return;
    }
}
