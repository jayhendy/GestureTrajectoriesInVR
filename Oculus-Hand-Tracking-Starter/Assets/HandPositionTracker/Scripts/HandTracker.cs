using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracker : MonoBehaviour
{
    // Unity serialized accessors 
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    [SerializeField] private GameObject trackPointPrefab;

    GameObject trackPoint;
    private bool isPinching;
    private bool isTracking;
    // Start is called before the first frame update
    void Start()
    {
        if(leftHand != null && rightHand != null) {
            isPinching = false;
            isTracking = true;
            trackPoint = Instantiate(trackPointPrefab);           // Instancing tracker on bones
            return;
        }
        
        isTracking = false;
        Debug.LogError("Missing hand object references!");
        Destroy(this);    
    }

    // Update is called once per frame
    void Update()
    {
        if (rightHand.GetComponent<OVRHand>().GetFingerIsPinching(OVRHand.HandFinger.Index)) {
            trackPoint.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            isPinching = true;
        } else {
            trackPoint.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            isPinching = false;
        }   

        trackPoint.transform.position = rightHand.GetComponent<OVRSkeleton>().Bones[(int) OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
    }

    // returns the tracked position for the condition
    public Vector3 GetTrackedPosition() {
        return trackPoint.transform.position;
    }

    // check if the hand(s) are being tracked
    public bool IsTracking() {
        return isTracking;
    }

    // check if the hand is pinching
    public bool IsPinching() {
        return isPinching;
    }
}
