// Author : Louis Hoe Zheng Sheng, Carolyn Ong
// Description: Attached to the player body, used to lock cursor and enter the building.
// Date: 25/07/2026

using UnityEngine;
using TMPro;

public class PlayerCallOnWhat : MonoBehaviour
{
    private IInteractable currentInteractable;

    // get the text from the UI and teleport point
    [SerializeField] private TMP_Text promptText;
    [SerializeField] private Transform teleportPoint;
    [SerializeField] private CharacterController player;

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

    public void OnInteract()
    {
        if (promptText.text == "Enter")
        {
            player.enabled = false;
            player.transform.position = teleportPoint.position;
            player.transform.rotation = teleportPoint.rotation;
            player.enabled = true;
        }    
    }
}