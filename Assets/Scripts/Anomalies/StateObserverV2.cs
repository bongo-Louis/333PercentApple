using UnityEngine;

public class StateObserverV2 : MonoBehaviour
{
    [Tooltip("The object that will always have the OPPOSITE active state of this object.")]
    [SerializeField] private GameObject oppositeObject;

    private void OnEnable()
    {
        // When THIS object turns ON, turn the opposite object OFF
        SetOppositeState(false);
    }

    private void OnDisable()
    {
        // When THIS object turns OFF, turn the opposite object ON
        SetOppositeState(true);
    }

    private void SetOppositeState(bool state)
    {
        if (oppositeObject != null)
        {
            oppositeObject.SetActive(state);
        }
    }
}