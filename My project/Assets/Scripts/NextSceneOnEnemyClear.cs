using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneOnEnemyClear : MonoBehaviour
{
    int enemyCount;
    bool initialized = false;

    private void OnEnable()
    {
        EnemyDeathNotifier.OnEnemySpawned += OnEnemySpawned;
        EnemyDeathNotifier.OnEnemyDestroyed += OnEnemyDestroyed;
    }

    private void OnDisable()
    {
        EnemyDeathNotifier.OnEnemyDestroyed -= OnEnemyDestroyed;
    }

    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        initialized = true;
        Debug.Log("初期敵数: " + enemyCount);
    }

    void OnEnemySpawned() 
    { 
        enemyCount++; Debug.Log("敵が出現！ 現在の敵数: " + enemyCount); 
    }

    void OnEnemyDestroyed()
    {
        // まだ初期化前 or もう使わないタイミングなら無視
        if (!initialized) 
        { 
            Debug.LogWarning("初期化前/無効状態で OnEnemyDestroyed が呼ばれたので無視しました"); 
            return; 
        }

        if (enemyCount <= 0)
        {
            Debug.LogWarning("enemyCount が 0 以下。初期化が正しく行われていない可能性があります。");
            return;
        }

        enemyCount--;
        Debug.Log("敵が倒れた通知を受け取った！ 残り: " + enemyCount);

        if (enemyCount == 0)
        {
            // これ以降の通知は無視したいので無効化
            initialized = false;
            LoadNextScene();
        }
    }


    void LoadNextScene()
    {
        Debug.Log("LoadNextScene()が呼ばれた！");

        //現在のシーン名を取得
        string currentScene = SceneManager.GetActiveScene().name;

        string nextSceneName = "";

        //PlayerScene 1 の場合 → PlayerScene 2 へ
        if (currentScene == "PlayerScene 1")
        {
            nextSceneName = "PlayerScene 2";
        }
        //PlayerScene 2 の場合 → ClearTestScene へ
        else if (currentScene == "PlayerScene 2")
        {
            nextSceneName = "ClearTestScene";
        }
        else
        {
            Debug.LogError("このシーンからの遷移先が設定されていません: " + currentScene);
            return;
        }

        // シーンが Build Profiles に登録されているか確認
        if (Application.CanStreamedLevelBeLoaded(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError($"シーン '{nextSceneName}' が Build Profiles に登録されていません");
        }
    }
}
