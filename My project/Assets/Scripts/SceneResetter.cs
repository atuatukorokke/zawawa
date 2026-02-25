using UnityEngine;
using UnityEngine.SceneManagement; // ★これが必要

public class SceneResetter : MonoBehaviour
{
    // ステージをリセットするメソッド
    public void ResetStage()
    {
        // 現在アクティブなシーン名を取得
        string currentSceneName = SceneManager.GetActiveScene().name;
        // シーンを読み込み直す（再読み込み）
        SceneManager.LoadScene(currentSceneName);
    }

    // 例：Rキーを押したらリセット
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetStage();
        }
    }
}
