using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MuzzleController : MonoBehaviour
{
    public InputActionReference leftSelect;
    public InputActionReference rightSelect;

    GameObject selectingObject = null;
    bool grabbingObject;

    void Start()
    {
        leftSelect.action.started += PickUpObject;
        rightSelect.action.started += PickUpObject;

        leftSelect.action.canceled += DropObject;
        rightSelect.action.canceled += DropObject;
    }

    private void Update()
    {
        if (grabbingObject)
        {
            SnappableObject snappable = selectingObject.GetComponent<SnappableObject>();
            if (snappable != null && snappable.IsSnapped())
            {
                grabbingObject = false;
                selectingObject.GetComponent<Rigidbody>().freezeRotation = false;
                selectingObject = null;
            }
            else
            { 
                selectingObject.transform.position = transform.position;
            }
        }
    }

    void PickUpObject(InputAction.CallbackContext ctx)
    {
        Debug.Log("Started");
        if (!grabbingObject && selectingObject != null) 
        {
            selectingObject.transform.position = transform.position;
            selectingObject.transform.Rotate(transform.rotation.eulerAngles);
            selectingObject.GetComponent<Rigidbody>().freezeRotation = true;
            grabbingObject = true;
        }
    }

    void DropObject(InputAction.CallbackContext ctx)
    {
        Debug.Log("Cancelled");
        if (grabbingObject)
        { 
            grabbingObject = false;
            selectingObject.GetComponent<Rigidbody>().freezeRotation = false;
            selectingObject = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<XRGrabInteractable>() != null) 
        {
            SnappableObject snappable = other.gameObject.GetComponent<SnappableObject>();
            if (snappable != null && !snappable.IsSnapped())
            { 
                selectingObject = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (selectingObject == other.gameObject && !grabbingObject)
        {
            selectingObject = null;
        }
    }
}
