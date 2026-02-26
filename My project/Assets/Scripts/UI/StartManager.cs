using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public void GoToStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene"); // ←スタートシーン名に変更
    }
}