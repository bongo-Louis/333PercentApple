using System.Collections;
using UnityEngine;

public class Stage2 : MonoBehaviour
{
    [Header("Target GameObjects")]
    [SerializeField] private GameObject victim;
    [SerializeField] private GameObject chaser;

    [Header("Victim Target Positions (4 Waypoints)")]
    [SerializeField] private Vector3[] victimPositions = new Vector3[4];

    [Header("Chaser Target Positions (4 Waypoints)")]
    [SerializeField] private Vector3[] chaserPositions = new Vector3[4];

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float turnSpeed = 10.0f;
    [SerializeField] private float pauseDuration = 1.0f;

    [Header("Use Parent/Local Coordinates?")]
    [Tooltip("Check this if coordinates are relative to a parent container.")]
    [SerializeField] private bool useLocalPosition = false;

    // Animators
    private Animator victimAnim;
    private Animator chaserAnim;
    private static readonly int IsWalkingHash = Animator.StringToHash("isWalking");

    private bool isOscillating = false;

    void Start()
    {
        // Cache Animators if present on the GameObjects or their children
        if (victim != null) victimAnim = victim.GetComponentInChildren<Animator>();
        if (chaser != null) chaserAnim = chaser.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!isOscillating)
        {
            StartCoroutine(OscillateRoutine());
        }
    }

    private IEnumerator OscillateRoutine()
    {
        isOscillating = true;

        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector3 vTarget = (i < victimPositions.Length) ? victimPositions[i] : Vector3.zero;
                Vector3 cTarget = (i < chaserPositions.Length) ? chaserPositions[i] : Vector3.zero;

                // Start Walking Animation
                SetWalkingAnimation(true);

                yield return StartCoroutine(MoveAndRotateBoth(vTarget, cTarget));

                // Stop Walking Animation during pause
                SetWalkingAnimation(false);

                yield return new WaitForSeconds(pauseDuration);
            }
        }
    }

    private IEnumerator MoveAndRotateBoth(Vector3 victimTarget, Vector3 chaserTarget)
    {
        while (GetDistance(victim.transform, victimTarget) > 0.01f ||
               GetDistance(chaser.transform, chaserTarget) > 0.01f)
        {
            if (victim != null) MoveAndRotateUnit(victim.transform, victimTarget);
            if (chaser != null) MoveAndRotateUnit(chaser.transform, chaserTarget);

            yield return null;
        }

        // Snap exact target upon arrival
        SetPos(victim.transform, victimTarget);
        SetPos(chaser.transform, chaserTarget);
    }

    private void MoveAndRotateUnit(Transform unitTransform, Vector3 targetPos)
    {
        Vector3 currentPos = useLocalPosition ? unitTransform.localPosition : unitTransform.position;
        Vector3 direction = (targetPos - currentPos);

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            unitTransform.rotation = Quaternion.Slerp(
                unitTransform.rotation, 
                targetRotation, 
                turnSpeed * Time.deltaTime
            );
        }

        Vector3 newPos = Vector3.MoveTowards(currentPos, targetPos, moveSpeed * Time.deltaTime);

        if (useLocalPosition)
            unitTransform.localPosition = newPos;
        else
            unitTransform.position = newPos;
    }

    private void SetWalkingAnimation(bool state)
    {
        if (victimAnim != null) victimAnim.SetBool(IsWalkingHash, state);
        if (chaserAnim != null) chaserAnim.SetBool(IsWalkingHash, state);
    }

    private float GetDistance(Transform t, Vector3 target)
    {
        if (t == null) return 0f;
        Vector3 current = useLocalPosition ? t.localPosition : t.position;
        return Vector3.Distance(current, target);
    }

    private void SetPos(Transform t, Vector3 target)
    {
        if (t == null) return;
        if (useLocalPosition) t.localPosition = target;
        else t.position = target;
    }
}