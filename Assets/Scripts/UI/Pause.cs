//Author:Jayden Ong
//Description: Script to pause the game and switch between scenes

using UnityEngine;
using UnityEngine.InputSystem; // 1. Include namespace

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    public static bool isPaused = false;

    // 2. Unity automatically calls "OnPause" when the "Pause" Action is triggered
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
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}