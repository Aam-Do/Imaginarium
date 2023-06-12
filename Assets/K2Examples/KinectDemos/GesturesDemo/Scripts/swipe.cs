using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

public class swipe : MonoBehaviour
{
    [Tooltip("Camera used for screen-to-world calculations. This is usually the main camera.")]
    public Camera screenCamera;

    [Tooltip("Whether the presentation slides may be changed with gestures (SwipeLeft, SwipeRight & SwipeUp).")]
    public bool slideChangeWithGestures = true;
    [Tooltip("Whether the presentation slides may be changed with keys (PgDown & PgUp).")]
    public int spinSpeed = 5;

    [Tooltip("List of the presentation slides.")]
    public List<GameObject> cubeSides;
    public GameObject particlestuff;
    public System.Timers.Timer timer = new System.Timers.Timer(200);


    //private int maxSides = 0;

    private int side = 0;
    private int tex = 0;
    private bool isSpinning = false;

    private int[] hsides = { 0, 1, 2, 3 };  // left, front, right, back
    private int[] vsides = { 4, 1, 5, 3 };  // up, front, down, back

    private CubeGestureListener gestureListener;
    public float initialRotation = 0;
    private int stepsToGo = 0;

    private float rotationStep;
    private Vector3 rotationAxis;

    public bool activeswipe = false;
    public Vector3 posPart;




    void Start()
    {
        // hide mouse cursor
        //Cursor.visible = false;

        // by default set the main-camera to be screen-camera
        if (screenCamera == null)
        {
            screenCamera = Camera.main;
        }



        initialRotation = 0;
        activeswipe = false;
        side = hsides[1];
        // get the gestures listener
        gestureListener = CubeGestureListener.Instance;
    }

    void Update()
    {
        posPart = new Vector3(particlestuff.transform.position.x, particlestuff.transform.position.y, particlestuff.transform.position.z);
        // dont run Update() if there is no gesture listener
        if (!gestureListener)
            return;

        if (!isSpinning)
        {
            if (slideChangeWithGestures && gestureListener)
            {
                if (gestureListener.IsSwipeUp())
                {
                    transform.eulerAngles = new Vector3(
                    particlestuff.transform.eulerAngles.x,
                    particlestuff.transform.eulerAngles.y + 180,
                    particlestuff.transform.eulerAngles.z
                    );
                    activeswipe = true;
                    particlestuff.transform.rotation.Set(0, 180, 0, 10);

                    timer.Elapsed += (timerSender, timerEvent) => send();
                    timer.AutoReset = true;
                    timer.Enabled = true;
                }
                else
                {
                    activeswipe = false;

                }
            }
        }
        else
        {


        }
        void send()
        {
            particlestuff.transform.rotation.Set(0, 0, 0, 0);
        }
    }

    // rotates cube left
}