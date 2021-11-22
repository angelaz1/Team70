using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour
{
    public UnityEvent OnPressed, OnReleased;
    public float threshold = .1f;
    public float deadZone = 0.025f;

    private bool _isPressed;
    private Vector3 startPoint;
    private ConfigurableJoint joint;
    private void Start()
    {
        startPoint = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        if(!_isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }
        else if(_isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }



    private float GetValue()
    {
        var value = Vector3.Distance(startPoint, transform.localPosition) / joint.linearLimit.limit;
        if(Mathf.Abs(value) < deadZone) { value = 0; }
        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        _isPressed = true;
        OnPressed.Invoke();
        Debug.Log("pressed button");
    }




    private void Released()
    {
        _isPressed = false;
        OnReleased.Invoke();
        Debug.Log("released button");
    }
}
