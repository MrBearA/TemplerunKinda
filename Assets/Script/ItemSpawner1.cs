using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Item itemPrefab;  // Prefab to spawn
    public float speed = 5f; // Speed at which items move towards the player
    public Transform leftSpawnPoint;  // Left lane spawn point
    public Transform middleSpawnPoint;  // Middle lane spawn point
    public Transform rightSpawnPoint;  // Right lane spawn point
    public List<Item> spawnedItems = new List<Item>();  // Track spawned items

    private void Start()
    {
        // Spawn items repeatedly at fixed intervals
        InvokeRepeating(nameof(SpawnItems), 2.0f, 4.0f);  // Adjust timing as needed
    }

    private void SpawnItems()
    {
        if (itemPrefab == null)
        {
            Debug.LogError("ItemPrefab is not assigned!");
            return;
        }

        if (leftSpawnPoint == null || middleSpawnPoint == null || rightSpawnPoint == null)
        {
            Debug.LogError("One or more spawn points are not assigned!");
            return;
        }

        // Spawn an item at each lane
        SpawnItemAt(leftSpawnPoint);
        SpawnItemAt(middleSpawnPoint);
        SpawnItemAt(rightSpawnPoint);
    }

    private void SpawnItemAt(Transform spawnPoint)
    {
        Debug.Log("Spawning at: " + spawnPoint.position);  // Log the spawn position
        var spawnedItem = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
        spawnedItem.transform.position = spawnPoint.position; // Explicitly set position
        spawnedItems.Add(spawnedItem);
    }



    private void Update()
    {
        // Move each item towards the player
        for (int i = 0; i < spawnedItems.Count; i++)
        {
            if (spawnedItems[i] != null)
            {
                spawnedItems[i].transform.position += Vector3.back * speed * Time.deltaTime;  // Move towards the player
            }
        }

        // Remove items that have passed the player
        spawnedItems.RemoveAll(item => item != null && item.transform.position.z < -10);
    }
}
