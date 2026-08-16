using UnityEngine;

public class UnlockCursor : MonoBehaviour
{
    // select cursor manager
    public CursorManager cursorManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cursorManager = FindObjectOfType<CursorManager>();
        if (cursorManager != null)
        {
            cursorManager.UnlockCursor();
        }
        else
        {
            Debug.LogWarning("CursorManager not found in the scene.");
        }
    }
}
