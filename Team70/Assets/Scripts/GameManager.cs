using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public GameObject simulatorObjects;
    public GameObject actualObjects;

    public GrandmaController grandma;
    GameObject cameraMover;

    // List of wait times before triggering next action in sequence
    public float[] waitTimes;

    public GameObject[] taskObjects;

    int currentEvent = 0;
    bool waitingForBark = false;

    public GameObject frontdoorCollider;
    public GameObject backdoorCollider;

    BGMManager bgmManager;

    Camera innerCamera;

    bool moveCamera = false;
    Quaternion currentRotation;
    Vector3 currentPosition;
    float currentTime = 0;
    float moveCamTime = 3f;

    private void Awake()
    {
        simulatorObjects.SetActive(!XRSettings.isDeviceActive);
        actualObjects.SetActive(XRSettings.isDeviceActive);
        cameraMover = GameObject.Find("CameraMover");
        innerCamera = cameraMover.GetComponentInChildren<Camera>();

        foreach (GameObject taskObject in taskObjects)
        {
            taskObject.SetActive(false);
        }

        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        backdoorCollider.SetActive(false);
    }

    private void Update()
    {
        if (moveCamera)
        {
            innerCamera.transform.localRotation = Quaternion.Lerp(currentRotation, Quaternion.identity, currentTime / moveCamTime);
            innerCamera.transform.localPosition = Vector3.Lerp(currentPosition, Vector3.zero, currentTime / moveCamTime);
            currentTime += Time.deltaTime;
            if (currentTime == 1) moveCamera = false;
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
            Invoke(nameof(TriggerGrandmaState), waitTimes[currentEvent]);

            //if (currentEvent == waitTimes.Length - 1) backdoorCollider.SetActive(true);
        }
        //else if (currentEvent == waitTimes.Length)
        //{
        //    // Prompt player to bark
        //    waitingForBark = true;
        //    barkUI.SetActive(true);
        //}
        else
        {
            Debug.LogError("No Actions left!");
        }
    }

    private void TriggerGrandmaState()
    {
        if (currentEvent < taskObjects.Length) taskObjects[currentEvent].SetActive(true);
        grandma.TriggerNextState();
        currentEvent++;
    }

    public void TriggerEndScene()
    {
        Debug.Log("Ending scene!");
        grandma.TriggerEndingState();
        cameraMover.GetComponent<Animator>().SetTrigger("EndingCutscene");
        cameraMover.GetComponentInChildren<TrackedPoseDriver>().enabled = false;
        moveCamera = true;
        currentRotation = innerCamera.transform.localRotation;
        currentPosition = innerCamera.transform.localPosition;
    }
}
