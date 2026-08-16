// Author : Louis Hoe Zheng Sheng, Carolyn Ong
// Description: Attached to the player body, used to lock cursor and enter the building.
// Date: 25/07/2026

using UnityEngine;
using TMPro;

public class PlayerCallOnWhat : MonoBehaviour
{
    [SerializeField] private IInteractable currentInteractable;
    [SerializeField] private DispelAnomaly dispel;

    // get the text from the UI and teleport point
    [SerializeField] private TMP_Text promptText;
    [SerializeField] private Transform teleportPoint;
    [SerializeField] private CharacterController player;

    [SerializeField] private SceneSwitcher sceneSwitcher;

    [Header("Cooldown Settings")]
    [SerializeField] private float interactCooldown = 5f;
    private float nextInteractTime = 0f;

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
        // Check if cooldown is still active
        if (Time.time < nextInteractTime)
        {
            return;
        }

        if (promptText.text == "Enter")
        {
            nextInteractTime = Time.time + interactCooldown;
            player.enabled = false;
            sceneSwitcher.LoadScene();
        }
        else if (promptText.text == "Interact")
        {
            if (dispel == null || !dispel.isActiveAndEnabled)
            {
                dispel = FindObjectOfType<DispelAnomaly>();
            }

            if (dispel != null && dispel.isActiveAndEnabled)
            {
                nextInteractTime = Time.time + interactCooldown;
                Debug.Log("Dispel called");
                dispel.HandleAnomaly();
            }
            else
            {
                Debug.LogWarning("No active DispelAnomaly found for interaction.");
            }
        }
    }
}