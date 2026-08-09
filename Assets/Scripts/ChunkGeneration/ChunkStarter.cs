// Author : Louis Hoe Zheng Sheng
// this script starts the loop when the player walks down the escalator
// talks to the loop manager via startLoopSystem() function

using UnityEngine;

public class ChunkStarter : MonoBehaviour
{
    public LoopManager loopManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            loopManager.StartLoopSystem(); // activate the loop when the player enters the trigger
            gameObject.SetActive(false); // disable the trigger so that it doesn't activate again
        }
    }
}