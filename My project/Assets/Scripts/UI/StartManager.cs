using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public void GoToStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("aStartScene"); // ←スタートシーン名に変更
    }
}