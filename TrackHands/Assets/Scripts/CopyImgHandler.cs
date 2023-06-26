using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Linq;
using UnityEngine.UI;

public class CopyImgHandler : MonoBehaviour
{



	public static List<Gesture> gestures;
	// public static float SMALL = 10f;
	public static float SIZE = 8f;
	// public static float LARGE = 30f;

	public static string SMALL = "SMALL";
    public static string MED = "MEDIUM";
    public static string LARGE = "LARGE";

    public static string SIMPLE = "SIMPLE";
    public static string MEDIUM = "MEDIUM_C";
    public static string COMPLEX = "COMPLEX";
    // Start is called before the first frame update
    // void Start()
    // {
    // 	blue = Resources.Load<Material>("blue");
    // 	white = Resources.Load<Material>("Circuit");

    // 	// blank = Resources.Load("3700b3", typeof(Material)) as Material;
    // 	imgs = Resources.LoadAll("Gestures", typeof(Material)).Cast<Material>().ToArray();    
    // 	gestures = CreateGestureList();

    // 	// SetNewImgToCopy(gameObject);
    // }

    public static void CreateGestureList(Material[] imgs) {
    	gestures = new List<Gesture>();
    	for (int i = 1; i < 13; i++) {
    		gestures.Add(new Gesture(i, SMALL, imgs[i-1]));
    		gestures.Add(new Gesture(i, MED, imgs[i-1]));
    		gestures.Add(new Gesture(i, LARGE, imgs[i-1]));
    	}
    	// return gestures;
    }

    public static void ClearGestures() {
        gestures.Clear();
    }

    public static void SetNewImgToCopy(GameObject canvas) {
        Debug.Log("Gesture size: " + gestures.Count + ", " + PlayerPrefs.GetInt("overallState"));
    	if (gestures.Count > 0) {
    		int r = Random.Range(0,gestures.Count);
	    	// Debug.Log(r + ", " + gestures.Count);
	    	Gesture g = gestures[r];
	    	gestures.RemoveAt(r);
            
            GameObject.Find("Text").GetComponent<Text>().text = g.GetSize();
            

	    	// float size = g.GetSize();
	    	canvas.transform.localScale = new Vector3(SIZE, SIZE, canvas.transform.localScale.z);
	    	// Debug.Log(size + ", " + canvas.transform.localScale);

            SaveData.SetGestureName(g.GetName());
            SaveData.SetGestureSize(g.GetSize());
            SaveData.SetGestureComplexity(g.GetComplexity());

	    	// canvas.GetComponent<Renderer>().material = g.GetMaterial();
	    	SetImgMaterial(canvas, g.GetMaterial());
	    	
	    } else {
            // int overallState = PlayerPrefs.GetInt("overallState");
            // overallState = overallState+1;
            // PlayerPrefs.SetInt("overallState", overallState);
	    	SceneManager.LoadScene("ContinueScene");
	    }
    	
    }

    public static void SetImgMaterial(GameObject canvas, Material m) {
    	canvas.GetComponent<Renderer>().material = m;
    	// Debug.Log("After set img to blank");
    }

  
}
