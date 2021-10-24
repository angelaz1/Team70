using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator doorAni;
    [SerializeField] AudioSource audioSource;
    [SerializeField] bool closeOnExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dog")
        {
            doorAni.SetBool("isOpen", true);
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Dog")
        {
            if (closeOnExit) doorAni.SetBool("isOpen", false);
        }
    }
}
