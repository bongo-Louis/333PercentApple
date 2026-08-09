using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    [Header("Prefab & Initial Position")]
    public GameObject sectionPrefab;
    public Transform initialAttachPoint; // Where the loop starts (e.g., bottom of starting escalator)

    [Header("Pool Setup")]
    public int poolSize = 5;
    
    [HideInInspector] public bool isLoopActive = false;
    [Header("Safety")]
    public float moveCooldown = 0.15f;

    private List<GameObject> pool = new List<GameObject>();
    private List<GameObject> activeChain = new List<GameObject>(); // Exactly 3 active chunks [0: Behind, 1: Current, 2: Ahead]
    private float lastMoveTime = -999f;

    private void Start()
    {
        // Pre-instantiate pool offscreen
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(sectionPrefab, Vector3.one * -9999f, Quaternion.identity);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public void StartLoopSystem()
    {
        isLoopActive = true;
        activeChain.Clear();
        
        // Spawn initial 3 chunks as [Behind, Current, Ahead]
        GameObject current = GetPooledObject();
        if (current == null || initialAttachPoint == null) return;
        current.transform.position = initialAttachPoint.position;
        current.transform.rotation = initialAttachPoint.rotation;
        current.SetActive(true);

        GameObject ahead = GetPooledObject();
        if (ahead == null) return;
        SnapSectionTo(ahead, current.transform.Find("ExitPoint"), true);
        ahead.SetActive(true);

        GameObject behind = GetPooledObject();
        if (behind == null) return;
        SnapSectionTo(behind, current.transform.Find("EntrancePoint"), false);
        behind.SetActive(true);

        activeChain.Add(behind);  // Index 0
        activeChain.Add(current); // Index 1
        activeChain.Add(ahead);   // Index 2
    }

    public void MoveForward()
    {
        if (!isLoopActive || activeChain.Count < 3) return;
        if (Time.time - lastMoveTime < moveCooldown) return;
        lastMoveTime = Time.time;

        // Recycle oldest chunk behind player (index 0) and snap it ahead of index 2
        GameObject recycleChunk = activeChain[0];
        activeChain.RemoveAt(0);

        Transform nextExit = activeChain[activeChain.Count - 1].transform.Find("ExitPoint"); // Current ahead chunk's exit
        SnapSectionTo(recycleChunk, nextExit, true);

        activeChain.Add(recycleChunk); // Becomes new ahead chunk (index 2)
    }

    public void MoveBackward()
    {
        if (!isLoopActive || activeChain.Count < 3) return;
        if (Time.time - lastMoveTime < moveCooldown) return;
        lastMoveTime = Time.time;

        // Recycle chunk ahead of player (index 2) and snap it behind index 0
        GameObject recycleChunk = activeChain[2];
        activeChain.RemoveAt(2);

        Transform prevEntrance = activeChain[0].transform.Find("EntrancePoint"); // Current behind chunk's entrance
        SnapSectionTo(recycleChunk, prevEntrance, false);

        activeChain.Insert(0, recycleChunk); // Becomes new behind chunk (index 0)
    }

    private void SnapSectionTo(GameObject target, Transform connectionPoint, bool connectAtEntrance)
    {
        if (target == null || connectionPoint == null) return;

        Transform anchor = connectAtEntrance ? target.transform.Find("EntrancePoint") : target.transform.Find("ExitPoint");
        if (anchor == null) return;
        
        // Align rotation first, then move by world-space anchor offset to preserve correct spacing.
        target.transform.rotation = connectionPoint.rotation * Quaternion.Inverse(anchor.localRotation);

        Vector3 offset = connectionPoint.position - anchor.position;
        target.transform.position += offset;
    }

    private GameObject GetPooledObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy) return obj;
        }
        return null;
    }
}