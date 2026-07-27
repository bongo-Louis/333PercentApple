// Author : Louis Hoe Zheng Sheng
// gonna preface this by saying that this depends on raycastmanager
// so read that if you can :3c

using UnityEngine;

public class OutlineInteractable : MonoBehaviour, IInteractable
{
    // Drag your Outline component (or any script controlling outline) here in the Inspector
    [SerializeField] private MonoBehaviour outlineComponent;
    // This is a serialized field for the prompt text that will be displayed when the player looks at this interactable object
    [SerializeField] private string promptText = "Interact";

    // share the prompt text with the UI system so the text can update
    public string PromptText => promptText;

    // outline disabled by default
    private void Awake()
    {
        if (outlineComponent != null)
            outlineComponent.enabled = false;
    }

    // onRaycastEnter and onRaycastExit are called by the RaycastManager when the player looks at this interactable object
    public void OnRaycastEnter()
    {
        if (outlineComponent != null) 
            outlineComponent.enabled = true;
    }
    public void OnRaycastExit()
    {
        if (outlineComponent != null) 
            outlineComponent.enabled = false;
    }
}