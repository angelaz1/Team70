using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class SnappableObject : MonoBehaviour
{
    bool isSnapped = false;
    bool isGrabbed = false;

    TaskObject parentTaskObject;

    public AudioClip grabbedClip;
    public AudioClip droppedClip;

    AudioSource audioSource;
    DogSFXManager dogSFXManager;

    private void Start()
    {
        parentTaskObject = GetComponentInParent<TaskObject>();
        audioSource = GetComponent<AudioSource>();
        dogSFXManager = GameObject.Find("DogSFXManager").GetComponent<DogSFXManager>();
    }

    public void SnapToLocation(Vector3 position)
    {
        isSnapped = true;
        GetComponent<InteractableObjects>().Dropped();
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = position;
        transform.rotation = Quaternion.identity;
    }

    public void SetGrabbed(bool value)
    {
        isGrabbed = value;
        parentTaskObject.SetPlacementAreaVisibility(value);

        if (isGrabbed)
        {
            audioSource.clip = grabbedClip;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = droppedClip;
            audioSource.Play();

            if (isSnapped) dogSFXManager.PlayHappyClip();
            else dogSFXManager.PlaySadClip();
        }
    }

    public bool IsSnapped()
    {
        return isSnapped;
    }

    public bool IsGrabbed()
    {
        return isGrabbed;
    }

    public void RemoveAnimator()
    {
        Destroy(GetComponent<Animator>());
    }
}
