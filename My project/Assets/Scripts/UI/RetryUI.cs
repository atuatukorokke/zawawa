using UnityEngine;
using UnityEngine.SceneManagement; // シーンを切り替えるために必要

// このスクリプトは
// リトライ画面でのキー入力を管理するもの
public class RetryUI : MonoBehaviour
{
    void Update()
    {
        // このUIが非表示なら何もしない
        // （activeSelf が false のときは処理を止める）
        if (!gameObject.activeSelf) return;

        // Qキーが押されたらリトライ
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Retry();
        }

        // Eキーが押されたらタイトルへ戻る
        if (Input.GetKeyDown(KeyCode.E))
        {
            BackToTitle();
        }
    }

    /// <summary>
    /// 現在のシーンをもう一度読み込む（リトライ）
    /// </summary>
    void Retry()
    {
        // もしTime.timeScaleを0にしていた場合のために戻しておく
        Time.timeScale = 1f;

        // 今開いているシーンを取得
        Scene current = SceneManager.GetActiveScene();

        // 同じシーンを読み込み直す
        SceneManager.LoadScene(current.name);
    }

    /// <summary>
    /// タイトル画面へ戻る処理
    /// </summary>
    void BackToTitle()
    {
        // "StartScene" はタイトルシーンの名前
        // 自分のプロジェクトのシーン名に合わせて変更すること
        SceneManager.LoadScene("aStartScene");
    }
}