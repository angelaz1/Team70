using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveTips : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject interactableTips;
    public InteractableObjects io;
    private bool onlyOnce = true;
    private void Awake()
    {
        interactableTips.SetActive(false);
        io = GetComponentInParent<InteractableObjects>();
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!io.isGrab && other.tag == "Dog" && onlyOnce)
        {
            interactableTips.SetActive(true);
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Dog")
        {
            interactableTips.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Dog")
        {
            if (io.isGrab)
            {
                interactableTips.SetActive(false);
                if (!io.canThrow)
                {
                    onlyOnce = false;
                }
            }
        }
    }


}
