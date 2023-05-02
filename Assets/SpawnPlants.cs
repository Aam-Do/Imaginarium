using UnityEngine;

public class SpawnCircle : MonoBehaviour
{
    public GameObject prefab; // The GameObject prefab to spawn
    public float radius = 2f; // The radius of the circle to spawn in
    public int numObjects = 1; // The number of objects to spawn
    public float spacing = 0.2f; // The distance between each object
    public float yOffset = 0f; // The distance from y=0 that the objects will spawn
    public float yMultiplier = 1f; // The multiplier to use for the number of objects to spawn, based on the original object's y position

    private void Start()
    {
        // Calculate the number of objects to spawn based on the original object's y position
        float y = transform.position.y;
        int numToSpawn = Mathf.RoundToInt(numObjects * (1f - Mathf.Abs(y) / radius) * yMultiplier);
        Debug.Log(numToSpawn);

        // Spawn the objects in a circle around the original object
        for (int i = 0; i < numToSpawn; i++)
        {
            float angle = i * Mathf.PI * 2f / numToSpawn;
            Vector3 spawnPos = new Vector3(transform.position.x + Mathf.Cos(angle) * radius, yOffset, transform.position.z + Mathf.Sin(angle) * radius);
            Instantiate(prefab, spawnPos, Quaternion.identity, transform);
        }
    }
}