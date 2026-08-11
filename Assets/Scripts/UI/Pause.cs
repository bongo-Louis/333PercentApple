//Author:Jayden Ong
//Description: Script to pause the game and switch between scenes

using UnityEngine;  
using StarterAssets;
using UnityEngine.InputSystem; // 1. Include namespace

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    public static bool isPaused = false;
    
    // new update where we add cursor manager
    private StarterAssetsInputs starterAssetsInputs;

    // 2. Unity automatically calls "OnPause" when the "Pause" Action is triggered

    // find the starter assets input script in the scene
    private void Awake()
    {
        starterAssetsInputs = FindObjectOfType<StarterAssetsInputs>();
        if (starterAssetsInputs == null)
        {
            Debug.LogError("StarterAssetsInputs not found in the scene.");
        }
    }
    public void OnPause(InputValue value)
    {
        // Only toggle when the button is initially pressed down
        if (value.isPressed)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // new update where we lock the camera from being moved when the game is paused
        starterAssetsInputs.cursorInputForLook = false;
        starterAssetsInputs.cursorLocked = false;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        starterAssetsInputs.cursorInputForLook = true;
        starterAssetsInputs.cursorLocked = true;
    }
}