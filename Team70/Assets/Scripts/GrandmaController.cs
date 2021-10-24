using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaController : MonoBehaviour
{
    public List<AudioClip> startActionClips; // This will likely need to be changed into "finish action clips" and "start action clips"
    public List<AudioClip> finishActionClips;
    public List<GameObject> thoughtBubbles;
    public GameObject thoughtCanvas;

    public float waitTimeTillShowFinish = 1f;

    string[] startActionTriggerNames = { "WaitForNewspaper", "WaitForGlasses", "WaitForMeds" };
    string[] finishActionTriggerNames = { "EnteredRoom", "GrabbedNewspaper", "GrabbedGlasses", "GrabbedMeds" };

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

        Invoke(nameof(FinishCurrentState), waitTimeTillShowFinish);
    }

    void FinishCurrentState()
    {
        if (currentState + 1 >= 0 && currentState + 1 < finishActionTriggerNames.Length)
        {
            anim.SetTrigger(finishActionTriggerNames[currentState + 1]);
        }

        if (currentState >= 0 && currentState < finishActionClips.Count)
        {
            audioSource.clip = finishActionClips[currentState];
            audioSource.Play();
        }
    }

    void StartNextState()
    {
        if (currentState < startActionClips.Count)
        { 
            audioSource.clip = startActionClips[currentState];
            audioSource.Play();
        }

        if (currentState < thoughtBubbles.Count) {
            currentThoughtBubble = Instantiate(thoughtBubbles[currentState], thoughtCanvas.transform);
        }

        if (currentState < startActionTriggerNames.Length)
        { 
            anim.SetTrigger(startActionTriggerNames[currentState]);
        }
    }
}
