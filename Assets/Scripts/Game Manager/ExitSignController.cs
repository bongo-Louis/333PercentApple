using System.Collections.Generic;
using UnityEngine;

public class ExitSignController : MonoBehaviour
{
    [Tooltip("Leave empty to auto-grab children in hierarchy order, or drag them in manually.")]
    public List<GameObject> signObjects = new List<GameObject>();

    /// <summary>
    /// Activates the sign at exitIndex and deactivates all other signs.
    /// Example: UpdateSign(0) turns on Sign 0 (or 1st child), deactivates the rest.
    /// </summary>
    public void UpdateSign(int exitScore)
{
    // Apply the offset: Exit Score 0 becomes Index 0 (Sign 1)
    int targetIndex = exitScore; 

    for (int i = 0; i < signObjects.Count; i++)
    {
        if (signObjects[i] != null)
        {
            signObjects[i].SetActive(i == targetIndex);
        }
    }
}
}