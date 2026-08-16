// Author : Louis Hoe Zheng Sheng
//  this script pushes the player in a certain direction when they are on the escalator, simulating the movement of an escalator.

using UnityEngine;

public class EscalatorForce : MonoBehaviour
{
    [Header("Escalator Settings")]
    [Tooltip("Direction to push the player (e.g., up and forward along the slope)")]
    public Vector3 moveDirection = new Vector3(0, 1, 1);
    public float speed = 3.0f;

    private void OnTriggerStay(Collider other)
    {
        // Check if the object entering is the player OR item
        if (other.CompareTag("Player") || other.CompareTag("Item"))
        {
            // Try to get CharacterController component
            CharacterController controller = other.GetComponent<CharacterController>();
            
            if (controller != null)
            {
                // Normalize direction to keep movement speed consistent regardless of vector length
                Vector3 worldDirection = transform.TransformDirection(moveDirection.normalized);
                
                // Move player continuously while inside trigger
                controller.Move(worldDirection * speed * Time.deltaTime);
            }
        }
    }
}