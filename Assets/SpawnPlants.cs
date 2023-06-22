using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlants : MonoBehaviour
{
    public GameObject[] prefabs;
    public float spawnDelay = 0.3f;
    public float despawnDelay = 4f;
    public int maxSpawnCount = 10;
    public int spawnY = 0;
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
                GameObject prefab = prefabs[randomIndex];
                GameObject newObject = Instantiate(prefab, new Vector3(transform.position.x, 0f, transform.position.z), Quaternion.identity);
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