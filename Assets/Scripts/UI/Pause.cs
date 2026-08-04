//Author:Jayden Ong
//Description: Script to pause the game and switch between scenes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Private variable that still shows up in editor
    [SerializeField] GameObject pauseMenu; 
    [SerializeField] private string sceneID;
    
    public void Pause()
    {
        //Freezes time to pause game
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        //Unfreezes game to unpause
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Menu(int sceneID)
    {
        //Unfreezes game and loads a different scene(Menu)
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
}