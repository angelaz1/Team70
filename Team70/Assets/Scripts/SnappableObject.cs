using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnappableObject : MonoBehaviour
{
    XRGrabInteractable interactable;
    bool isSnapped = false;

    private void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
    }

    public void SnapToLocation(Vector3 position)
    {
        isSnapped = true;
        interactable.enabled = false;
        transform.position = position;
    }

    public bool IsSnapped()
    {
        return isSnapped;
    }
}
