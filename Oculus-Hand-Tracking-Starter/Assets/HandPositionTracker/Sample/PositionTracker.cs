using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    // Unity serialized accessors 
    [SerializeField] private OVRSkeleton leftHandSkeleton;
    [SerializeField] private OVRSkeleton rightHandSkeleton;
    
    // Left hand tracked points
    public Vector3 LeftHandPosition {get; private set;}
    public Vector3 LeftThumbPosition {get; private set;}
    public Vector3 LeftIndexPosition {get; private set;}

    // Right hand tracked points
    public Vector3 RightHandPosition {get; private set;}
    public Vector3 RightThumbPosition {get; private set;}
    public Vector3 RightIndexPosition {get; private set;}

    private void Start() {
        if(leftHandSkeleton != null && rightHandSkeleton != null)
            return;

        Debug.LogError("Missing an OVRSkeleton object reference!");
        Destroy(this);
    }

    private void Update()
    {
        // Tutorial : https://9to5tutorial.com/how-to-get-finger-joint-information-and-more-with-oculusquest-hand-tracking
    
        LeftHandPosition = leftHandSkeleton.Bones[(int) OVRSkeleton.BoneId.Hand_WristRoot].Transform.position;
        LeftThumbPosition = leftHandSkeleton.Bones[(int) OVRSkeleton.BoneId.Hand_ThumbTip].Transform.position;
        LeftIndexPosition = leftHandSkeleton.Bones[(int) OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;

        RightHandPosition = rightHandSkeleton.Bones[(int) OVRSkeleton.BoneId.Hand_WristRoot].Transform.position;
        RightThumbPosition = rightHandSkeleton.Bones[(int) OVRSkeleton.BoneId.Hand_ThumbTip].Transform.position;
        RightIndexPosition = rightHandSkeleton.Bones[(int) OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;  
    }
}
