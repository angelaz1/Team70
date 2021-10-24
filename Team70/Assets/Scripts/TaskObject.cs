using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskObject : MonoBehaviour
{
    public GameObject placementArea;
    public GameObject snappableObject;
    public float timeTillActivateObject = 0f;

    private void Start()
    {
        snappableObject.SetActive(false);
        placementArea.SetActive(false);

        Invoke(nameof(SetObjectActive), timeTillActivateObject);
    }

    void SetObjectActive()
    {
        snappableObject.SetActive(true);
    }

    public void SetPlacementAreaVisibility(bool value)
    {
        if (placementArea != null)
        {
            placementArea.SetActive(value);
        }    
    }
}
