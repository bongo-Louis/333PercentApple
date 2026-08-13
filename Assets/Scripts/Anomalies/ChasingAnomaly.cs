using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class ChasingAnomaly : MonoBehaviour
{
    [Header("Detection & Chase")]
    public float chaseRadius = 8f;
    public float catchdistance = 1.5f;
    public float chaseSpeed = 3.5f;

    [Header("Fade UI")]
    public GameObject fadeObject;
    public Image fadeImage;
    public float fadeDuration = 1f;

    [Header("References")]
    public Transform playerTransform;
    public Transform teleportPosition;

    private NavMeshAgent agent;
    private Animator animator;
    private CharacterController playerController;
    private bool isChasing = false;
    private bool isCaught = false;
    private Vector3 startPos;
    private Quaternion startRot;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        startPos = transform.position;
        startRot = transform.rotation;

        if (fadeObject != null && fadeImage == null)
        {
            fadeImage = fadeObject.GetComponent<Image>();
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
            playerController = playerTransform.GetComponent<CharacterController>();
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found in the scene.");
        }
    }

    private void Update()
    {
        if (playerTransform == null || isCaught || !gameObject.activeSelf) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= chaseRadius)
        {
            isChasing = true;

            if (agent != null)
            {
                agent.isStopped = false;
                agent.speed = chaseSpeed;
                agent.SetDestination(playerTransform.position);
            }

            if (animator != null)
            {
                animator.SetBool("isChasing", true);
            }
        }

        if (isChasing && distanceToPlayer <= catchdistance)
        {
            StartCoroutine(CaughtPlayer());
        }
    }

    private IEnumerator CaughtPlayer()
    {
        if (isCaught) yield break;
        isCaught = true;

        if (agent != null)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

        if (animator != null)
        {
            animator.SetBool("isChasing", false);
        }

        yield return StartCoroutine(FadeToBlack());
        yield return new WaitForSeconds(0.5f);

        if (playerTransform != null)
        {
            if (playerController == null)
            {
                playerController = playerTransform.GetComponent<CharacterController>();
            }

            if (playerController != null)
            {
                playerController.enabled = false;
            }

            if (teleportPosition != null)
            {
                playerTransform.position = teleportPosition.position;
                playerTransform.rotation = teleportPosition.rotation;
            }
            else
            {
                Debug.LogWarning("Teleport position is not assigned on " + name);
            }

            if (playerController != null)
            {
                playerController.enabled = true;
            }
        }

        yield return StartCoroutine(FadeToClear());

        if (AnomalyManager.Instance != null)
        {
            transform.position = startPos;
            transform.rotation = startRot;
            AnomalyManager.Instance.ResetProgress();
        }
    }

    private IEnumerator FadeToBlack()
    {
        if (fadeObject != null)
        {
            fadeObject.SetActive(true);
        }

        if (fadeImage == null)
        {
            yield break;
        }

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
            if (fadeObject != null)
            {
                fadeObject.SetActive(false);
            }
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

        if (fadeObject != null)
        {
            fadeObject.SetActive(false);
        }
    }
}