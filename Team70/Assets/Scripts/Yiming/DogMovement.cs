using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{

    public GameObject rightInput;
    public GameObject leftInput;
    public List<GameObject> pace = new List<GameObject>();

    
    private List<string> rightPawMovepace = new List<string>();
    private List<string> leftPawMovepace =  new List<string>();
    // Start is called before the first frame update

    public Rigidbody rigidBody;
    public float MoveSpeed = 10f;
    public float upWardShake = 0.05f;
    public float velocityLimit = 10f;
    //public bool isPrepare = false;
    void Start()
    {
     
        foreach(var item in pace)
        {
            rightPawMovepace.Add(rightInput.name + item.name);
            leftPawMovepace.Add(leftInput.name + item.name);
        }
    
        rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveDogPawsBehavior(string pawsInput)
    {
         if(rightPawMovepace.Contains(pawsInput))
        {
            addFroceToDogBody();
        }
        else if(leftPawMovepace.Contains(pawsInput))
        {
            addFroceToDogBody();
        }
    }

    private void addFroceToDogBody()
    {
            Vector3 dir = Camera.main.transform.forward;
            //add small froce toward up
            dir = dir + upWardShake * Vector3.up;
            if(rigidBody.velocity.magnitude < velocityLimit)
            {
                rigidBody.AddForce(dir * MoveSpeed, ForceMode.Impulse);
            }
            
    }
}
