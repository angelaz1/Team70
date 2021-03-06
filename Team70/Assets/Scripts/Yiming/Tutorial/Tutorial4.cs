using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial4 : TutorialNode
{
    public float delayTime = 0.5f;
    private bool changeColor = false;
    public AudioClip pickDownSound;
    private void OnTriggerEnter(Collider other)
    {
        GameObject t3 = FindObjectOfType<Tutorial3>().gameObject;
        bool withBone = FindObjectOfType<Tutorial3>().isGrab;
        print(withBone);
        if(other.tag == "Dog" && withBone)
        {
            t3.transform.position = transform.position;
            t3.transform.rotation = Quaternion.identity;
            t3.transform.SetParent(transform);
            if (tutorialClip) { this.GetComponent<AudioSource>().PlayOneShot(tutorialClip); }
            AudioSource aSource = this.gameObject.AddComponent<AudioSource>();
            aSource.PlayOneShot(pickDownSound);
            EndNode();
        }
    }

    private void Update()
    {
        if (changeColor)
        {
            Color w = Color.white;
            Color now = RenderSettings.skybox.GetColor("_Tint");
            now = Color.Lerp(now, w, .2f * Time.deltaTime);
            RenderSettings.skybox.SetColor("_Tint", now);

        }
    }

    public override void EndNode()
    {
        if (!changeColor)
        { 
            changeColor = true;
            StartCoroutine(JumpScene());
        }
    }

    IEnumerator JumpScene()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadSceneAsync("MainGame");
    }
}
