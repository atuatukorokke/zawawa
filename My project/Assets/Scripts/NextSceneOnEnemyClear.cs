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
    }

    void OnEnemySpawned()
    {
        enemyCount++;
    }

    void OnEnemyDestroyed()
    {
        if (!initialized) return;

        enemyCount--;

        if (enemyCount <= 0)
        {
            initialized = false;
            closeAnim.PlayClose();
        }
    }
}