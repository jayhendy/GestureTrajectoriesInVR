using TMPro;
using UnityEngine;

public class SampleHandPositionAccessor : MonoBehaviour
{
    [SerializeField] private PositionTracker positionTracker;
    [SerializeField] private GameObject trackPointPrefab;

    [Space]
    [SerializeField] private TextMeshProUGUI[] texts;       // List of text to be updated depending on bones position
    private Transform[] trackPoints = new Transform[6];     // List of visual trackers for bones

    private void Start() {
        // Searching for PositionTracker
        if(positionTracker == null) {
            Debug.LogError("Missing PositionTracker object reference!");
            Destroy(this);
        }

        if(texts.Length != 6) {
            Debug.LogError("Missing TextMeshProUGUI objects references!");
            Destroy(this);
        }

        // Adding visual trackers on bones 
        for(int i=0; i<6; i++) {
            trackPoints[i] = Instantiate(trackPointPrefab).transform;           // Instancing tracker on bones
            if(i % 3 == 0) trackPoints[i].localScale = Vector3.one * .075f;     // Adjusting size of hand tracker to appears bigger
        }
            
    }

    private void Update() {
        // Following code is just a sample way of extracting / exploiting data from bones position.

        texts[0].text = "Left Hand:\n" + positionTracker.LeftHandPosition;
        trackPoints[0].position = positionTracker.LeftHandPosition;

        texts[1].text = "Left Thumb:\n" + positionTracker.LeftThumbPosition;
        trackPoints[1].position = positionTracker.LeftThumbPosition;

        texts[2].text = "Left Index:\n" + positionTracker.LeftIndexPosition;
        trackPoints[2].position = positionTracker.LeftIndexPosition;

        texts[3].text = "Right Hand:\n" + positionTracker.RightHandPosition;
        trackPoints[3].position = positionTracker.RightHandPosition;

        texts[4].text = "Right Thumb:\n" + positionTracker.RightThumbPosition;
        trackPoints[4].position = positionTracker.RightThumbPosition;

        texts[5].text = "Right Index:\n" + positionTracker.RightIndexPosition;
        trackPoints[5].position = positionTracker.RightIndexPosition;
    }
}
