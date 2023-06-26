using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

using System;

public class HandPoint
{


	/**

	Important things: 
	direction, isExtended, tipPosition

	**/
	public float confidence;

  	/** thumb **/
  	public Vector thumbTip, thumbDirection;
  	public bool thumbExtended;
	/** index **/
	public Vector indexTip, indexDirection;
	public bool indexExtended;
	/** middle **/
	public Vector middleTip, middleDirection;
	public bool middleExtended;
	/** ring **/
	public Vector ringTip, ringDirection;
	public bool ringExtended;
	/** pinky **/
	public Vector pinkyTip, pinkyDirection;
	public bool pinkyExtended;
	/** palm **/
	public Vector palmPos, stabPalmPos, palmNorm;
	/** wrist **/
	public Vector wristPos;

  	public long time;
  	public bool isPinching, isDrawing;


	// public HandPoint(Vector3 i, Vector3 t, Vector3 p, bool pinch, bool draw) {
	// 	index = i;
	// 	thumb = t;
	// 	palm = p;
	// 	isPinching = pinch;
	// 	isDrawing = draw;
	// 	time = DateTime.Now.Ticks-SaveData.BEGIN.Ticks;
	// }

	public HandPoint(Hand h, bool pinch, bool draw) {

		Debug.Log("Confidence: " + h.Confidence);

		confidence = h.Confidence;
		palmPos = h.PalmPosition;
		palmNorm = h.PalmNormal;
		stabPalmPos = h.StabilizedPalmPosition;
		wristPos = h.WristPosition;


		for (int i = 0; i < h.Fingers.Count; i++) {
			switch (h.Fingers[i].Type) {
				case (Finger.FingerType.TYPE_THUMB):
					thumbTip = h.Fingers[i].TipPosition;
					thumbDirection = h.Fingers[i].Direction;
					thumbExtended = h.Fingers[i].IsExtended;
					// stabThumbTip = h.Fingers[i].StabilizedTipPosition;
					break;
				case (Finger.FingerType.TYPE_INDEX):
					indexTip = h.Fingers[i].TipPosition;
					indexDirection = h.Fingers[i].Direction;
					indexExtended = h.Fingers[i].IsExtended;

					// stabIndexTip = h.Fingers[i].StabilizedTipPosition;
					break;
				case (Finger.FingerType.TYPE_MIDDLE):
					middleTip = h.Fingers[i].TipPosition;
					middleDirection = h.Fingers[i].Direction;
					middleExtended = h.Fingers[i].IsExtended;
					// stabMiddleTip = h.Fingers[i].StabilizedTipPosition;
					break;
				case (Finger.FingerType.TYPE_RING):
					ringTip = h.Fingers[i].TipPosition;
					ringDirection = h.Fingers[i].Direction;
					ringExtended = h.Fingers[i].IsExtended;
					// stabRingTip = h.Fingers[i].StabilizedTipPosition;

					break;
				case (Finger.FingerType.TYPE_PINKY):
					pinkyTip = h.Fingers[i].TipPosition;
					pinkyDirection = h.Fingers[i].Direction;
					pinkyExtended = h.Fingers[i].IsExtended;
					// stabPinkyTip = h.Fingers[i].StabilizedTipPosition;

					break;
			}
		}



		isPinching = pinch;
		isDrawing = draw;
		time = DateTime.Now.Ticks-SaveData.BEGIN.Ticks;
	}

	// public string GetJsonString() {
	// 	return JsonUtility.ToJson(this);
		
	// }

}
