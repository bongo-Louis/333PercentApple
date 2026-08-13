using System.Collections.Generic;
using UnityEngine;

public class AnomalySpawner : MonoBehaviour
{
    [Header("Scripted Narrative Anomalies")]
    public GameObject exit3Anomaly;
    public GameObject exit4Anomaly;
    public GameObject exit5Anomaly;
    public GameObject exit7Anomaly;
    public GameObject exit9Anomaly;

    [Header("Procedural / Random Anomalies")]
    public List<GameObject> randomAnomalies = new List<GameObject>();

    [Header("Random Anomaly Spawn Chance")]
    [Range(0f, 1f)]
    public float randomSpawnChance = 0.5f; // 50% chance of spawning a random anomaly

    // Call this before spawning a new one
    public void ClearAllAnomalies()
    {
        if (exit3Anomaly != null) exit3Anomaly.SetActive(false);
        if (exit4Anomaly != null) exit4Anomaly.SetActive(false);
        if (exit5Anomaly != null) exit5Anomaly.SetActive(false);
        if (exit7Anomaly != null) exit7Anomaly.SetActive(false);
        if (exit9Anomaly != null) exit9Anomaly.SetActive(false);

        foreach (GameObject anomaly in randomAnomalies)
        {
            if (anomaly != null) anomaly.SetActive(false);
        }
    }

    public bool SpawnScriptedAnomaly(int exitStage, out bool requiresDispel)
    {
        ClearAllAnomalies();
        requiresDispel = false; // Default unless specified

       switch (exitStage)
        {
            case 3:
                if (exit3Anomaly) { exit3Anomaly.SetActive(true); return true; }
                break;
            case 4:
                if (exit4Anomaly) { exit4Anomaly.SetActive(true); return true; }
                break;
            case 5:
                if (exit5Anomaly) { exit5Anomaly.SetActive(true); requiresDispel = true; return true; }
                break;
            case 7:
                if (exit7Anomaly) { exit7Anomaly.SetActive(true); return true; }
                break;
            case 9:
                if (exit9Anomaly) { exit9Anomaly.SetActive(true); return true; }
                break;
        }

        return false; // if object is missing
    }

    public bool SpawnNormalAnomaly()
    {
        ClearAllAnomalies();
        Debug.Log("Rolling for a random anomaly spawn...");
        bool shouldSpawn = Random.value < randomSpawnChance;
        Debug.Log($"should it spawn?: {shouldSpawn}");
        if (shouldSpawn && randomAnomalies.Count > 0)
        {
            int randomIndex = Random.Range(0, randomAnomalies.Count);
            if (randomAnomalies[randomIndex] != null)
            {
                randomAnomalies[randomIndex].SetActive(true);
                Debug.Log("Random anomaly spawned!");
                return true; // Successfully spawned!
            }
        }

        return false; // Normal hallway
    }
}