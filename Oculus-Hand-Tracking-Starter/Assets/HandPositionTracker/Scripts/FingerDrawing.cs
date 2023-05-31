using UnityEngine;
using OculusSampleFramework;

public class FingerDrawing : MonoBehaviour
{
    [SerializeField] public LineRenderer lineRenderer;
    [SerializeField] private HandTracker handTracker;

    private bool isDrawing = false;


    private void Update()
    {
        if (handTracker.IsTracking())
        {
            Vector3 fingerTipPosition = handTracker.GetTrackedPosition();
            
            if (handTracker.IsPinching())
            {
                if (!isDrawing)
                {
                    isDrawing = true;
                    lineRenderer.positionCount = 1;
                    lineRenderer.SetPosition(0, fingerTipPosition);
                }
                else
                {
                    int positionCount = lineRenderer.positionCount;
                    lineRenderer.positionCount = positionCount + 1;
                    lineRenderer.SetPosition(positionCount, fingerTipPosition);
                }
            }
            else
            {
                isDrawing = false;
            }
        }
        else
        {
            isDrawing = false;
        }
    }
}