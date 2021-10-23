using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{

    public GameObject rightInput;
    public GameObject leftInput;
    public GameObject pace1;
    public GameObject pace2;

    private string rightPawPrepare;
    private string rightPawMovepace;
    private string leftPawPrepare;
    private string leftPawMovepace;
    // Start is called before the first frame update

    public Rigidbody rigidBody;
    public float MoveSpeed = 10f;
    public float upWardShake = 0.05f;
    private bool isRightPrepare = false;
    private bool isLeftPrepare = false;
    private bool isPrepare = false;
    void Start()
    {
        rightPawPrepare = rightInput.name + pace1.name;
        rightPawMovepace = rightInput.name + pace2.name;
        leftPawPrepare = leftInput.name + pace1.name;
        leftPawMovepace = leftInput.name + pace2.name;
        rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveDogPawsBehavior(string pawsInput)
    {
        if(pawsInput == rightPawPrepare)
        {
            print(pawsInput);
            isPrepare = true;
        }
        else if(pawsInput == rightPawMovepace)
        {
            print(pawsInput);
            addFroceToDogBody();
        }
        else if(pawsInput == leftPawPrepare)
        {
            isPrepare = true;
        }
        else if(pawsInput == leftPawMovepace)
        {
            addFroceToDogBody();
        }
    }

    private void addFroceToDogBody()
    {
        if(isPrepare)
        {
            Vector3 dir = Camera.main.transform.forward;
            //add small froce toward up
            dir = dir + upWardShake * Vector3.up;
            rigidBody.AddForce(dir * MoveSpeed, ForceMode.Impulse);
            isPrepare = false;
        }   
    }
}
