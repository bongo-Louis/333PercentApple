// Author : Louis Hoe Zheng Sheng
// simple script that will expand the crosshair and show text when the player is aiming at an interactable object

using UnityEngine;
using TMPro;
public class CrosshairUI : MonoBehaviour
{
    [Header("Scale Settings")]
    [SerializeField] private Vector3 normalScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 hoverScale = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField] private float speed = 12f;

    [Header("Text Settings")]
    [SerializeField] private TextMeshProUGUI promptTextLabel; // Assign your UI text here

    private Vector3 targetScale;

    public TextMeshProUGUI PromptTextLabel { get => promptTextLabel; set => promptTextLabel = value; }

    // Initialize the crosshair scale and hide the prompt text on start
    private void Awake()
    {
        targetScale = normalScale;
        transform.localScale = normalScale;

        if (PromptTextLabel != null)
            PromptTextLabel.gameObject.SetActive(false);
    }


    // Update the crosshair scale smoothly towards the target scale
    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
    }

    // Show the hover state with the specified message when pinged from RaycastManager
    public void ShowHoverState(string message)
    {
        targetScale = hoverScale;

        if (PromptTextLabel != null)
        {
            PromptTextLabel.text = message;
            PromptTextLabel.gameObject.SetActive(true);
        }
    }
    // same as above but shrink the crosshair and hide the text when pinged from RaycastManager
    public void HideHoverState()
    {
        targetScale = normalScale;

        if (PromptTextLabel != null)
        {
            PromptTextLabel.gameObject.SetActive(false);
        }
    }
}

