using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gestures : MonoBehaviour
{
    private string name;
    public List<Vector3> gesture = new List<Vector3>();
    public bool isDebug = false;

    public GameObject debugCube;

    public void Update()
    {
        if (isDebug && debugCube)
        {
            foreach (var item in gesture)
            {
                Instantiate(debugCube, item, Quaternion.identity);
            }

        }
    }
}
