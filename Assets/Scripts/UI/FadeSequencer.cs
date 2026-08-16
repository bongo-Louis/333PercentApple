using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiObjectFadeSequence : MonoBehaviour
{
    public enum FadeType { FadeIn, FadeOut }

    [System.Serializable]
    public class FadeStep
    {
        public CanvasGroup targetGroup;
        public FadeType fadeType = FadeType.FadeIn;
        [Tooltip("Delay in seconds before fading this specific object")]
        public float delayBefore = 0f;
        [Tooltip("Duration of the fade transition")]
        public float fadeDuration = 1.0f;
    }

    [Header("Sequence Settings")]
    public List<FadeStep> fadeSequence = new List<FadeStep>();

    public SceneSwitcher sceneSwitcher; // Reference to the SceneSwitcher script

    private void Start()
    {
        // Initialize starting alphas based on targeted action
        foreach (var step in fadeSequence)
        {
            if (step.targetGroup != null)
            {
                step.targetGroup.alpha = (step.fadeType == FadeType.FadeIn) ? 0f : 1f;
            }
        }

        StartCoroutine(RunSequence());

    }

    private IEnumerator RunSequence()
    {
        // 1. Run through and wait for every fade step in order
        foreach (var step in fadeSequence)
        {
            if (step.targetGroup == null) continue;

            if (step.delayBefore > 0f)
            {
                yield return new WaitForSeconds(step.delayBefore);
            }

            float startAlpha = (step.fadeType == FadeType.FadeIn) ? 0f : 1f;
            float endAlpha = (step.fadeType == FadeType.FadeIn) ? 1f : 0f;

            yield return StartCoroutine(FadeGroup(step.targetGroup, startAlpha, endAlpha, step.fadeDuration));
        }
        // 2. Trigger scene load once after the entire sequence finishes
        SceneHandover();
    }

    private IEnumerator FadeGroup(CanvasGroup cg, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            yield return null;
        }

        cg.alpha = endAlpha;
    }

    private void SceneHandover()
    {
        if (sceneSwitcher != null)
        {
            sceneSwitcher.LoadScene();
        }
        else
        {
            Debug.LogWarning("SceneSwitcher reference is not set.");
        }
    }
}