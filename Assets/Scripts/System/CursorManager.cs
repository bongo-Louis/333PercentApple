//  Author : Louis Hoe Zheng Sheng
//  Simple script that will lock and unlock the cursor on start
//  this will be attached to a dontdestroyon load object and you can call this script to lock and unlock the cursor when needed
// Date: 25/07/2026


using UnityEngine;

public class CursorManager : MonoBehaviour
{
    // dont die when you switch scenes
    private void Awake()
    {
        // unless theres already a cursor manager in the scene, then destroy this one
        if (FindObjectsOfType<CursorManager>().Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // function to lock the cursor
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // function to unlock the cursor
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}