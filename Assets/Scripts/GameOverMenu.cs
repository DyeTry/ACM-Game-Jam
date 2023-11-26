using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    bool gameHasEnded = false;
    [SerializeField] private GameObject gameOver;

    public void EndGame()
    {
        if (gameHasEnded == false) 
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            GameOver();
        }
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
        gameHasEnded = false;

        //gameOver.SetActive(true);
    }

    public void Continue()
    {
        SceneManager.LoadScene(0);
        gameHasEnded = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}