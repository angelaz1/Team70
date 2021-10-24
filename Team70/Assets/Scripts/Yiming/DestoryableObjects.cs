using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryableObjects : MonoBehaviour
{
    public int health = 3;
    Animator animator;



    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {

        Destroy();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Dog")
        {
            health--;
            animator.SetInteger("Health", health);
        }
    }

    public void Destroy()
    {
        if(health == 0)
        {
            Destroy(this.gameObject,  1.5f);
        }
    }

}
