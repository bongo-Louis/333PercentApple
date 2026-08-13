using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DispelAnomaly : MonoBehaviour
{
    [Header("UI & Model References")]
    public GameObject dialogText;
    public float displayDuration = 3.0f;
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    [Tooltip("Models/props to hide once the anomaly is dispelled.")]
    public GameObject[] modelsToHide;

    public void HandleAnomaly()
    {
        Debug.Log("DispelAnomaly: HandleAnomaly called.");
        StartCoroutine(DispelSequence());
    }

    private IEnumerator DispelSequence()
    {
        if (dialogText != null)
        {
            dialogText.SetActive(true);
        }

        yield return new WaitForSeconds(displayDuration);

        yield return StartCoroutine(FadeToBlack());

        if (dialogText != null)
        {
            dialogText.SetActive(false);
        }

        SetModelsActive(false);

        if (AnomalyManager.Instance != null)
        {
            AnomalyManager.Instance.isDispelled = true;
        }

        yield return StartCoroutine(FadeToClear());
    }

    private IEnumerator FadeToBlack()
    {
        if (fadeImage == null)
        {
            yield break;
        }

        fadeImage.gameObject.SetActive(true);

        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Clamp01(timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }

    private IEnumerator FadeToClear()
    {
        if (fadeImage == null)
        {
            yield break;
        }

        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(false);
    }

    private void SetModelsActive(bool state)
    {
        if (modelsToHide == null) return;

        foreach (GameObject obj in modelsToHide)
        {
            if (obj != null) obj.SetActive(state);
        }
    }
}