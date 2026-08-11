// Author : Louis Hoe Zheng Sheng, Carolyn Ong
// Description: Depends on RayCastManager. Manages the outline + text that appears when the raycast hits an interactable object.
// Date: 27/07/2026

using UnityEngine;

public class OutlineInteractable : MonoBehaviour, IInteractable
{
    // Drag your Outline component (or any script controlling outline) here in the Inspector
    [SerializeField] private MonoBehaviour outlineComponent;
    // Prompt text that will be displayed when the player looks at this interactable object
    [SerializeField] private string promptText = "Interact";

    // share the prompt text with the UI system so the text can update
    public string PromptText => promptText;

    // onRaycastEnter and onRaycastExit are called by the RaycastManager when the player looks at this interactable object
    public void OnRaycastEnter()
    {
    }
    
    public void OnRaycastExit()
    {
    }
}