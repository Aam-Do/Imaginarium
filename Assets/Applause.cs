using System.Collections;
using System.Timers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEditor.VFX;
using UnityEngine.Experimental.VFX;

public class Applause : MonoBehaviour
{
    public int playerIndex = 0;
    public KinectInterop.JointType joint = KinectInterop.JointType.HandRight;
    public KinectInterop.JointType joint2 = KinectInterop.JointType.HandLeft;
    public Vector3 jointPosition;
    public Vector3 jointPosition2;
    public GameObject particlespawn;

    public Vector3 velocityspirit;
    //calibration needed!
    public int schwelle;
    public float xwerthandaus;

    public float scaleFactor = 60f;
    public float scaleFactor2 = 80f;
    public float duration = 1f;
    public float originalScalex;
    private float originalScaley;
    private float originalScalez;
    public static bool isTimerElapsed = false;
    public float distancex;
    public float distancey;
    public float distancez;
    public bool test;
    private Vector3 previousPosition;
    private Vector3 currentVelocity;
    public VisualEffect visualEffect;



    // Start is called before the first frame update
    void Start()
    {
        originalScalex = particlespawn.transform.localScale.x;
        originalScaley = particlespawn.transform.localScale.y;
        originalScalez = particlespawn.transform.localScale.z;

    }

    // Update is called once per frame
    void Update()
    {
        KinectManager manager = KinectManager.Instance;
        particlespawn.transform.localScale = new Vector3(originalScalex, originalScaley, originalScalez);



        if (manager && manager.IsInitialized())
        {
            if (manager.IsUserDetected(playerIndex))
            {
                long userId = manager.GetUserIdByIndex(playerIndex);

                if (manager.IsJointTracked(userId, (int)joint) && manager.IsJointTracked(userId, (int)joint2))
                {
                    // output the joint position for easy tracking
                    Vector3 jointPos = manager.GetJointPosition(userId, (int)joint);
                    jointPosition = jointPos;
                    Vector3 jointPos2 = manager.GetJointPosition(userId, (int)joint2);
                    jointPosition2 = jointPos2;

                    //velocity
                    Vector3 currentPosition = transform.position;
                    velocityspirit = (currentPosition - previousPosition) / Time.deltaTime;
                    // Update the previous position to the current position for the next frame
                    previousPosition = currentPosition;


                    distancex = jointPosition2.x - jointPosition.x;
                    if (distancex < 0)
                    {
                        distancex *= (-1);
                    }
                    // distancey = jointPosition2.y - jointPosition.y;
                    // if (distancey < 0)
                    // {
                    //     distancey *= (-1);
                    // }
                    // distancez = jointPosition2.z - jointPosition.z;
                    // if (distancez < 0)
                    // {
                    //     distancez *= (-1);
                    // }

                    if (distancex >= xwerthandaus)
                    {
                        test = true;
                        particlespawn.transform.localScale = new Vector3(originalScalex * scaleFactor2, originalScaley * scaleFactor2, originalScalez);

                        visualEffect.SetVector3("outputSize", new Vector3(300, 0.6f, 0.6f));
                    }
                    else if (velocityspirit.y >= schwelle || velocityspirit.y <= -schwelle)
                    {
                        particlespawn.transform.localScale = new Vector3(originalScalex * scaleFactor, originalScaley * scaleFactor, originalScalez);

                        visualEffect.SetVector3("outputSize", new Vector3(0.6f, 0.6f, 0.6f));
                    }
                    else
                    {
                        particlespawn.transform.localScale = new Vector3(originalScalex, originalScaley, originalScalez);
                        visualEffect.SetVector3("outputSize", new Vector3(0.6f, 0.6f, 0.6f));
                    }
                }
            }
        }
    }
    static void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        isTimerElapsed = true;
    }
}
