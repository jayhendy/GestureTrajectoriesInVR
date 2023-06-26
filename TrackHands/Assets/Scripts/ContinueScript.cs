using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ContinueScript : MonoBehaviour
{
	// public VideoPlayer vp_VideoPlayerRef;
	public GameObject intro, pinch, index, touch;
	public GameObject button;
	public GameObject text, startText, nonDominantText;

	public void Start() {

		text.SetActive(false);
		startText.SetActive(false);
		nonDominantText.SetActive(false);

		int state = PlayerPrefs.GetInt("overallState");
		if (state == 0) {
			PlayClip(intro);
			startText.SetActive(true);
			pinch.SetActive(false);
			index.SetActive(false);
			touch.SetActive(false);
		} else if ((state%4) == 1) {
			// go into types of tasks
			int taskState = PlayerPrefs.GetInt("taskState");
			taskState = taskState + 1;
			nonDominantText.SetActive(true);

			PlayerPrefs.SetInt("taskState", taskState);
			string[] condArr = (PlayerPrefs.GetString("conditionOrdering")).Split(',');


			Debug.Log("Task state "+ taskState);
			switch (condArr[taskState]) {
				case ("Pinch"):
					PlayClip(pinch);
					intro.SetActive(false);
					index.SetActive(false);
					touch.SetActive(false);
					break;
				case ("Touch"):
					PlayClip(touch);
					pinch.SetActive(false);
					index.SetActive(false);
					intro.SetActive(false);
					break;
				case ("Index"):
					PlayClip(index);
					pinch.SetActive(false);
					intro.SetActive(false);
					touch.SetActive(false);
					break;
			}


		} else {
			button.SetActive(true);
			if ((state%4)==0) {
				text.SetActive(true);
			}
		}
	}

    public void Continue() {
    	pinch.SetActive(false);
		intro.SetActive(false);
		touch.SetActive(false);
		index.SetActive(false);
    	button.SetActive(false);
    	int state = PlayerPrefs.GetInt("overallState") + 1;
    	PlayerPrefs.SetInt("overallState", state);
    	Debug.Log("Current overall state " + state);

    	if ((state < 2) || ((state%4)==1)) {
    		SceneManager.LoadScene("ContinueScene");
    	} else {
			SceneManager.LoadScene("ExperimentalTask");
    	}
    	

    }
    // public void PlayVideo(string videoName)
    // {
    //      VideoClip clip = Resources.Load<VideoClip>(videoName) as VideoClip;
    //      vp_VideoPlayerRef.clip = clip;
    //      vp_VideoPlayerRef.Play();
    // }

     public void PlayClip(GameObject video)
    {
    	VideoPlayer vp = video.GetComponent<VideoPlayer>();
         // VideoClip clip = Resources.Load<VideoClip>(videoName) as VideoClip;
    	video.SetActive(true);
        vp.Play();
    }
}
