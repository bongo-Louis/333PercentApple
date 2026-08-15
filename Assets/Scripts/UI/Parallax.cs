using UnityEngine;
using UnityEngine.InputSystem;

public class Parallax : MonoBehaviour
{
    [Header("Parallax Settings")]
    [Tooltip("Maximum offset distance in 3D units.")]
    public Vector2 maxOffset = new Vector2(1.5f, 1.0f);
    
    [Tooltip("Smoothing speed for camera motion.")]
    public float smoothSpeed = 5.0f;

    [Header("Optional Focus Point")]
    public Transform target;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Read mouse position using the Active Input System
        if (Pointer.current == null) return;
        Vector2 mousePos = Pointer.current.position.ReadValue();

        // Convert mouse position to normalized screen coordinates (-1 to 1)
        float normalizedX = (mousePos.x / Screen.width - 0.5f) * 2f;
        float normalizedY = (mousePos.y / Screen.height - 0.5f) * 2f;

        // Calculate target offset
        Vector3 targetOffset = transform.right * (normalizedX * maxOffset.x) 
                             + transform.up * (normalizedY * maxOffset.y);
        
        Vector3 targetPosition = initialPosition + targetOffset;

        // Smoothly move position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);

        // Optional: Keep looking at target if provided
        if (target != null)
        {
            Quaternion targetRot = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * smoothSpeed);
        }
    }
}