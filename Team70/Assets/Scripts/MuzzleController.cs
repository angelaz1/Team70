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
        leftSelect.action.started += CheckForObject;
        rightSelect.action.started += CheckForObject;
    }

    private void Update()
    {
        if (grabbingObject)
        {
            SnappableObject snappable = selectingObject.GetComponent<SnappableObject>();
            if (snappable != null && snappable.IsSnapped())
            {
                DropObject(true);
            }
            else
            { 
                selectingObject.transform.position = transform.position;
            }
        }
    }

    void CheckForObject(InputAction.CallbackContext ctx)
    { 
        if (grabbingObject)
        {
            Debug.Log("Dropping object");
            DropObject(false);
        }
        else if (!grabbingObject && selectingObject != null)
        {
            Debug.Log("Grabbing object");
            GrabObject();
        }
    }

    private void DropObject(bool dropFromSnapping)
    {
        grabbingObject = false;
        selectingObject.GetComponent<Rigidbody>().freezeRotation = false;
        selectingObject.GetComponent<SnappableObject>().SetGrabbed(false);
        selectingObject.transform.SetParent(null);
        selectingObject.transform.rotation = Quaternion.identity;
        if (!dropFromSnapping) selectingObject.transform.position += 0.5f * Vector3.up;
        //selectingObject = null;
    }

    private void GrabObject()
    {
        selectingObject.transform.position = transform.position;
        selectingObject.transform.SetParent(transform);
        selectingObject.transform.localRotation = Quaternion.identity;
        selectingObject.GetComponent<Rigidbody>().freezeRotation = true;
        selectingObject.GetComponent<SnappableObject>().SetGrabbed(true);
        grabbingObject = true;
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
