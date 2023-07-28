using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlants : MonoBehaviour
{
    public GameObject[] prefabs;
    public float spawnDelay = 6f;
    public float maxSpawnRate = 0.1f;
    public float spawnDistanceThreshold = 4f;
    public float despawnDelay = 4f;
    public int spawnY = 0;
    public float maxSpawnRadius = 2f;
    public float minSpawnRadius = 0.3f;
    public float minScale = 0.2f;
    public float maxScale = 1.2f;

    private float lastSpawnTime;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Update()
    {
        if (prefabs != null && prefabs.Length > 0)
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
                    spawnPosition.y = 0f;
                    spawnPosition.z = 0f;
                    // scaling
                    float randomScale = Random.Range(minScale, maxScale);
                    Vector3 scale = new Vector3(
                    randomScale, randomScale, randomScale
                    );
                    GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
                    newObject.transform.GetChild(1).gameObject.transform.localScale = scale;
                    spawnedObjects.Add(newObject);

                    StartCoroutine(DestroyObjectAfterDelay(newObject, despawnDelay));
                }
                lastSpawnTime = Time.time;
            }
        }
    }

    IEnumerator DestroyObjectAfterDelay(GameObject obj, float delay)
    {
        Debug.Log("enter destroy function");
        Animator animator = obj.transform.GetChild(0).gameObject.GetComponent<Animator>();
        Debug.Log(animator);
        // Wait until the object is not in transition (animation has finished)
        yield return new WaitUntil(() => !animator.IsInTransition(0));
        yield return new WaitForSeconds(delay);
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