using UnityEngine;

public class ChunkCenterTrigger : MonoBehaviour
{
    [HideInInspector] public TreadmillLoopManager manager;
    [HideInInspector] public int chunkIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && manager != null)
        {
            manager.OnPlayerEnteredChunk(chunkIndex);
        }
    }
}