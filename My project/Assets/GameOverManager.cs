using UnityEngine;
using UnityEngine.SceneManagement; // シーンを切り替えるために必要
using System.Collections;          // コルーチンを使うために必要

// このスクリプトは
// 「ゲームオーバー演出」を管理するもの
public class GameOverManager : MonoBehaviour
{
    [Header("ゲームオーバー画像")]
    // 「GAME OVER」と表示する画像オブジェクト
    [SerializeField] private GameObject gameOverImage;

    [Header("リトライ画像")]
    // 「Retry」などのUIオブジェクト
    [SerializeField] private GameObject retryimage;

    [Header("待ち時間")]
    // GAME OVER表示後、何秒待ってからリトライUIを出すか
    [SerializeField] private float waitTime = 2.0f;

    /// <summary>
    /// ゲームオーバー処理を開始する
    /// 他のスクリプト（プレイヤー死亡時など）から呼ばれる
    /// </summary>
    public void ShowGameOver()
    {
        // コルーチン（時間を使った処理）を開始する
        StartCoroutine(GameOverFlow());
    }

    /// <summary>
    /// ゲームオーバーの流れ
    /// ・GAME OVER表示
    /// ・少し待つ
    /// ・リトライUI表示
    /// </summary>
    IEnumerator GameOverFlow()
    {
        // GAME OVER画像を表示する
        gameOverImage.SetActive(true);

        // 指定した秒数だけ待つ
        yield return new WaitForSeconds(waitTime);

        // リトライUIを表示する
        retryimage.SetActive(true);
    }

    /// <summary>
    /// リトライ処理
    /// 今のシーンをもう一度読み込む
    /// </summary>
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// ゲーム終了処理
    /// ビルドしたゲームでのみ有効
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}