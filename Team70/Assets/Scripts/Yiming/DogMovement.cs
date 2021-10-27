using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DogMovement : MonoBehaviour
{

    public GameObject rightInput;
    public GameObject leftInput;
    public List<GameObject> pace = new List<GameObject>();
    public bool canMove = true;
    
    private List<string> rightPawMovepace = new List<string>();
    private List<string> leftPawMovepace =  new List<string>();
    // Start is called before the first frame update

    public Rigidbody rigidBody;
    public float MoveSpeed = 10f;
    public float upWardShake = 0.05f;
    public float velocityLimit = 10f;

    private InputDevice rightHand;
    private bool pressRightGrip = false;
    private InputDevice leftHand;
    private bool pressLeftGrip = false;
    public float rayLength = 4f;
    public Transform HitGround;
    public float slopeSpeed = .5f;
    public LayerMask DogMask;
    private Vector3 slopeDir = Vector3.zero;
    Vector3 hitNormal;
    Vector3 normalPlane;
    //public bool isPrepare = false;
    void Start()
    {

        normalPlane = new Vector3(0, 1, 0);
        foreach(var item in pace)
        {
            rightPawMovepace.Add(rightInput.name + item.name);
            leftPawMovepace.Add(leftInput.name + item.name);
        }
    
        rigidBody = this.GetComponent<Rigidbody>();
        InitialDevice();
    }

    // Update is called once per frame
    void Update()
    {
        if (rightHand.TryGetFeatureValue(CommonUsages.gripButton, out bool rgb))
        {
            
            pressRightGrip = rgb;
        }
        if (leftHand.TryGetFeatureValue(CommonUsages.gripButton, out bool lgb))
        {

            pressLeftGrip = lgb;
        }
        

        DetectAngle();

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

    public void RecieveDogPawsBehavior(string pawsInput)
    {
        if (canMove)
        {
            if (rightPawMovepace.Contains(pawsInput))
            {
                addFroceToDogBody();
            }
            else if (leftPawMovepace.Contains(pawsInput))
            {
                addFroceToDogBody();
            }
        }
       
    }

    private void addFroceToDogBody()
    {
            Vector3 dir = Vector3.zero;
            if(pressRightGrip || pressLeftGrip)
            {
                dir = Vector3.ProjectOnPlane(Camera.main.transform.forward, normalPlane) * -1;
                
            }
            else
            {
                dir = Vector3.ProjectOnPlane(Camera.main.transform.forward, normalPlane);
            } 
            //add small froce toward up
            dir = dir + upWardShake * Vector3.up;
            dir = dir.normalized;
            if (DetectAngle())
            {
                dir = Vector3.ProjectOnPlane(dir, hitNormal);
            dir = dir * slopeSpeed;
            }
            if(rigidBody.velocity.magnitude < velocityLimit)
            {
                rigidBody.AddForce(dir * MoveSpeed, ForceMode.Impulse);
            }
            
    }

    bool DetectAngle()
    {
        Ray ray = new Ray(HitGround.position, Vector3.down);
        Debug.DrawRay(HitGround.position, Vector3.down,Color.black);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, rayLength, DogMask))
        {
            print(hit.collider.name);
            hitNormal = hit.normal;
            float slopeAngle = Vector3.Angle(hitNormal, Vector3.up);
            print(slopeAngle);
            if(slopeAngle > 0)
            {
                return true;
            }
        }
        return false;
    }

    
}
