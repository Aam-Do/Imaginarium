using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAurora : MonoBehaviour
{
    public int playerIndex = 0;
    public KinectInterop.JointType joint = KinectInterop.JointType.HandRight;
    public KinectInterop.JointType joint2 = KinectInterop.JointType.HandLeft;
    private KinectManager manager = KinectManager.Instance;

    public GameObject spirit;
    public GameObject aurora;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (manager && manager.IsInitialized())
        {
            if (manager.IsUserDetected(playerIndex))
            {
                long userId = manager.GetUserIdByIndex(playerIndex);

                if (manager.IsJointTracked(userId, (int)joint) && manager.IsJointTracked(userId, (int)joint2))
                {
                    spirit.SetActive(true);
                    aurora.SetActive(false);
                }
                else
                {
                    aurora.SetActive(true);
                    spirit.SetActive(false);
                }
            }
            else
            {
                aurora.SetActive(true);
                spirit.SetActive(false);
            }
        }
    }
}
