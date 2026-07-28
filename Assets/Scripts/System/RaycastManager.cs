// Author : Louis Hoe Zheng Sheng
// ok this script might be kinda hard to for me to work on
// but this script is attached to the main camera, and is used to communicate with other scripts and other game objects
// example, toggling an outline, etc.
// if i in the future add more features i think im gonna call this script something else lol
// you attach this to the maincamera btw

// import unity
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    // settings for the raycaster for that easy tweaking
    [Header("Raycast Settings")]
    [SerializeField] private float rayDistance = 10f; // The distance of the raycast
    [SerializeField] private LayerMask raycastLayerMask; // The layer mask for the raycast

    [Header("UI Settings")]
    [SerializeField] private CrosshairUI crosshairUI; // Optional crosshair UI reference

    private IInteractable currentInteractable; // The currently detected interactable object

    void Update()
    {
        HandleRaycast();
    }

    private void HandleRaycast()
    {
        // raycast from the middle of the camera viewport
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, raycastLayerMask))
        {
            // Check if the hit object has an IInteractable component (uses the rules from IInteractable.cs)
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                // If we hit a new interactable, call OnRaycastEnter on it
                if (interactable != currentInteractable)
                {
                    currentInteractable?.OnRaycastExit(); // Call exit on the previous interactable
                    currentInteractable = interactable;
                    currentInteractable.OnRaycastEnter(); // Call enter on the new interactable
                }

                // Update crosshair hover state while aiming at an interactable
                crosshairUI?.ShowHoverState(currentInteractable.PromptText);
            }
            else
            {
                // If we hit something that is not interactable, call OnRaycastExit on the current interactable
                currentInteractable?.OnRaycastExit();
                currentInteractable = null;
                crosshairUI?.HideHoverState();
            }
        }
        else
        {
            // If we didn't hit anything, call OnRaycastExit on the current interactable
            currentInteractable?.OnRaycastExit();
            currentInteractable = null;
            crosshairUI?.HideHoverState();
        }
    }
}