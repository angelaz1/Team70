using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Tutorial3 : TutorialNode
{
    InputDevice rightHand;
    InputDevice leftHand;
    Transform muzzleLocation;
    DogMovement dogMovement;
    public AudioClip GrabBoneInstruction;
    public GameObject Bone;
    public float showBoneTime = 1f;
    public bool isGrab = false;
    public bool risPress = false;
    public bool lisPress = false;
    private void Start()
    {
        if (tutorialClip) { this.GetComponent<AudioSource>().PlayOneShot(tutorialClip); }
        muzzleLocation = GameObject.Find("MuzzleLocation").GetComponent<Transform>();
        dogMovement = FindObjectOfType<DogMovement>();
        Bone.SetActive(false);
        StartCoroutine(ShowBone());
        StartCoroutine(InstructionSound(tutorialClip.length));
        InitialDevice();
    }
    

    IEnumerator ShowBone()
    {
        yield return new WaitForSeconds(showBoneTime);
        Transform dog = GameObject.Find("dog").GetComponent<Transform>();
        this.transform.position = transform.position + transform.forward * 20f;
        Bone.SetActive(true);
       
    }
    IEnumerator InstructionSound(float i)
    {
        yield return new WaitForSeconds(i);
        this.GetComponent<AudioSource>().PlayOneShot(GrabBoneInstruction);
    }
    private void Update()
    {
        if (rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool rt))
        {
        
             risPress = rt; 
        }

        if (leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool lt))
        {
            lisPress = lt; 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Dog" && !isGrab)
        {
            dogMovement.canMove = false;
        }

        
        if (risPress && (other.tag == "Dog") && !isGrab) { GrabObject(); print("enter bone area"); }
        else if (lisPress && (other.tag == "Dog") && !isGrab) { GrabObject(); print("enter bone area"); }
        

    }
    private void OnTriggerStay(Collider other)
    {
        if (risPress && (other.tag == "Dog") && !isGrab) { GrabObject(); print("enter bone area"); }
        else if (lisPress && (other.tag == "Dog") && !isGrab) { GrabObject(); print("enter bone area"); }
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

    private void GrabObject()
    {
        this.transform.position = muzzleLocation.position;
        this.transform.SetParent(muzzleLocation);
        this.transform.localRotation = Quaternion.identity;
        //this.GetComponent<Rigidbody>().freezeRotation = true;
        isGrab = true;
        EndNode();
    }
    public override void EndNode()
    {
        tutorialManager.GenerateNewNode();
        dogMovement.canMove = true;
    }
}
