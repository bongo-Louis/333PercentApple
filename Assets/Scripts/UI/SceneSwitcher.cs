// Author : Louis Hoe Zheng Sheng
// Description : Versatile script to assign any scene to the button that has this script. Loads scene when button is clicked.
// this is very simple

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // type which scene to load from the attribute in the inspector
    [SerializeField] private string sceneToLoad;

    // function to load the scene when button is clicked
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // on click
    public void OnClick()
    {
        LoadScene();
    }

}