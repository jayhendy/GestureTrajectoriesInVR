using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Leap;
using UnityEngine.UI;
using System.Text;
using System;


public enum Status { ShowGesture, BlankBeforeDraw, Drawing, BlankAfterDraw }

public enum Condition { Pinch, Index, Touch }


public class PaintbrushHandler : MonoBehaviour
{
    public Camera cam;


    // tips
    public Transform rIndexTip, lIndexTip, rThumbTip, lThumbTip;
    public Transform rIndexB, lIndexB, rThumbB, lThumbB;
    public GameObject lowPolyLeft, lowPolyRight;

	Transform indexTip, thumbTip, indexB, thumbB, palm;
    float CONFIDENCE_THRESHOLD = 0.70f;
    public GameObject canvasForCopy;
    public GameObject uiCanvas;
    public GameObject screen;
    public GameObject paint;
    // public GameObject cursor;
    public Material red, blue, white;

    public static Condition mode; 
    public static bool pinching;

    public float pinchThreshold;

    public static bool isDrawing, prevIsDrawing;
  
    static string cursorTag = "cursorObj";
    static Vector3 DEFAULT_VEC3 = new Vector3(-999f,-999f,-999f);
    
    Controller controller;
    // public GameObject cS;

    GameObject cS;

    int sphereCount;
    // int status, prevStatus;
    public static Status status, prevStatus;
    float timeLeft = 1.5f;

    // Task Master
    int taskState;
    int handedness;

    // bool DEBUG = true;
    bool DEBUG = false;

    // Start is called before the first frame update
    public void Start()
    {   
        controller = new Controller();
        


        /**
        DEBUG MODE
        **/
        int pid;

        if (DEBUG) {
            handedness = 0;
            mode = Condition.Touch;
            pid = 999;
            SaveData.SetFilePath(pid);
        } else {
            pid = PlayerPrefs.GetInt("pid");
            

            string[] condArr = (PlayerPrefs.GetString("conditionOrdering")).Split(',');
            // Debug.Log(PlayerPrefs.GetString("conditionOrdering"));
            mode = (Condition) Enum.Parse(typeof(Condition), condArr[PlayerPrefs.GetInt("taskState")]);
            // if (TaskMaster.GetTaskState() == 0) {
            //     TaskMaster.Setup(pid);

                
            // } 
            handedness = PlayerPrefs.GetInt("left");

        }
        

        // int handedness = 0;
        if (handedness == 0) {
            indexTip = rIndexTip;
            thumbTip = rThumbTip;
            indexB = rIndexB;
            thumbB = rThumbB;
            if (mode == Condition.Touch) {
                lowPolyRight.SetActive(false);
            } else {
                lowPolyRight.SetActive(true);
            }
            lowPolyLeft.SetActive(false);
        } else {
            indexTip = lIndexTip;
            thumbTip = lThumbTip;
            indexB = lIndexB;
            thumbB = lThumbB;
            if (mode == Condition.Touch) {
                lowPolyLeft.SetActive(false);
            } else {
                lowPolyLeft.SetActive(true);
            }
            lowPolyRight.SetActive(false);
        }







        CopyImgHandler.CreateGestureList(LoadMaterials());
        sphereCount = 0;
        isDrawing = false;
        
        pinching = false;
        prevStatus = Status.Drawing;
        status = Status.BlankAfterDraw;

        SaveData.SetCondition(Enum.GetName(typeof(Condition), mode));
    }


    public void PinchDetected() {
        // Debug.Log("pinch detected");
        if (status == Status.Drawing) {
            pinching = true;
        }
    }

    public void PinchReleased() {
        if (status == Status.Drawing) {
            pinching = false;
        }
    }

    public Material[] LoadMaterials() {
        Material[] items = new Material[12];

        for (int i = 0; i < items.Length; i++) {
            items[i] = Resources.Load("Gestures/Materials/G"+(i+1),typeof(Material)) as Material;
            // items[i] = Resources.Load("Default/Materials/untitled",typeof(Material)) as Material;
        }
        return items;
    }


    public bool DetectPinch(Vector3 t1, Vector3 t2) {
        if (Vector3.Distance(t1, t2) < pinchThreshold) {
            return true;
        }
        return false;
    }   

    public static Vector3 GetMidPoint(Vector3 t1, Vector3 t2) {
        return new Vector3((t1.x+t2.x)/2, (t1.y+t2.y)/2, (t1.z+t2.z)/2);

    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) {
            CopyImgHandler.ClearGestures();
        }


    // timeLeft -= Time.deltaTime;
     // if ( timeLeft < 0 )
     // {
     //     GameOver();
     // }
         if (PlayerPrefs.GetInt("left") == 0) {
            if (mode == Condition.Touch) {
                lowPolyRight.SetActive(false);
            } else {
                lowPolyRight.SetActive(true);
            }
            lowPolyLeft.SetActive(false);
        } else {
             if (mode == Condition.Touch) {
                lowPolyLeft.SetActive(false);
            } else {
                lowPolyLeft.SetActive(true);
            }
            lowPolyRight.SetActive(false);
        }

        switch (status) 
        {
            case (Status.BlankBeforeDraw):
                // cursor.SetActive(false);

                if (prevStatus != status) {
                    uiCanvas.SetActive(false);
                    canvasForCopy.SetActive(false);

                    CopyImgHandler.SetImgMaterial(screen,blue);
                }
                

                    


                timeLeft -= Time.deltaTime;
                if (timeLeft < 0) {
                    status = Status.Drawing;
                    timeLeft = 1.5f;
                } else {
                   prevStatus = status; 
                }
                

                break;

            case (Status.BlankAfterDraw):
                // cursor.SetActive(false);

                if (prevStatus != status) {
                    uiCanvas.SetActive(false);
                    canvasForCopy.SetActive(false);
                    
                    CopyImgHandler.SetImgMaterial(screen,blue);
                }
                    

                timeLeft -= Time.deltaTime;
                if (timeLeft < 0) {
                    status = Status.ShowGesture;
                    timeLeft = 1.5f;
                } else {
                    prevStatus = status;
                }
                

                break;

            case (Status.ShowGesture):

                // cursor.SetActive(false);
                if (prevStatus != status) {
                    
                    uiCanvas.SetActive(true);
                    canvasForCopy.SetActive(true);
                    // Debug.Log("Hello Show Gesture --- " + status + ", " + prevStatus);
                    CopyImgHandler.SetNewImgToCopy(canvasForCopy);
                    
                }
                    
                    // CopyImgHandler.SetImgToWhite(screen);

                // timeLeft -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space)) {
                    status = Status.BlankBeforeDraw;
                    timeLeft = 1.5f;
                } else {
                    prevStatus = status;
                }

                break;

            case (Status.Drawing):    

                

                Vector3 t = DoDrawing(); 
                // ChangeCursor();

                if (t != DEFAULT_VEC3) {
                    if (isDrawing) {
                        SaveData.path.Add((t,DateTime.Now.Ticks-SaveData.BEGIN.Ticks));
                        if (!prevIsDrawing) {
                            cS = CreateSphere("cursorObj"+sphereCount, t, mode);
                            sphereCount++;
                        } else {
                            cS.transform.position = t;
                           
                        }
                
                    }    
          

                }
               // Debug.Log("Index " + indexTip.position.y);     
               // Debug.Log("Thumb" + thumb.position.y);
               // Debug.Log(t.y);
               


            
                   
                break;
          }

    }





    public static bool IsDrawing() {
        return isDrawing;
    }


    public Vector3 DoDrawing() {
        if (prevStatus != status) {
                    // CopyImgHandler.SetImgToWhite(canvasForCopy);
            uiCanvas.SetActive(false);
            canvasForCopy.SetActive(false);
            CopyImgHandler.SetImgMaterial(screen,white);

            SaveData.SetTimeTrialStart();
        }

        Frame frame = controller.Frame();
        bool extendedBool = true;
        for (int i = 0; i < frame.Hands.Count; i++) {
            if ((frame.Hands[i].IsLeft) && (PlayerPrefs.GetInt("left") == 1)) {
                // LH
                extendedBool = frame.Hands[i].Fingers[1].IsExtended;
                HandPoint hp = new HandPoint(frame.Hands[i], DetectPinch(indexTip.position, thumbTip.position), isDrawing);
                SaveData.writeHandPoint(hp);
            
            } else if ((!frame.Hands[i].IsLeft) && (PlayerPrefs.GetInt("left") == 0)) {
                // RH
                extendedBool = frame.Hands[i].Fingers[1].IsExtended;

                HandPoint hp = new HandPoint(frame.Hands[i], DetectPinch(indexTip.position, thumbTip.position), isDrawing);
                SaveData.writeHandPoint(hp);

            }
         
        }


        prevIsDrawing = isDrawing;
        Vector3 t = DEFAULT_VEC3;
        prevStatus = status;


        switch (mode) 
        {
            case (Condition.Pinch):

        // if (pinchMode) {
                t = GetMidPoint(indexTip.position, thumbTip.position);
                // if (!extendedBool) {
                //     t = GetMidPoint(indexB.position, thumbB.position);
                // }
                // cursor.transform.position = t;
                if (Input.GetKeyDown(KeyCode.Space)) {
                    if (isDrawing) {
                        SaveData.SetTimeDrawEnd();
                        ClearCanvas();
                        status = Status.BlankAfterDraw;
                        timeLeft = 1.5f;
                    } else {
                        SaveData.SetTimeDrawStart();
                    }
                    isDrawing = !isDrawing;                
                }
                break;

                // Frame frame = controller.Frame(); // controller is a Controller object
                // if(frame.Hands.Count > 0){

                //     if (DetectPinch(indexTip.position, thumb.position)) {
                //         isDrawing = true;
                //     } else {
                //         isDrawing = false;
                //     }
                // } else {
                //     isDrawing = false;
                // }

                // if ((!isDrawing) && (prevIsDrawing)) {
                //     ClearCanvas();
                //     status = Status.BlankAfterDraw;
                //     timeLeft = 1.5f;
                // } else {
                //     prevStatus = status;
                // }
                // break;

        // } else {
                // Debug.Log("do we get in here somehow");
            case (Condition.Index):
                t = indexTip.position;
                // if (!extendedBool) {
                //     t = indexB.position;
                // }
                // cursor.transform.position = t;
                if (Input.GetKeyDown(KeyCode.Space)) {
                    if (isDrawing) {
                        SaveData.SetTimeDrawEnd();
                        ClearCanvas();
                        status = Status.BlankAfterDraw;
                        timeLeft = 1.5f;
                    } else {
                        
                        SaveData.SetTimeDrawStart();
                        // prevStatus = status;
                    }
                    isDrawing = !isDrawing;                
                }
                break;

            case (Condition.Touch):
                Debug.Log("TC " + Input.touchCount);
                if (Input.touchCount > 0) {
                    isDrawing = true;
                    // t = cam.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, -5.16f));
                    t = cam.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, cam.nearClipPlane));
                    Debug.Log(t);
                    // cursor.transform.position = indexTip.position;
                } else {
                    isDrawing = false;
                }
                if ((!isDrawing) && (prevIsDrawing)) {
                    Debug.Log("Are we clearing the canvas");
                    SaveData.SetTimeDrawEnd();
                    ClearCanvas();
                    status = Status.BlankAfterDraw;
                    timeLeft = 1.5f;
                
                } else {
                        
                    SaveData.SetTimeDrawStart();
                    // prevStatus = status;
                }
                break;
                

        }

        return t;
    }

    public GameObject CreateSphere(string name, Vector3 position, Condition cond) {
        GameObject c = Instantiate(paint) as GameObject;
        c.name = name;

        TrailRenderer trail = c.GetComponent<TrailRenderer>();
        trail.material = red;
        Debug.Log("position " + position.x + ", " + position.y + ", " + position.z);
       
        if (cond == Condition.Touch) {
             position.z = -16.95f;
            c.transform.position = position;
            c.transform.localScale = new Vector3(0.12f, 0.12f, c.transform.localScale.z);
            // c.transform.localScale = new Vector3(0.001f, 0.001f, c.transform.localScale.z);

        } else {
            c.transform.position = position;
            c.transform.localScale = new Vector3(4.8f, 4.8f, c.transform.localScale.z);
        }
        return c;
    }


    public static void ClearCanvas() {
        GameObject[] gosArr = (GameObject[]) Resources.FindObjectsOfTypeAll(typeof(GameObject));
   
        List<GameObject> gos = new List<GameObject>(gosArr);
        for (int i = 0; i < gos.Count; i++) {
            if (gos[i].name.Length > 9) {
                if (gos[i].name.Substring(0,9) == cursorTag) {
                    gos[i].SetActive(false);


                }
            }
        
        }
        SaveData.AddCurrentRecord();
        // StringBuilder handData = new StringBuilder("{");
        // for (int i = 0; i < hps.Count; i++) {
        //     handData = handData.Append(JsonUtility.ToJson(hps[i]) + ",");
        // }
        // handData[handData.Length-1] = '}';

        // StringBuilder pathData = new StringBuilder("{");
        // for (int i = 0; i < path.Count; i++) {
        //     pathData = pathData.Append(JsonUtility.ToJson(path[i]) + ",");
        // }

        // pathData[pathData.Length-1] = '}';

        // Debug.Log(handData.ToString());
        // hps.Clear();
        // path.Clear();

    }

    // public void ChangeCursor() {
    //     if (mode == Condition.Pinch) {
    //         // cursor.SetActive(true);
    //         if (DetectPinch(indexTip.position, thumbTip.position)) {
    //             cursor.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    //         } else {
    //             cursor.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    //         }
    //     } else if (mode == Condition.Index) {
    //         // cursor.SetActive(true);
    //         if (isDrawing) {
    //             cursor.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    //         } else {
    //             cursor.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    //         }
    //     } else {
    //         cursor.SetActive(false);
    //     }
       
    // }





}
