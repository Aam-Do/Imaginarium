using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlants : MonoBehaviour
{
    public GameObject[] prefabs;
    public float spawnDelay = 0.2f;
    public float despawnDelay = 4f;
    public int maxSpawnCount = 15;
    public int spawnY = 0;
    public float spawnRadius = 1f;
    public float maxY = 2;

    private float lastSpawnTime;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Update()
    {
        if (prefabs != null && prefabs.Length > 0)
        {
            if (Time.time > lastSpawnTime + spawnDelay && spawnedObjects.Count < maxSpawnCount && transform.position.y >= spawnY && transform.position.y <= maxY)
            {
                int randomIndex = Random.Range(0, prefabs.Length);
                Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
                GameObject prefab = prefabs[randomIndex];
                GameObject newObject = Instantiate(prefab, new Vector3(transform.position.x + randomOffset.x, 0f, transform.position.z + randomOffset.y), Quaternion.identity);
                spawnedObjects.Add(newObject);
                lastSpawnTime = Time.time;

                StartCoroutine(DestroyObjectAfterDelay(newObject, despawnDelay));
            }
        }
    }

    IEnumerator DestroyObjectAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
        spawnedObjects.Remove(obj);
    }
}