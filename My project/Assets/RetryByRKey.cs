using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryByRKey : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Retry();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            NextScene();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            PreviousScene();
        }
    }

    void Retry()
    {
        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void NextScene()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
    }

    void PreviousScene()
    {
        int prevIndex = SceneManager.GetActiveScene().buildIndex - 1;

        if (prevIndex >= 0)
        {
            SceneManager.LoadScene(prevIndex);
        }
    }
}