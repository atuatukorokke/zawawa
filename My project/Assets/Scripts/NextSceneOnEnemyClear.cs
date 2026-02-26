using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneOnEnemyClear : MonoBehaviour
{
    int enemyCount;

    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        EnemyDeathNotifier.OnEnemyDestroyed += OnEnemyDestroyed;
    }

    void OnDestroy()
    {
        EnemyDeathNotifier.OnEnemyDestroyed -= OnEnemyDestroyed;
    }

    void OnEnemyDestroyed()
    {
        enemyCount--;
        Debug.Log("敵が倒れた通知を受け取った！ 残り: " + enemyCount);

        if (enemyCount <= 0)
        {
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
