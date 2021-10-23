using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskObject : MonoBehaviour
{
    public GameObject placementArea;
    public GameObject snappableObject;

    private void Start()
    {
        snappableObject.SetActive(true);
        placementArea.SetActive(false);
    }

    public void SetPlacementAreaVisibility(bool value)
    {
        if (placementArea != null)
        {
            placementArea.SetActive(value);
        }    
    }
}
