using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnappableObject : MonoBehaviour
{
    XRGrabInteractable interactable;

    private void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
    }

    public void SnapToLocation(Vector3 position)
    {
        interactable.enabled = false;
        transform.position = position;
    }
}
