using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;
using System;

public class TaskManager : MonoBehaviour
{

    public Text pidTextField;
    public Toggle isLeft;


    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    public void Update()
    {
		// if (Input.GetKeyDown(KeyCode.Return)) {
		// 	StartTask(int.Parse(pidTextField.text));
		// }        
    }




    public void StartTask() {

        int id = int.Parse(pidTextField.text);
        PlayerPrefs.SetInt("taskState", -1);
        PlayerPrefs.SetInt("overallState",0);
        PlayerPrefs.SetInt("pid", id);

        // PlayerPrefs.SetInt("ordering", ordering.value);

        PlayerPrefs.SetInt("left", isLeft.isOn?1:0);
        // Debug.Log("Uh hello");
        string conditionOrderString = GetConditionOrdering(id);
        // Debug.Log("///// " + conditionOrderString);
        PlayerPrefs.SetString("conditionOrdering",conditionOrderString);

        SaveData.SetFilePath(id);
    	SceneManager.LoadScene("ContinueScene");
    }

    // public static string GetConditionOrdering(int pid) {

    //     int conditionOrdering = pid%3;
    //     string conditions = "";
    //     switch (conditionOrdering) {

    //         case (0):
    //             conditions = "[\"Touch\", \"Index\", \"Pinch\"]";
    //             break;
    //         case (1):
    //             conditions = "[\"Index\", \"Pinch\", \"Touch\"]";
    //             break;
    //         case (2):
    //             conditions = "[\"Pinch\", \"Touch\", \"Index\"]";
    //             break;

    //     }
    //     return conditions;
    // }


     public static string GetConditionOrdering(int pid) {

        int order = pid%3;
        // Debug.Log(order);
        string conditions = "";
        switch (order) {

            case (0):
                conditions = "Touch,Index,Pinch";
                break;
            case (1):
                conditions = "Index,Pinch,Touch";
                break;
            case (2):
                conditions = "Pinch,Touch,Index";
                break;

        }

        return conditions;
    }
}