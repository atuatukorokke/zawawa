using UnityEngine;
using UnityEngine.SceneManagement; // シーンを切り替えるために必要

// このスクリプトは
// ・Enterキーでシーンを切り替える
// ・シーンが読み込まれたときに特定の処理をする
// ためのスクリプト
public class SceneChange : MonoBehaviour
{
    private void Update()
    {
        // Enterキーが押された瞬間
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // "PlayerScene 1" という名前のシーンを読み込む
            SceneManager.LoadScene("Stage01");
        }
    }

    private void OnEnable()
    {
        // シーンが読み込まれたときに呼ばれるイベントに登録する
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // オブジェクトが無効になったらイベント登録を解除する
        // （これをしないとバグの原因になることがある）
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// シーンが読み込まれたときに呼ばれる処理
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 読み込まれたシーンの名前をチェック
        if (scene.name == "Stage01")
        {
            // TimeCounterというシングルトンのタイマーを開始する
            TimeCounter.Instance.StartTimer();
        }
    }
}