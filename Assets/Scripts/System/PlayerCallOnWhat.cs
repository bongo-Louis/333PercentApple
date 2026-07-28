// Author : Louis Hoe Zheng Sheng
// okay this script is abit funnier
// but this script acts as a driver to call other scripts in public 
// that MIIGHT need to be called on start when the player body is active, 
// for example cursormanager, scene switcher, etc

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
