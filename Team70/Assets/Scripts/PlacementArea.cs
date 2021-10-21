using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementArea : MonoBehaviour
{
    public GameObject targetObject;

    bool placed = false;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject) 
        {
            other.gameObject.GetComponent<SnappableObject>().SnapToLocation(transform.position);
            TriggerableAction();
        }
    }

    public void TriggerableAction()
    {
        if (!placed)
        {
            placed = true;
            gameManager.TriggerNextAction();
        }
    }
}
