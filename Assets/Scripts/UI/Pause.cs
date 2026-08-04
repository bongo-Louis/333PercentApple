//Author:Jayden Ong
//Description: Script to pause the game and switch between scenes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Private variable that still shows up in editor 
    [SerializeField] private GameObject pauseMenuUI;
    public static bool isPaused = false;

    void Update()
    {
        //Checks if the escape key is pressed
        if ()
        {
            //If the pause menu is active, resume game
            if (isPaused)
            {
                Resume();
            }
            //If the pause menu is inactive, pause game
            else
            {
                Pause();
            }
        }
    }
    
    public void Pause()
    {
        //Freezes time to pause game
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        //Unfreezes game to unpause
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}