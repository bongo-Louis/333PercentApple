using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            FindObjectOfType<ChunkManager>().SpawnChunk();
        }
    }
}