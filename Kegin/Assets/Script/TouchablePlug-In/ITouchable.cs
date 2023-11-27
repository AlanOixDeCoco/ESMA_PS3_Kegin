using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchable 
{
    public void OnTouchDown(Vector3 touchPosition);
    public void OnTouchStay(Vector3 touchPosition);
    public void OnTouchUp();
}
