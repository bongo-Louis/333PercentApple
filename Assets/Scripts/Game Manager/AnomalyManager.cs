using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public static AnomalyManager Instance;
    [Header("Game State")]
    public int currentExitCount = 0; // number of exits the player has passed through
    public int targetWinCount = 10; // number of exits required to trigger the win condition

    [Header("Current Hallway State")]
    public bool isAnomalyPresent = false; // is the anomaly currently present in the hallway?
    public bool requiresDispel = false; // does the anomaly need to be dispelled to count as a valid exit?
    public bool isDispelled = false; // has the anomaly been dispelled by the player?
    public AnomalySpawner spawner;
    public ExitSignController exitSignController;

    [SerializeField] private WinSpawner winSpawner;

    private void Awake()
    {
        Instance = this;
    }
       
    public void EvaluatePlayerChoice(bool wentForward)
    {
        if (requiresDispel && !isDispelled)
        {
            // Player did not dispel the anomaly when required
            Debug.Log("Anomaly was not dispelled. Exit does not count.");
            ResetProgress();
            RollNextHallway();
            return;
        }

        bool wasCorrectChoice = false;

        if (wentForward && !isAnomalyPresent)
        {
            wasCorrectChoice = true; // Forward and no anomaly
            Debug.Log("forward and no anomaly");
        }
        else if (!wentForward && isAnomalyPresent)
        {
            wasCorrectChoice = true; // Backward and anomaly present
            Debug.Log("backward and anomaly present");
        }
        else if (wentForward && isAnomalyPresent)
        {
            wasCorrectChoice = false; // Forward and anomaly present
            Debug.Log("forward and anomaly present");
        }
        else if (!wentForward && !isAnomalyPresent)
        {
            wasCorrectChoice = false; // Backward and no anomaly
            Debug.Log("backward and no anomaly");
        }

        if (wasCorrectChoice)
        {
            currentExitCount++;
            Debug.Log("Correct choice! Current exit count: " + currentExitCount);
            
            if (currentExitCount >= targetWinCount)
            {
                UpdateSign(); // Update the sign to reflect the final exit count
                TriggerWinEscalator();
                return;
            }
        }
        else
        {
            Debug.Log("Incorrect choice. Resetting progress.");
            ResetProgress();
            UpdateSign();
        }

        RollNextHallway();
    }

    public void ResetProgress()
    {
        currentExitCount = 0;
        Debug.Log("Progress reset. Current exit count: " + currentExitCount);
        UpdateSign();
        spawner.ClearAllAnomalies(); // Clear any anomalies when progress is reset
    }

    private void RollNextHallway()
    {
        // reset flags/bool states for the next hallway
        isDispelled = false;
        requiresDispel = false;
        // exit count 3, 4, 5, 7 and 9 have scripted anomaly spawns to drive narrative
        // dont spawn anomaly for exit 10
        // everything else is random chance 50% chance of anomaly spawn and itll NEVER require dispelling (as all dispel types are scripted)
        switch (currentExitCount)
        {
            case 3:
                isAnomalyPresent = spawner.SpawnScriptedAnomaly(currentExitCount, out requiresDispel);
                break;
            case 4:
                isAnomalyPresent = spawner.SpawnScriptedAnomaly(currentExitCount, out requiresDispel);
                break;
            case 5:
                isAnomalyPresent = spawner.SpawnScriptedAnomaly(currentExitCount, out requiresDispel);
                break;
            case 7:
                isAnomalyPresent = spawner.SpawnScriptedAnomaly(currentExitCount, out requiresDispel);
                break;
            case 9:
                isAnomalyPresent = spawner.SpawnScriptedAnomaly(currentExitCount, out requiresDispel);
                break;
            case 10:
                isAnomalyPresent = false;
                spawner.ClearAllAnomalies(); // no anomaly for exit 10
                break;
            default:
                isAnomalyPresent = spawner.SpawnNormalAnomaly();
                break;
        }

        UpdateSign();
    }

    private void UpdateSign()
    {
        if (exitSignController != null)
        {
            exitSignController.UpdateSign(currentExitCount);
        }
        else
        {
            Debug.LogWarning("ExitSignController is not assigned in AnomalyManager.");
        }
    }

    private void TriggerWinEscalator()
    {
        // trigger the win escalator sequence
        Debug.Log("Player has reached the target exit count! Triggering win escalator.");
        
        if (winSpawner != null)
        {
            winSpawner.SpawnWin();
        }
        else
        {
            Debug.LogWarning("WinSpawner is not assigned in AnomalyManager.");
        }
    }
}

