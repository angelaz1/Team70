using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnappableObject : MonoBehaviour
{
    XRGrabInteractable interactable;
    bool isSnapped = false;
    bool isGrabbed = false;

    TaskObject parentTaskObject;

    private void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        parentTaskObject = GetComponentInParent<TaskObject>();
    }

    public void SnapToLocation(Vector3 position)
    {
        isSnapped = true;
        interactable.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = position;
        transform.rotation = Quaternion.identity;
    }

    public void SetGrabbed(bool value)
    {
        isGrabbed = value;
        parentTaskObject.SetPlacementAreaVisibility(value);
    }

    public bool IsSnapped()
    {
        return isSnapped;
    }

    public bool IsGrabbed()
    {
        return isGrabbed;
    }
}
