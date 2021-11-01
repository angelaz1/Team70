using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Transform cameraMirro;
    public Transform mainCamera;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = mainCamera.position - cameraMirro.position;
        cameraMirro.LookAt(dir);

    }
}
