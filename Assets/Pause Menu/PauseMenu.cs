using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        //isPaused = false;
    }

    public void Resume()
    {
        //Debug.Log("Resuming");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        //Debug.Log("Pausing");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Quit(int sceneID)
    {
        //Debug.Log("Quit");
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    public void LoadMenu()
    {
        //Debug.Log("LoadMenu");
    }
}
