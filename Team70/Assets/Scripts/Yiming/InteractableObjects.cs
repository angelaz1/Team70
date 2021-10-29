using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class InteractableObjects : MonoBehaviour
{
    //not a task object, can be pick up
    //need to shake your head to some speed to throw it away
    //need to use the button?
    //may not

    public bool isGrab = false;
    public bool canThrow = true;
    public int Priority = 0;
    private HeadRecognize headRecognize;
    private Transform muzzleLocation;
    private Rigidbody rigidbody;

    private void Start()
    {
        headRecognize = FindObjectOfType<HeadRecognize>();
        muzzleLocation = GameObject.Find("MuzzleLocation").GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
    }


    private void OnTriggerEnter(Collider other)
    {
        print("collision");
        if(other.tag == "Player")
        {
            Grabbed();
        }
    }
    /// <summary>
    /// when object was grabbed call this function
    /// </summary>
    public void Grabbed()
    {
        if(!headRecognize.isHold)
        {
            if (!isGrab)
            {
                this.transform.position = muzzleLocation.position;
                this.transform.SetParent(muzzleLocation);
                this.transform.localRotation = Quaternion.identity;
                rigidbody.isKinematic = true;
                isGrab = true;
                headRecognize.GrabSomething(this.gameObject);
            }
        }
        else
        {
            
        }
      
    }

    public void Dropped()
    {
        if (isGrab && canThrow)
        {
            rigidbody.isKinematic = false;
            this.transform.SetParent(null);
            isGrab = false;
            headRecognize.DropSomething();
        }
    }

}
