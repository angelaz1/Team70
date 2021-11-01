using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementArea : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject positionIndicator;

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
            other.gameObject.GetComponent<SnappableObject>().SnapToLocation(positionIndicator.transform.position);
            TriggerableAction();
        }
    }

    public void TriggerableAction()
    {
        if (!placed)
        {
            placed = true;
            gameManager.TriggerNextAction();
            Destroy(gameObject);
        }
    }
}
