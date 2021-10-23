using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using UnityEngine.XR;
using UnityEngine.Events;

[System.Serializable]
public class NewStringEvent : UnityEvent<string> { }
public class PawsRecognize : MonoBehaviour
{
    public Transform handT;
    public Transform rigT;
    public Vector3 ReversoHandT;
    private List<Gesture> gesturesList = new List<Gesture>();//store gesture date to match
    public List<Gestures> gestureStoreList = new List<Gestures>();//the prefab store gestures pos data

    public GameObject debugCubePrefab;
    public string handName;
    private Queue<Vector3> handPos = new Queue<Vector3>();
    public float recordInterval = 0.05f;
    public float recognitionThreshold = 0.7f;

    public int HandListLimitCount = 10;
    public NewStringEvent onRecognize;
    private void Start()
    {
        handName = handT.name;
    }
    private void FixedUpdate()
    {
        ReversoHandT = handT.position - rigT.position;
        GestureListInitial();
        HandGestureRecognize();
    }
    /// <summary>
    /// right hand gesture recognize without trigger
    /// </summary>
    private void HandGestureRecognize()
    {
        //get data in queue
        if (handPos.Count == 0)
        {
            handPos.Enqueue(ReversoHandT);
        }
        if (handPos.Count > 0)
        {
            Vector3[] rightHandPosArray = handPos.ToArray();
            Vector3 latestPos = rightHandPosArray[rightHandPosArray.Length - 1];
            if (Vector3.Distance(latestPos, ReversoHandT) > recordInterval && handPos.Count < HandListLimitCount)
            {
                if (debugCubePrefab) Destroy(Instantiate(debugCubePrefab, ReversoHandT, Quaternion.identity), 1);
                handPos.Enqueue(ReversoHandT);
            }
            //when queue touch the limit long than renew it by enqueue the first one
            else if (Vector3.Distance(latestPos, ReversoHandT) > recordInterval && handPos.Count == HandListLimitCount)
            {
                handPos.Dequeue();
                handPos.Enqueue(ReversoHandT);
                //each time turn the queue into a queue
                rightHandPosArray = handPos.ToArray();
                Gesture newGesture = TurnPositionToGesture(rightHandPosArray);
                //check if this in the gesturesList
                Result result = PointCloudRecognizer.Classify(newGesture, gesturesList.ToArray());
                string movementName = result.GestureClass;
                //print(result.GestureClass + " : " + result.Score);
                if(result.Score > recognitionThreshold)
                {
                    onRecognize.Invoke(handName + movementName);
                    handPos.Clear();
                }
            }
        }
    }


    /// <summary>
    /// to get datas from gesture store and creat gesture list base on it
    /// </summary>
    private void GestureListInitial()
    {
        //Debug.Log(gestureStore.ForwardPositionList.Count);
        gesturesList.Clear();
        if(gestureStoreList.Count > 0)
        {
            foreach(var item in gestureStoreList)
            {
                if(item.gesture.Count > 0)
                {
                    Gesture newGesture = TurnPositionToGesture(item.gesture);
                    newGesture.Name = item.gameObject.name;
                    gesturesList.Add(newGesture);

                }
            }
        }
    }


    /// <summary>
    /// project position as gesture base on Camera.normal and pdollar
    /// </summary>
    /// <param name="positionList"></param>
    /// <returns></returns>
    private Gesture TurnPositionToGesture(List<Vector3> positionList)
    {
        Point[] pointArray = new Point[positionList.Count];
        for (int i = 0; i < positionList.Count; i++)
        {
            Vector3 rightPoint = Vector3.ProjectOnPlane(positionList[i], GameObject.Find("Main Camera").transform.right);
            if (debugCubePrefab) Destroy(Instantiate(debugCubePrefab, rightPoint, Quaternion.identity), 3);
            pointArray[i] = new Point(rightPoint.z, rightPoint.y, 0);
        }
        Gesture newGesture = new Gesture(pointArray);
        return newGesture;
    }
    private Gesture TurnPositionToGesture(Vector3[] positionList)
    {
        Point[] pointArray = new Point[positionList.Length];
        for (int i = 0; i < positionList.Length; i++)
        {
            Vector3 rightPoint = Vector3.ProjectOnPlane(positionList[i], GameObject.Find("Main Camera").transform.right);
            if (debugCubePrefab) Destroy(Instantiate(debugCubePrefab, rightPoint, Quaternion.identity), 3);
            pointArray[i] = new Point(rightPoint.z, rightPoint.y, 0);
        }
        Gesture newGesture = new Gesture(pointArray);
        return newGesture;
    }
}
