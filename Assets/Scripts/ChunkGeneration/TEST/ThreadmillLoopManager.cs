using UnityEngine;

public class TreadmillLoopManager : MonoBehaviour
{
    [Header("Setup")]
    public GameObject sectionPrefab;
    public Transform escalatorExitPoint; // End of your starting escalator area

    private GameObject[] chunks = new GameObject[5]; // Expanded to 5 chunks!
    private bool hasReachedTriggerDistanceOnce = false;

    private void Start()
    {
        // 1. Spawn 5 chunks
        for (int i = 0; i < 5; i++)
        {
            chunks[i] = Instantiate(sectionPrefab);
            chunks[i].name = "Chunk_" + i;
            
            ChunkCenterTrigger trigger = chunks[i].GetComponentInChildren<ChunkCenterTrigger>();
            if (trigger != null)
            {
                trigger.manager = this;
                trigger.chunkIndex = i;
            }
        }

        // 2. Line up all 5 initial chunks sequentially down the slope:
        // Escalator -> Chunk 0 -> Chunk 1 -> Chunk 2 -> Chunk 3 -> Chunk 4
        SnapTo(chunks[0], escalatorExitPoint, true);
        for (int i = 1; i < 5; i++)
        {
            Transform prevExit = chunks[i - 1].transform.Find("ExitPoint");
            SnapTo(chunks[i], prevExit, true);
        }
    }

    public void OnPlayerEnteredChunk(int currentIndex)
    {
        // Wait until player reaches Chunk 2 (index 2) so they don't despawn the starting escalator too early
        if (!hasReachedTriggerDistanceOnce)
        {
            if (currentIndex >= 2)
            {
                hasReachedTriggerDistanceOnce = true; // Unlock infinite loop!
            }
            else
            {
                return; // Ignore Chunks 0 and 1 on the very first descent
            }
        }

        // Calculate 2 chunks ahead using Modulo (% 5)
        int ahead1 = (currentIndex + 1) % 5;
        int ahead2 = (currentIndex + 2) % 5;

        // Calculate 2 chunks behind using Modulo (% 5)
        int behind1 = (currentIndex + 4) % 5; // Equivalent to (currentIndex - 1)
        int behind2 = (currentIndex + 3) % 5; // Equivalent to (currentIndex - 2)

        // 1. Position 1st chunk ahead
        Transform currentExit = chunks[currentIndex].transform.Find("ExitPoint");
        SnapTo(chunks[ahead1], currentExit, true);

        // 2. Position 2nd chunk ahead (snaps to ahead1's ExitPoint)
        Transform ahead1Exit = chunks[ahead1].transform.Find("ExitPoint");
        SnapTo(chunks[ahead2], ahead1Exit, true);

        // 3. Position 1st chunk behind
        Transform currentEntrance = chunks[currentIndex].transform.Find("EntrancePoint");
        SnapTo(chunks[behind1], currentEntrance, false);

        // 4. Position 2nd chunk behind (snaps to behind1's EntrancePoint)
        Transform behind1Entrance = chunks[behind1].transform.Find("EntrancePoint");
        SnapTo(chunks[behind2], behind1Entrance, false);
    }

    private void SnapTo(GameObject chunk, Transform targetPoint, bool connectAtEntrance)
    {
        Transform anchor = connectAtEntrance 
            ? chunk.transform.Find("EntrancePoint") 
            : chunk.transform.Find("ExitPoint");

        Vector3 offset = targetPoint.position - anchor.position;
        chunk.transform.position += offset;
        chunk.transform.rotation = targetPoint.rotation;
    }
}