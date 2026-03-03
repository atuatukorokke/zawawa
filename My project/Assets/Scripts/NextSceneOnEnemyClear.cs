using UnityEngine;

public class NextSceneOnEnemyClear : MonoBehaviour
{
    int enemyCount = 0;
    bool initialized = false;

    [SerializeField] private CloseAnimation closeAnim;

    private void OnEnable()
    {
        EnemyDeathNotifier.OnEnemySpawned += OnEnemySpawned;
        EnemyDeathNotifier.OnEnemyDestroyed += OnEnemyDestroyed;
    }

    private void OnDisable()
    {
        EnemyDeathNotifier.OnEnemySpawned -= OnEnemySpawned;
        EnemyDeathNotifier.OnEnemyDestroyed -= OnEnemyDestroyed;
    }

    void Start()
    {
        initialized = true;
        Debug.Log("初期化完了"+ enemyCount);
    }

    void OnEnemySpawned()
    {
        enemyCount++;
        Debug.Log("敵出現 → 現在の敵数: " + enemyCount);
    }

    void OnEnemyDestroyed()
    {
        if (!initialized) return;

        enemyCount--;
        Debug.Log("敵撃破 → 現在の敵数: " + enemyCount);

        if (enemyCount <= 0)
        {
            Debug.Log("敵全滅！");
            initialized = false;
            closeAnim.PlayClose();
        }
    }
}