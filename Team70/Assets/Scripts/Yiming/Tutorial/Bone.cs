using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class Bone : MonoBehaviour
{
    public AudioClip appearsound;
    private void OnEnable()
    {
        this.GetComponent<AudioSource>().PlayOneShot(appearsound);
       
    }
}
