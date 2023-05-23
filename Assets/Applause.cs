using System.Collections;
using System.Timers;
using System.Collections.Generic;
using UnityEngine;

public class Applause : MonoBehaviour
{
    public int playerIndex = 0;
    public KinectInterop.JointType joint = KinectInterop.JointType.HandRight;
    public KinectInterop.JointType joint2 = KinectInterop.JointType.HandLeft;
    public Vector3 jointPosition;
    public Vector3 jointPosition2;
    public GameObject particlespawn;

    public float scaleFactor = 60f;
    public float duration = 1f;
    public float originalScalex;
    private float originalScaley;
    private float originalScalez;
    public static bool isTimerElapsed = false;
    public float distancex;
    public float distancey;
    public float distancez;

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

                    distancex = jointPosition2.x - jointPosition.x;
                    if (distancex < 0)
                    {
                        distancex *= (-1);
                    }
                    distancey = jointPosition2.y - jointPosition.y;
                    if (distancey < 0)
                    {
                        distancey *= (-1);
                    }
                    distancez = jointPosition2.z - jointPosition.z;
                    if (distancez < 0)
                    {
                        distancez *= (-1);
                    }

                    if (distancex <= 0.1 && distancey <= 0.1 && distancez <= 0.1)
                    {
                        particlespawn.transform.localScale = new Vector3(originalScalex * scaleFactor, originalScaley * scaleFactor, originalScalez);
                    }
                    else
                    {
                        particlespawn.transform.localScale = new Vector3(originalScalex, originalScaley, originalScalez);
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
