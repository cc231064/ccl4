using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueButtons : MonoBehaviour
{
    public void OnMainMenuPressed()
    {
        // Replace "MainMenu" with your actual scene name
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnQuitPressed()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
