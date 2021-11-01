using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class DogBark : MonoBehaviour
{
    InputDevice rightHand;
    InputDevice leftHand;
    DogSFXManager sFXManager;
    // Start is called before the first frame update
    void Start()
    {
        InitialDevice();
    }

    // Update is called once per frame
    void Update()
    {
        if (rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool ra))
        {

            if (ra) { Bark(); }
        }

        if (leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool la))
        {

            if (la) { Bark(); }
        }
    }


    public void InitialDevice()
    {
        List<InputDevice> rightdevices = new List<InputDevice>();
        List<InputDevice> leftdevices = new List<InputDevice>();
        InputDeviceRole righthand = InputDeviceRole.RightHanded;
        InputDeviceRole lefthand = InputDeviceRole.LeftHanded;
        InputDevices.GetDevicesWithRole(righthand, rightdevices);
        if (rightdevices.Count > 0)
        {
            rightHand = rightdevices[0];
        }
        InputDevices.GetDevicesWithRole(lefthand, leftdevices);
        if (leftdevices.Count > 0)
        {
            leftHand = leftdevices[0];
        }
    }

    private void Bark()
    {
        sFXManager.PlayHappyClip();
    }
}
