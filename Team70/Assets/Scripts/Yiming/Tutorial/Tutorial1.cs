using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tutorial1 : TutorialNode
{
    public float offsetTime = 0.5f;
    private DogSFXManager dogSFXManager;
    private void Start()
    {
        dogSFXManager = FindObjectOfType<DogSFXManager>();
        if (tutorialClip) 
        {
            this.GetComponent<AudioSource>().PlayOneShot(tutorialClip);
            offsetTime += tutorialClip.length;
        }

        StartCoroutine(ResponeBark());
    }

    IEnumerator ResponeBark()
    {
        yield return new WaitForSeconds(offsetTime);

        dogSFXManager.PlayHappyClip();
        StartCoroutine(ToEnd());
    }

    IEnumerator ToEnd()
    {
        yield return new WaitForSeconds(1f);
        EndNode();
    }
}
