using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]Animator doorAni;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dog")
        {
            doorAni.SetBool("isOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Dog")
        {
            doorAni.SetBool("isOpen", false);
        }
    }
}
