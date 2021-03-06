using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractableObjects : MonoBehaviour
{
    //not a task object, can be pick up
    //need to shake your head to some speed to throw it away
    //need to use the button?
    //may not
    public bool canGrab = true;//when throw something it willhave 1 or 2 second cold down that can not grab
    public bool isGrab = false;
    public bool canThrow = true;
    public int Priority = 0;
    private HeadRecognize headRecognize;
    private Transform muzzleLocation;
    [Tooltip("Priority of object when being grabbed. Higher number = higher priority")]
    private Rigidbody rigidbody;
    public float coldDown = 1f;
    public string TipsToThrow = "Shake your head to throw!";
    private TextMeshProUGUI tipsTxt;

    private void Start()
    {
        headRecognize = FindObjectOfType<HeadRecognize>();
        muzzleLocation = GameObject.Find("MuzzleLocation").GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        tipsTxt = GameObject.Find("TutorialText").GetComponent<TextMeshProUGUI>();
    }


    private void OnTriggerEnter(Collider other)
    {
        //print("collision");
        if(other.tag == "Player" && canGrab)
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
                GrabObject();
            }
        }
        else
        {
            if(headRecognize.currentHold.GetComponent<InteractableObjects>().Priority < Priority)
            {
                headRecognize.currentHold.GetComponent<InteractableObjects>().Dropped();
                GrabObject();
            }
        }
    }

    void GrabObject()
    {
        this.transform.position = muzzleLocation.position;
        this.transform.SetParent(muzzleLocation);
        this.transform.localRotation = Quaternion.identity;
        rigidbody.isKinematic = true;
        isGrab = true;
        headRecognize.GrabSomething(this.gameObject);
        //tips to throw the object
        if (canThrow)
        {
            tipsTxt.text = TipsToThrow;
            tipsTxt.color = new Color(255, 255, 255, 1);
            StartCoroutine(TipsDisappear());
        }


        SnappableObject snappable = GetComponent<SnappableObject>();
        if (snappable)
        {
            // This is a task object
            snappable.SetGrabbed(true);
        }
    }

    IEnumerator TipsDisappear()
    {
        yield return new WaitForSeconds(3.5f);
        tipsTxt.color = new Color(255, 255, 255, 0);
    }


    public void Dropped()
    {
        if (isGrab)
        {
            rigidbody.isKinematic = false;
            this.transform.SetParent(null);
            isGrab = false;
            headRecognize.DropSomething();

            SnappableObject snappable = GetComponent<SnappableObject>();
            if (snappable)
            {
                // This is a task object
                snappable.SetGrabbed(false);
            }
        }
    }

    public void CanNotGrabColdDown()
    {
        canGrab = false;
        StartCoroutine(ColdDown());
    }

    IEnumerator ColdDown()
    {
        yield return new WaitForSeconds(coldDown);
        canGrab = true;
    }
}
