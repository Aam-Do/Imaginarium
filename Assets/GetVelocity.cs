using UnityEngine;
using UnityEngine.VFX;

public class GetVelocity : MonoBehaviour
{
    private Vector3 previousPosition;
    private Vector3 currentVelocity;
    public VisualEffect visualEffect;

    void Start()
    {
        // Initialize the previous position to the starting position of the game object
        previousPosition = transform.position;
    }

    void Update()
    {
        // Calculate the current velocity based on the change in position
        Vector3 currentPosition = transform.position;
        currentVelocity = (currentPosition - previousPosition) / Time.deltaTime;

        // Update the previous position to the current position for the next frame
        previousPosition = currentPosition;
        // Debug.Log(currentVelocity);

        if (visualEffect.enabled == true)
        {
            // visualEffect.SetVector3("Velocity", currentVelocity);
        }

    }

    // You can access the current velocity from other scripts or components
    public Vector3 GetCurrentVelocity()
    {
        return currentVelocity;
    }
}