using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRecognize : MonoBehaviour
{
    public GameObject currentHold;
    public bool isHold = false;






    public void GrabSomething(GameObject go)
    {
        currentHold = go;
        isHold = true;
    }

    public void DropSomething()
    {
        currentHold = null;
        isHold = false;
    }

  
}
