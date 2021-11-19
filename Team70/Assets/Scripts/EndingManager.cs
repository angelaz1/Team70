using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
public class EndingManager : MonoBehaviour
{
    public GameObject simulatorObjects;
    public GameObject actualObjects;
    public List<GameObject> environmentAssets;
    public GameObject directionalLight;
    public GameObject creditsCanvas;
    public bool endscene = false;
    GameObject mainCamera;
    public GameObject movementController;
    //BGMManager bgmManager;

    Quaternion targetRotation = Quaternion.Euler(312.906311f, 184.462158f, 181.859985f);


    private void Awake()
    {
        simulatorObjects.SetActive(!XRSettings.isDeviceActive);
        actualObjects.SetActive(XRSettings.isDeviceActive);
        mainCamera = GameObject.Find("Main Camera");
        //bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        //bgmManager.SwapToBackyard();
        creditsCanvas.SetActive(false);
        if (endscene)
        {
            RemoveEverything();
        }
    }

    public void TriggerCredits()
    {
        mainCamera.GetComponent<Animator>().SetTrigger("FadeIn");
        Invoke(nameof(RemoveEverything), 2f);
    }

    void RemoveEverything()
    {
        foreach (GameObject environmentAsset in environmentAssets)
        { 
            environmentAsset.SetActive(false);
        }
        directionalLight.transform.rotation = targetRotation;
        creditsCanvas.SetActive(true);
        mainCamera.GetComponent<Animator>().SetTrigger("FadeOut");
        movementController.SetActive(true);
    }
}
