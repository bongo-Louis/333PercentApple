using System.Collections;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [Header("Settings")]
    public GameObject dialogObject; // Drag your Dialog UI GameObject here
    public float displayDuration = 3f; // How long to show (in seconds)

    private Coroutine hideCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        // Check if Player entered (make sure Player has "Player" tag)
        if (other.CompareTag("Player"))
        {
            ShowDialog();
        }
    }

    public void ShowDialog()
    {
        if (dialogObject == null) return;

        // Activate the dialog UI
        dialogObject.SetActive(true);

        // Restart timer if already running
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        // Start countdown to hide
        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        dialogObject.SetActive(false);
        Destroy(gameObject); // Destroy the trigger after hiding the dialog
    }
}