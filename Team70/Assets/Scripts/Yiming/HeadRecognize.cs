using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRecognize : MonoBehaviour
{
    public GameObject currentHold;
    public bool isHold = false;
    public Transform muzzleLocation;
    public Transform XRrig;
    private float w = 0;//Head angular velocity
    private float r = 1f;// radius
    private float v = 0f;//holding objects line speed
    private float centrifugalForce = 0;
    private Vector3 escapeVelocity;
    public float recordTime = 0.01f;
    private float t = 0;
    private Vector3 lastHeadProjectOnXZPlane;
    private Vector3 nowHeadProjectOnXZPlane;
    //public List<Vector3> ProjectHeadOnXZPlane = new List<Vector3>();
    public Queue<float> angleList;

    private void Start()
    {
        lastHeadProjectOnXZPlane = ProjectHeadOnXZPlane();
    }

    private void Update()
    {
        t += Time.deltaTime;
        if(t >= recordTime)
        {
            t = 0;
            nowHeadProjectOnXZPlane = ProjectHeadOnXZPlane();
            float angle = Vector3.Angle(nowHeadProjectOnXZPlane, lastHeadProjectOnXZPlane);
            if(Vector3.Cross(nowHeadProjectOnXZPlane, lastHeadProjectOnXZPlane).y > 0)
            {
                angle = -angle;
            }
            lastHeadProjectOnXZPlane = nowHeadProjectOnXZPlane;
            print(angle);
        }
    }










    /// <summary>
    /// make head vector3 project on XZ plane
    /// </summary>
    /// <returns></returns>
    public Vector3 ProjectHeadOnXZPlane()
    {
        Vector3 headVector = muzzleLocation.position - XRrig.position;
        headVector = Vector3.ProjectOnPlane(headVector, Vector3.up);
        headVector = headVector.normalized;
        return headVector;
    }





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
