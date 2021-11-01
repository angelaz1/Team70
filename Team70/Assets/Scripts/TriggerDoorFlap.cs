using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorFlap : MonoBehaviour
{
    public GameObject doorFlap;

    private void Start()
    {
        if (doorFlap == null) doorFlap = GameObject.Find("DoorFlap");
    }

    public void TriggerDoorFlapOpen()
    {
        doorFlap.GetComponent<Animator>().SetTrigger("OpenFlap");
    }
}
