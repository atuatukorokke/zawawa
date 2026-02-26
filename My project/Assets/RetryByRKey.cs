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
    }

    void Retry()
    {
        // ★ 時間を戻す（超重要）
        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}