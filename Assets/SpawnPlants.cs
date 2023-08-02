using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlants : MonoBehaviour
{
    public GameObject[] prefabs;
    public bool spawn = false;
    public float spawnDelay = 6f;
    public float maxSpawnRate = 0.1f;
    public float spawnDistanceThreshold = 4f;
    public float animationLength = 5f;
    public float minSpeed = 0.3f;
    public float maxSpeed = 1.8f;
    public int spawnY = -1;
    public float maxSpawnRadius = 2f;
    public float minSpawnRadius = 0.3f;
    public float minScale = 0.2f;
    public float maxScale = 1.2f;

    private float lastSpawnTime;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Update()
    {
        if (prefabs != null && prefabs.Length > 0 && spawn == true)
        {
            if (Time.time > lastSpawnTime + GetSpawnRate())
            {
                // Check if the object's position is within the desired range
                if (transform.position.y <= spawnDistanceThreshold && transform.position.y >= spawnY)
                {
                    int randomIndex = Random.Range(0, prefabs.Length);
                    GameObject prefab = prefabs[randomIndex];
                    // Calculate the spawn position within a varying radius based on the object's y-coordinate
                    float spawnRadius = Mathf.Lerp(maxSpawnRadius, minSpawnRadius, Mathf.Abs(transform.position.y - 0.5f));
                    Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
                    spawnPosition.y = spawnY;
                    spawnPosition.z = 2.384186e-07f;
                    // scaling
                    float randomScale = Random.Range(minScale, maxScale);
                    Vector3 scale = new Vector3(
                    randomScale, randomScale, randomScale
                    );
                    GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
                    newObject.transform.GetChild(1).gameObject.transform.localScale = scale;


                    // Add a random rotation around the y-axis
                    float randomYRotation = Random.Range(0f, 360f);
                    newObject.transform.rotation = Quaternion.Euler(0f, randomYRotation, 0f);


                    // Get the Animator component from the new object
                    Animator animator = newObject.transform.GetChild(0).gameObject.GetComponent<Animator>();

                    // Set a random animation speed
                    float randomSpeed = Random.Range(minSpeed, maxSpeed);
                    animator.speed = randomSpeed;

                    // Calculate the despawn delay based on animation speed and length
                    float despawnDelay = animationLength / randomSpeed;

                    spawnedObjects.Add(newObject);

                    StartCoroutine(DestroyObjectAfterDelay(newObject, despawnDelay));
                }
                lastSpawnTime = Time.time;
            }
        }
    }

    IEnumerator DestroyObjectAfterDelay(GameObject obj, float delay)
    {
        Animator animator = obj.transform.GetChild(0).gameObject.GetComponent<Animator>();
        yield return new WaitForSeconds(delay);
        // Ensure the animation is finished by checking the normalized time
        float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        while (normalizedTime < 1f)
        {
            yield return null;
            normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        yield return new WaitForSeconds(1f);
        Destroy(obj);
        spawnedObjects.Remove(obj);
    }

    private float GetSpawnRate()
    {
        // Calculate the spawn rate based on the distance from y = 0
        float spawnRate = Mathf.Lerp(spawnDelay, maxSpawnRate, transform.position.y / spawnDistanceThreshold);
        return spawnRate;
    }
}