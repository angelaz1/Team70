using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;    
    }

    void Update()
    {
        transform.LookAt(cam.transform);

        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
    }
}
