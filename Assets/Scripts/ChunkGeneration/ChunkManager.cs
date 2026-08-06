using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header("Prefabs & Triggers")]
    public GameObject[] chunkPrefabs; // Assign normal and anomaly variations
    public Transform playerTransform;

    [Header("Settings")]
    public int maxLoadedChunks = 3;
    
    private Queue<GameObject> activeChunks = new Queue<GameObject>();
    private Transform lastExitPoint;

    void Start()
    {
        // Initialize by spawning the starting buffer chunks
        for (int i = 0; i < maxLoadedChunks; i++)
        {
            SpawnChunk();
        }
    }

    public void SpawnChunk()
    {
        // 1. Pick a random prefab (or logic-based anomaly chunk)
        GameObject prefabToSpawn = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        GameObject newChunkObject;

        if (activeChunks.Count == 0)
        {
            // First chunk spawns at origin/initial position
            newChunkObject = Instantiate(prefabToSpawn, Vector3.zero, Quaternion.identity);
        }
        else
        {
            // 2. Instantiate temporary offscreen chunk
            newChunkObject = Instantiate(prefabToSpawn);
            Chunk chunkComponent = newChunkObject.GetComponent<Chunk>();

            // 3. Align entryPoint of new chunk to the lastExitPoint
            Quaternion rotationOffset = lastExitPoint.rotation * Quaternion.Inverse(chunkComponent.entryPoint.localRotation);
            newChunkObject.transform.rotation = rotationOffset;

            Vector3 positionOffset = lastExitPoint.position - chunkComponent.entryPoint.position;
            newChunkObject.transform.position += positionOffset;
        }

        // 4. Update track variables
        Chunk currentChunk = newChunkObject.GetComponent<Chunk>();
        lastExitPoint = currentChunk.exitPoint;
        activeChunks.Enqueue(newChunkObject);

        // 5. Cleanup old chunks beyond maxLoadedChunks limit
        if (activeChunks.Count > maxLoadedChunks)
        {
            GameObject oldChunk = activeChunks.Dequeue();
            Destroy(oldChunk);
        }
    }
}