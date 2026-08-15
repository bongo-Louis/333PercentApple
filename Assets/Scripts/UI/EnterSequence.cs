using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneFaderLoader : MonoBehaviour
{
    [Header("UI & Transition Settings")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private string targetSceneName;

    private bool isFading = false;

    private void Update()
    {
        if (isFading) return;

        // Check active keyboard input for Space or E
        var keyboard = Keyboard.current;
        if (keyboard != null && (keyboard.spaceKey.wasPressedThisFrame || keyboard.eKey.wasPressedThisFrame))
        {
            StartCoroutine(FadeAndLoad());
        }
    }

    private IEnumerator FadeAndLoad()
    {
        isFading = true;
        float elapsed = 0f;

        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.blocksRaycasts = true; // Prevent UI interactions while fading
            float startAlpha = fadeCanvasGroup.alpha;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, elapsed / fadeDuration);
                yield return null;
            }

            fadeCanvasGroup.alpha = 1f;
        }

        SceneManager.LoadScene(targetSceneName);
    }
}