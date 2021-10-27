using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2 : TutorialNode
{
    private void Start()
    {
        if (tutorialClip) { this.GetComponent<AudioSource>().PlayOneShot(tutorialClip); }
    }
    private void OnTriggerEnter(Collider other)
    {
        EndNode(other);
    }
}
