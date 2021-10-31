using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject simulatorObjects;
    public GameObject actualObjects;

    public List<GameObject> nodeList = new List<GameObject>();
    private TutorialNode currentNode;
    public GameObject startNode;
    public Vector3 generatePlacer;
    public float distance = 20f;
    Transform dogTransform;
    public Text tutorialTxt;
    private void Start()
    {
        simulatorObjects.SetActive(!XRSettings.isDeviceActive);
        actualObjects.SetActive(XRSettings.isDeviceActive);

        RenderSettings.skybox.SetColor("_Tint", Color.black);
        GameObject go = Instantiate(startNode);
        currentNode = startNode.GetComponent<TutorialNode>();
        dogTransform = GameObject.Find("dog").GetComponent<Transform>();
        for (int i=0; i < nodeList.Count; i++)
        {
            nodeList[i].GetComponent<TutorialNode>().num = i;
        }
    }

    public void GenerateNewNode()
    {
        if (currentNode.nextNode)
        {
            generatePlacer = dogTransform.position + dogTransform.forward * distance;
            GameObject go = Instantiate(nodeList[currentNode.nextNode.num], generatePlacer, Quaternion.identity);
            currentNode = go.GetComponent<TutorialNode>();
            tutorialTxt.color = new Color(255, 255, 255, 1);
            tutorialTxt.text = currentNode.tutorialText;
            StartCoroutine(TxtDisappear());
        }
    }

    IEnumerator TxtDisappear()
    {
        yield return new WaitForSeconds(2);
        tutorialTxt.color = new Color(255, 255, 255, 0);
    }
}
