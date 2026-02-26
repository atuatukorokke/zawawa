using UnityEngine;
using UnityEngine.SceneManagement;

// このスクリプトは
// ・敵の出現と撃破をイベントでカウントする
// ・敵が全滅したら次のシーンへ進む
// ための管理クラス
public class NextSceneOnEnemyClear : MonoBehaviour
{
    // 現在生きている敵の数
    int enemyCount = 0;

    // 初期化が終わったかどうか
    // （誤作動を防ぐためのフラグ）
    bool initialized = false;

    private void OnEnable()
    {
        // 敵出現イベントに登録
        EnemyDeathNotifier.OnEnemySpawned += OnEnemySpawned;

        // 敵撃破イベントに登録
        EnemyDeathNotifier.OnEnemyDestroyed += OnEnemyDestroyed;
    }

    private void OnDisable()
    {
        // イベント解除（超重要）
        EnemyDeathNotifier.OnEnemySpawned -= OnEnemySpawned;
        EnemyDeathNotifier.OnEnemyDestroyed -= OnEnemyDestroyed;
    }

    void Start()
    {
        // 今回はStart時に敵を数えない
        // すべてイベント管理にしている

        initialized = true;

        Debug.Log("EnemyClearManager 起動");
    }

    // 敵が出現したとき呼ばれる
    void OnEnemySpawned()
    {
        enemyCount++;

        Debug.Log("敵出現 +1 → " + enemyCount);
    }

    // 敵が倒されたとき呼ばれる
    void OnEnemyDestroyed()
    {
        // 初期化前なら無視
        if (!initialized) return;

        enemyCount--;

        Debug.Log("敵撃破 -1 → " + enemyCount);

        // 敵が0以下になったら全滅と判断
        if (enemyCount <= 0)
        {
            initialized = false;

            LoadNextScene();
        }
    }

    // 次のシーンへ進む処理
    void LoadNextScene()
    {
        // 今いるシーンの番号を取得
        int index = SceneManager.GetActiveScene().buildIndex;

        // Build Settingsに登録されているシーン数
        int max = SceneManager.sceneCountInBuildSettings;

        // 次のシーンが存在するなら
        if (index + 1 < max)
        {
            SceneManager.LoadScene(index + 1);
        }
        else
        {
            Debug.Log("これ以上次のシーンはありません");
        }
    }
}