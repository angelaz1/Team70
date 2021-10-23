using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GestureRecord : MonoBehaviour
{
    private List<Vector3> rightPositionList = new List<Vector3>();
    public bool isCreatMode= true;
    bool ButtonPress;
    bool IsMoving = false;
    InputDevice rightHand;
    InputDevice leftHand;
    public Transform rightT;//only right Hand input to record the gesture
    public Transform rigT;
    private Vector3 ReversoRightT;
    public float recordInterval = 0.05f;

    public GameObject debugCubePrefab;
    public Gestures gestureStore;//The object that store the gesture list
    public void Start()
    {
        InitialDevice();
    }

    private void Update()
    {
        if (rightHand.TryGetFeatureValue(CommonUsages.gripButton, out bool rgb))
        {

            ButtonPress = rgb;
        }

        ReversoRightT = rightT.position - rigT.position;
        if (isCreatMode)
        {
            if (!IsMoving && ButtonPress)
            {
                RightStartMovement();
            }
            else if (IsMoving && !ButtonPress)
            {
                RightEndMovement();
            }
            else if (IsMoving && ButtonPress)
            {
                RightUpdateMovement();
            }
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

    private void RightStartMovement() 
    {
        rightPositionList.Clear();
        IsMoving = true;
        rightPositionList.Add(ReversoRightT);
        gestureStore.gesture.Clear();
        gestureStore.gesture.Add(ReversoRightT);
        if (debugCubePrefab) Destroy(Instantiate(debugCubePrefab, ReversoRightT, Quaternion.identity), 3);
    }

    private void RightEndMovement() 
    {
        IsMoving = false;
        Debug.Log(rightPositionList.Count); 
    }

    private void RightUpdateMovement() 
    {
        Vector3 latestPos = rightPositionList[rightPositionList.Count - 1];
        if (Vector3.Distance(latestPos, ReversoRightT) > recordInterval)
        {
            if (debugCubePrefab) Destroy(Instantiate(debugCubePrefab, ReversoRightT, Quaternion.identity), 1);
            rightPositionList.Add(ReversoRightT);
            gestureStore.gesture.Add(ReversoRightT);
        }
    }
}
