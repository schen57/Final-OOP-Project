using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class UIController : MonoBehaviour
{

    public static bool isGamePaused;

    

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        isGamePaused = false;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        isGamePaused = false;
        Debug.Log("Menu button is pressed");
    }

    public void QuitGame()
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        isGamePaused = false;

    }

    public void ResumeGame()
    { 
        isGamePaused = false;
        Debug.Log("Resume button is pressed");
    }
    public void PauseGame()
    { 
        
        isGamePaused = true;
        
    }


}
