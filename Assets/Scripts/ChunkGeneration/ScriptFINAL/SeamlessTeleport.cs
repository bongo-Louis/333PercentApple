// Author : Louis Hoe Zheng Sheng
// this is genuinely the last time im gonna throw myself
// chunk gen doesnt work and so do i so lets just teleport
// OH MY GOD THE SCRIPT WORKS

using System.Collections;
using UnityEngine;

public class SeamlessTeleport : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform destinationAnchor; // where is the middle chunk equavalent to the trigger of the subchunk?
    public Transform player; // player transform to teleport
    public CharacterController playerController; // to freeze the player while teleporting to avoid collision issues
    private static bool isTeleporting = false; // bool to prevent multiple teleportations at once

    // when a trigger is hit
    private void OnTriggerEnter(Collider other)
    {
        // is the player the one that hit the trigger? is it already "teleporting"?
        if (other.CompareTag("Player") && !isTeleporting)
        {
            // start the teleport routine
            StartCoroutine(TeleportRoutine());
        }
    }

    private IEnumerator TeleportRoutine()
    {
        // set isTeleporting to true to prevent triggerr spam
        isTeleporting = true;

        // vector difference between destination and trigger
        Vector3 offset = destinationAnchor.position - transform.position;

        if (playerController !=null) // failsafe
        {
            playerController.enabled = false; // Disable the CharacterController to avoid collision issues
        }

        // offset the player position by the value calc above
        player.position += offset;

        if (playerController != null) // failsafe
        {
            playerController.enabled = true; // Re-enable the CharacterController after teleporting
        }
        
        // wait a short time to prevent multiple triggers from firing
        yield return new WaitForSeconds(0.1f); 
        // allow teleporting again
        isTeleporting = false;

        // add future anomaly callings here if needed
    }
}
