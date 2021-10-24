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

    public GameObject frontdoorCollider;
    public GameObject backdoorCollider;

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
        backdoorCollider.SetActive(false);
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
        if (currentEvent == 0)
        {
            frontdoorCollider.GetComponent<Door>().CloseDoor();
            frontdoorCollider.SetActive(false);
            bgmManager.FadeOutBGM();
        }

        if (currentEvent < waitTimes.Length)
        {
            grandma.CompleteCurrentState();
            Invoke(nameof(TriggerNewspaperState), waitTimes[currentEvent]);

            if (currentEvent == waitTimes.Length - 1) backdoorCollider.SetActive(true);
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
