using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class UIController : MonoBehaviour
{
    public GameObject pauseUI;
    public static bool isGamePaused;

    

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
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
        pauseUI.SetActive(false);
        isGamePaused = false;
        //Time.timeScale = 1;
        Debug.Log("Resume button is pressed");
    }
    public void PauseGame()
    { 
        pauseUI.SetActive(true);
        isGamePaused = true;
        //Time.timeScale = 0;
    }


}
