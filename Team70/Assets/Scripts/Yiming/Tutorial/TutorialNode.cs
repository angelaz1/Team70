using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TutorialNode : MonoBehaviour
{
    public int num;
    public TutorialNode nextNode;
    public AudioClip tutorialClip;
    public TutorialManager tutorialManager;
    private void OnEnable()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }




    public virtual void EndNode(Collider other)
    {
        if(other.tag == "Dog")
        {
            tutorialManager.GenerateNewNode();
            Destroy(this.gameObject);
        }
        
    }

    public virtual void EndNode()
    {    
        tutorialManager.GenerateNewNode();
        Destroy(this.gameObject);
    }
}
