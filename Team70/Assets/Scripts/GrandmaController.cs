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

    string[] startActionTriggerNames = { "WaitForNewspaper", "WaitForGlasses", "WaitForPills", "GoToMusicBox" };
    string[] finishActionTriggerNames = { "EnteredRoom", "GrabbedNewspaper", "GrabbedGlasses", "GrabbedPills" };

    int currentState = -1;
    Animator anim;
    AudioSource audioSource;
    GameObject currentThoughtBubble = null;

    public Door backDoor;
    public Animator detailedGrandmaAnimator;
    public List<AudioClip> finalAudioClips;

    [Header("Task Objects")]
    public GameObject inWorldNewspaper;
    public GameObject newspaper;
    public GameObject openNewspaper;
    public GameObject inWorldGlasses;
    public GameObject glasses;
    public GameObject inWorldMedicine;
    public GameObject inWorldFrisbee;
    public GameObject frisbee;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TriggerNextState()
    {
        currentState++;

        if (currentState < 3)
        {
            StartNextState();
        }
        else if (currentState == 3)
        {
            // Finished indoor tasks, trigger music box
            audioSource.clip = startActionClips[currentState];
            audioSource.Play();
            Invoke(nameof(TriggerGrandmaOutside), audioSource.clip.length + 1f);
            anim.SetTrigger(startActionTriggerNames[currentState]);
            Invoke(nameof(PlayMusicBGM), 2f);
        }
        else
        {
            Debug.Log("No Actions left for Grandma!");
        }
    }

    void PlayMusicBGM()
    {
        GameObject.Find("BGMManager").GetComponent<BGMManager>().PlayMusicBoxBGM();
    }

    public void TriggerGrandmaOutside()
    {
        anim.SetTrigger("GoOutdoors");

        currentThoughtBubble = Instantiate(thoughtBubbles[currentState], thoughtCanvas.transform);
    }

    public void TriggerEndingState()
    {
        anim.SetTrigger("FinalAnimation");
        audioSource.clip = finalAudioClips[0];
        audioSource.Play();
    }

    public void TriggerActualEndingState()
    {
        Invoke(nameof(ThrowFrisbee), 1f);
        audioSource.clip = finalAudioClips[1];
        audioSource.Play();
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
            Invoke(nameof(PlayCurrentFinishClip), 0.5f);
        }
        else if (currentState == finishActionClips.Count)
        {
            TriggerEndingState();
        }
    }

    void StartNextState()
    {
        if (currentState < startActionClips.Count)
        {
            Invoke(nameof(PlayCurrentStartClip), 0.5f);
        }

        if (currentState < thoughtBubbles.Count) 
        {
            currentThoughtBubble = Instantiate(thoughtBubbles[currentState], thoughtCanvas.transform);
        }

        if (currentState < startActionTriggerNames.Length)
        { 
            anim.SetTrigger(startActionTriggerNames[currentState]);
        }
    }

    void PlayCurrentFinishClip()
    {
        audioSource.clip = finishActionClips[currentState];
        audioSource.Play();
    }

    void PlayCurrentStartClip()
    {
        audioSource.clip = startActionClips[currentState];
        audioSource.Play();
    }

    #region animation
    public void TriggerFrisbeePickup()
    {
        frisbee.SetActive(false);
    }

    public void TriggerOpenBackdoor()
    {
        backDoor.OpenDoor();
    }

    public void TriggerCloseBackdoor()
    {
        backDoor.CloseDoor();
    }

    public void StartWalkCycle()
    {
        detailedGrandmaAnimator.SetBool("isWalking", true);
    }

    public void StopWalkCycle()
    {
        detailedGrandmaAnimator.SetBool("isWalking", false);
    }

    public void SitDown()
    {
        detailedGrandmaAnimator.SetBool("isSitting", true);
    }

    public void StandUp()
    {
        detailedGrandmaAnimator.SetBool("isSitting", false);
    }

    public void StartTalking()
    {
        detailedGrandmaAnimator.SetBool("isTalking", true);
    }

    public void StopTalking()
    {
        detailedGrandmaAnimator.SetBool("isTalking", false);
    }

    public void StartReading()
    {
        detailedGrandmaAnimator.SetBool("isReading", true);
    }

    public void StopReading()
    {
        detailedGrandmaAnimator.SetBool("isReading", false);
    }

    public void PickUpObject()
    {
        detailedGrandmaAnimator.SetTrigger("pickUpObject");
    }

    public void ThrowFrisbee()
    {
        detailedGrandmaAnimator.SetTrigger("throwFrisbee");
    }

    public void PickUpNewspaper()
    {
        inWorldNewspaper.SetActive(false);
        newspaper.SetActive(true);
    }

    public void OpenNewspaper()
    {
        newspaper.SetActive(false);
        openNewspaper.SetActive(true);
    }

    public void PutAwayNewspaper()
    {
        newspaper.SetActive(false);
        openNewspaper.SetActive(false);
    }

    public void PickUpGlasses()
    {
        inWorldGlasses.SetActive(false);
        glasses.SetActive(true);
    }

    public void PickUpMedicine()
    {
        inWorldMedicine.SetActive(false);
    }

    public void PickUpFrisbee()
    {
        inWorldFrisbee.SetActive(false);
        frisbee.SetActive(true);
    }
    #endregion
}
