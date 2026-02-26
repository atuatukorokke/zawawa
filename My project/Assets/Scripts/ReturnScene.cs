using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("キーが押された！ スタートシーンに戻るよ！");
            SceneManager.LoadScene("StartScene");
        }
    }
}
