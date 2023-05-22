using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocity : MonoBehaviour
{
    public Vector3 PrevPos;
    public Vector3 NewPos;
    public Vector3 ObjVelocity;

    public float magnitude;
    public float targetVelocity = 10f; // The desired velocity threshold
    public string messageToSend = "Object reached target velocity!"; // The message to send

    private void Start()
    {
        PrevPos = transform.position;
        NewPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {
        NewPos = transform.position;  // each frame track the new position
        ObjVelocity = (NewPos - PrevPos) / Time.fixedDeltaTime;  // velocity = dist/time
        magnitude = ObjVelocity.magnitude;
        PrevPos = NewPos;  // update position for next frame calculation
    }
}
