using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRecognize : MonoBehaviour
{
    public GameObject currentHold;
    public bool isHold = false;
    public Transform muzzleLocation;
    public Transform XRrig;
    public float recordTime = 0.01f;
    public float QueueLimit = 10;//when reach the limit do an calculation of the velocity
    public Queue<float> angleList = new Queue<float>();
    public float throwThreshold = 200f;//when angularVelocity's abs larger than this throw the holding thing;
    public float startFroce = 5f;//throw object with that centrifugalforce
    public float startVelocity = 5f;//object have that inertia



    private float w = 0;//Head angular velocity
    private float r = 1f;// radius
    private float v = 0f;//holding objects line speed
    private float centrifugalForce = 0;
    private Vector3 escapeVelocity;
    private float t = 0;
    private Vector3 lastHeadProjectOnXZPlane;
    private Vector3 nowHeadProjectOnXZPlane;
   
    

    private void Start()
    {
        lastHeadProjectOnXZPlane = ProjectHeadOnXZPlane();
    }

    private void Update()
    {
        CalculateHeadAngularVelocity();
        ThrowObjects();
    }



    public void ThrowObjects()
    {
        if (isHold && currentHold.GetComponent<InteractableObjects>().canThrow)
        {
            if(Mathf.Abs(w) > throwThreshold)
            {
                GameObject go = currentHold;
                go.GetComponent<InteractableObjects>().CanNotGrabColdDown();
                go.GetComponent<InteractableObjects>().Dropped();
                Rigidbody objectRigidbody = go.GetComponent<Rigidbody>();
                objectRigidbody.velocity = go.transform.right * (w * 2 * Mathf.PI / 360) * r * startVelocity;
                centrifugalForce = Mathf.Pow((w * 2 * Mathf.PI / 360), 2) * r * startFroce;
                objectRigidbody.AddForce(go.transform.forward * centrifugalForce, ForceMode.Impulse);
            }
        }
    }







    /// <summary>
    /// calculate Head AngularVelocity
    /// </summary>
    private void CalculateHeadAngularVelocity()
    {
        t += Time.deltaTime;
        if (angleList.Count < QueueLimit)
        {  
            if (t >= recordTime)
            {
                t = 0;
                nowHeadProjectOnXZPlane = ProjectHeadOnXZPlane();
                float angle = HeadTurnAngle(nowHeadProjectOnXZPlane, lastHeadProjectOnXZPlane);
                lastHeadProjectOnXZPlane = nowHeadProjectOnXZPlane;
                angleList.Enqueue(angle);
            }
        }
        else if(angleList.Count == QueueLimit)
        {
            if(t >= recordTime)
            {
                t = 0;
                nowHeadProjectOnXZPlane = ProjectHeadOnXZPlane();
                float angle = HeadTurnAngle(nowHeadProjectOnXZPlane, lastHeadProjectOnXZPlane);
                lastHeadProjectOnXZPlane = nowHeadProjectOnXZPlane;
                angleList.Dequeue();
                angleList.Enqueue(angle);
                w = Sum(angleList.ToArray()) / (recordTime * QueueLimit);
                print(w);
            }
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
    /// <summary>
    /// two vector3's angle with dirction
    /// </summary>
    /// <param name="now"></param>
    /// <param name="last"></param>
    /// <returns></returns>
    public float HeadTurnAngle(Vector3 now, Vector3 last)
    {
        float newAngle = Vector3.Angle(now, last);
        if(Vector3.Cross(now, last).y > 0)
        {
            newAngle = -newAngle;
        }
        return newAngle;
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

  
    public float Sum(float[] array)
    {
        float sum = 0;
        foreach(var item in array)
        {
            sum += (float)item;
        }
        return sum;
    }
}
