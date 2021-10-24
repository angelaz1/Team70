using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public GameObject simulatorObjects;
    public GameObject actualObjects;

    public GrandmaController grandma;
    public GameObject cameraMover;

    // List of wait times before triggering next action in sequence
    public float[] waitTimes;

    public GameObject[] taskObjects;

    int currentEvent = 0;
    bool waitingForBark = false;

    public GameObject barkUI;
    public InputActionReference barkButton;

    BGMManager bgmManager;

    private void Awake()
    {
        simulatorObjects.SetActive(!XRSettings.isDeviceActive);
        actualObjects.SetActive(XRSettings.isDeviceActive);

        foreach (GameObject taskObject in taskObjects)
        {
            taskObject.SetActive(false);
        }

        barkButton.action.started += CheckForBark;

        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
    }

    void CheckForBark(InputAction.CallbackContext ctx)
    {
        if (waitingForBark)
        {
            Debug.Log("Woof!");
            TriggerEndScene();
            waitingForBark = false;
        }
    }

    public void TriggerNextAction() 
    {
        if (currentEvent == 0) bgmManager.FadeOutBGM();

        if (currentEvent < waitTimes.Length)
        {
            grandma.CompleteCurrentState();
            Invoke(nameof(TriggerNewspaperState), waitTimes[currentEvent]);
        }
        else if (currentEvent == waitTimes.Length)
        {
            // Prompt player to bark
            waitingForBark = true;
            barkUI.SetActive(true);
        }
        else
        {
            Debug.LogError("No Actions left!");
        }

        //switch (currentEvent) 
        //{
        //    case 0:
        //        {
        //            // Player walks through door -> wait for player to explore then trigger grandma
        //            Invoke(nameof(TriggerNewspaperState), waitTimes[currentEvent]);
        //            break;
        //        }
        //    case 1: // Player gets newspaper -> trigger grandma anim + dialogue
        //    case 2: // Player gets glasses -> trigger grandma anim + dialogue, wait, then anim + dialogue
        //    case 3: // Player gets meds -> trigger grandma anim + dialogue, grandma goes outside
        //    case 4: // Player goes outside -> trigger UI/o.w. to tell player to bark
        //    case 5: // Player barks -> trigger anim + dialogue
        //        grandma.TriggerNextState(); break;
        //    default: Debug.LogError("No Actions left!"); return;
        //}

        //currentEvent++;
    }

    private void TriggerNewspaperState()
    {
        if (currentEvent < taskObjects.Length) taskObjects[currentEvent].SetActive(true);
        grandma.TriggerNextState();
        currentEvent++;
    }

    public void TriggerEndScene()
    {
        // Player did bark
        barkUI.SetActive(false);
        grandma.TriggerEndingState();
        cameraMover.GetComponent<Animator>().SetTrigger("EndingCutscene"); // Change this to be the correct camera movement
    }
}
