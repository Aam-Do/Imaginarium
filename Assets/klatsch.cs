using System.Collections;
using System.Timers;
using System.Collections.Generic;
using UnityEngine;

public class klatsch : MonoBehaviour
{
    public Vector3 previousPositionR; // The previous position of the object
    public Vector3 previousPositionL;
    public float targetVelocity = 10f; // The desired velocity threshold
    public GameObject handR;
    public GameObject handL;

    public bool klatschen = false;
    public float velocityMagnitudeR;
    public float velocityMagnitudeL;

    private void Start()
    {
        // Store the initial position of the object
        previousPositionR = handR.transform.position;
        previousPositionL = handL.transform.position;
    }

    private void Update()
    {
        // Calculate the displacement since the last frame
        Vector3 displacementR = handR.transform.position - previousPositionR;
        Vector3 displacementL = handL.transform.position - previousPositionL;

        // Calculate the velocity magnitude
        velocityMagnitudeR = displacementR.magnitude / Time.deltaTime;
        velocityMagnitudeL = displacementL.magnitude / Time.deltaTime;

        // Check if the current velocity magnitude is equal to or greater than the target velocity
        if (velocityMagnitudeR >= targetVelocity && velocityMagnitudeL >= targetVelocity)
        {
            klatschen = true;
            previousPositionR = handR.transform.position;
            previousPositionL = handL.transform.position;
        }
        else
        {
            klatschen = false;
        }

        // Update the previous position for the next frame

    }
}
