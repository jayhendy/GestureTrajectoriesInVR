using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.Video;
 using UnityEngine.SceneManagement;
 
 public class ShowButton : MonoBehaviour
 { 
      // public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
      public GameObject button;     
     void Start() 
     {
     	
        gameObject.GetComponent<VideoPlayer>().loopPointReached += LoadScene;
     }

     void Update() {
     	if (Input.GetKeyDown(KeyCode.S)) {
            button.SetActive(true);

        }

     }
     void LoadScene(VideoPlayer vp)
     {
          button.SetActive(true);
      }
  }

