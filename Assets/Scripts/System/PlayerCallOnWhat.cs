// Author : Louis Hoe Zheng Sheng
// Description: This script is attached to the player body and is used to call other scripts on start, such as locking the cursor.
// Date: 25/07/2026

using UnityEngine;

public class PlayerCallOnWhat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // lock the cursor access CursorManager
        CursorManager cursorManager = FindObjectOfType<CursorManager>();
        if (cursorManager != null)
        {
            cursorManager.LockCursor();
        }
        else
        {
            Debug.LogWarning("CursorManager not found in the scene.");
        }

    }
}
