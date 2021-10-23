using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaController : MonoBehaviour
{
    public List<AudioClip> audioClips; // This will likely need to be changed into "finish action clips" and "start action clips"
    public List<GameObject> thoughtBubbles;
    public GameObject thoughtCanvas;

    public string[] triggerNames = { "EnteredRoom", "GrabbedNewspaper", "GrabbedGlasses", "GrabbedMeds" };

    int currentState = -1;
    Animator anim;
    AudioSource audioSource;
    GameObject currentThoughtBubble = null;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TriggerNextState()
    {
        currentState++;

        if (currentState < thoughtBubbles.Count)
        {
            StartNextState();
        }
        else
        {
            Debug.LogError("No Actions left for Grandma!");
        }

        //switch (currentState)
        // {
        //    case 0: 
        //    case 1: // Player gets newspaper -> trigger grandma anim + dialogue
        //    case 2: // Player gets glasses -> trigger grandma anim + dialogue, wait, then anim + dialogue
        //    case 3: // Player gets meds -> trigger grandma anim + dialogue, grandma goes outside
        //    case 4: // Player goes outside -> trigger UI/o.w. to tell player to bark
        //    case 5: // Player barks -> trigger anim + dialogue
        //        StartNextState(); break;
        //    default: Debug.LogError("No Actions left!"); return;
        // }
    }

    public void TriggerEndingState()
    {
        // TODO: Have grandma walk outside, frisbee, audio
        //audioSource.clip = finalAudioClip;
        //audioSource.Play();

        //anim.SetTrigger("FinalAnimation");
    }

    public void CompleteCurrentState()
    {
        if (currentThoughtBubble) Destroy(currentThoughtBubble);
    }

    void StartNextState()
    {
        if (currentState < audioClips.Count)
        { 
            audioSource.clip = audioClips[currentState];
            audioSource.Play();
        }

        if (currentState < thoughtBubbles.Count) {
            currentThoughtBubble = Instantiate(thoughtBubbles[currentState], thoughtCanvas.transform);
        }

        if (currentState < triggerNames.Length)
        { 
            anim.SetTrigger(triggerNames[currentState]);
        }
    }
}
