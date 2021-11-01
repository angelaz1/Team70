using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2 : TutorialNode
{

    public string otherTurtorialTxt;
    private void Start()
    {
        if (tutorialClip) { this.GetComponent<AudioSource>().PlayOneShot(tutorialClip); }
        tutorialManager.ShowTurtorialTxt(tutorialText + "\n" + otherTurtorialTxt);
        
    }


  

    private void OnTriggerEnter(Collider other)
    {
        EndNode(other);
    }
}
