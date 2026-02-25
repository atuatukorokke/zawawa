using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryManager : MonoBehaviour
{
    public void Retry()
    {
        Time.timeScale = 1f;    
        // ¡ŠJ‚¢‚Ä‚¢‚éƒV[ƒ“‚ğÄ“Ç‚İ‚İ
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}