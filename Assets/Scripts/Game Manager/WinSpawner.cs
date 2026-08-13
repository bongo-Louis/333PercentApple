using UnityEngine;

public class WinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject winObject;

    private void Start()
    {
        // Make sure the win object starts hidden.
        if (winObject != null)
        {
            winObject.SetActive(false);
        }
    }

    public void SpawnWin()
    {
        if (winObject != null)
        {
            winObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("WinSpawner: Win Object has not been assigned.");
        }
    }
}
