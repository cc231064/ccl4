using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        // Load the specified scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
        Debug.Log("Game is quitting");
    }
}