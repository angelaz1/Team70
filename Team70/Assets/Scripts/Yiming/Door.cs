using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator doorAni;
    [SerializeField] AudioSource audioSource;
    [SerializeField] bool closeOnExit;
    [SerializeField] GameObject doorCollider;

    bool canCloseDoorForever = false;

    private void Start()
    {
        if (doorCollider) doorCollider.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dog")
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Dog")
        {
            if (closeOnExit) CloseDoor();
            if (canCloseDoorForever) GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void OpenDoor()
    {
        doorAni.SetBool("isOpen", true);
        audioSource.Play();
    }

    public void CloseDoor()
    {
        doorAni.SetBool("isOpen", false);
    }

    public void TurnOnCollider()
    {
        if (doorCollider) doorCollider.SetActive(true);
        canCloseDoorForever = true;
    }
}
