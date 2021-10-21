using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaController : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public List<GameObject> thoughtBubbles;
    public GameObject thoughtCanvas;

    float[] waitTimes = { 2f };
    string[] triggerNames = { "EnterRoom" };

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

        switch (currentState)
        {
            case 0: 
            case 1: // Player gets newspaper -> trigger grandma anim + dialogue
            case 2: // Player gets glasses -> trigger grandma anim + dialogue, wait, then anim + dialogue
            case 3: // Player gets meds -> trigger grandma anim + dialogue, grandma goes outside
            case 4: // Player goes outside -> trigger UI/o.w. to tell player to bark
            case 5: // Player barks -> trigger anim + dialogue
                StartCoroutine(WaitToTriggerState()); break;
            default: Debug.LogError("No Actions left!"); return;
        }

    }

    IEnumerator WaitToTriggerState()
    {
        yield return new WaitForSeconds(waitTimes[currentState]);
        audioSource.clip = audioClips[currentState];
        audioSource.Play();

        if (currentThoughtBubble) Destroy(currentThoughtBubble);
        if (currentState < thoughtBubbles.Count) {
            currentThoughtBubble = Instantiate(thoughtBubbles[currentState], thoughtCanvas.transform);
        }

        anim.SetTrigger(triggerNames[currentState]);
    }
}
