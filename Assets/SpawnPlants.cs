using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlants : MonoBehaviour
{
    public GameObject prefab;
    public float spawnDelay = 0.5f;
    public float despawnDelay = 4f;
    public int maxSpawnCount = 4;
    public int spawnY = 0;
    public float maxY = 2;

    private float lastSpawnTime;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Update()
    {
        if (Time.time > lastSpawnTime + spawnDelay && spawnedObjects.Count < maxSpawnCount && transform.position.y >= spawnY && transform.position.y <= maxY)
        {
            GameObject newObject = Instantiate(prefab, new Vector3(transform.position.x, 0f, transform.position.z), Quaternion.identity);
            spawnedObjects.Add(newObject);
            lastSpawnTime = Time.time;

            Animator animator = newObject.transform.GetChild(0).GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("growingLoop");
                StartCoroutine(DestroyObjectAfterDelay(newObject, animator.GetCurrentAnimatorStateInfo(0).length));
            }
            else
            {
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