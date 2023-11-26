using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScreen : MonoBehaviour
{
    public void OpenScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}