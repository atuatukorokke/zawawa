using UnityEngine;
using System;

public class EnemyDeathNotifier : MonoBehaviour
{
    public static event Action OnEnemySpawned;
    public static event Action OnEnemyDestroyed;

    private bool isQuitting = false;

    void OnApplicationQuit() 
    { 
        isQuitting = true; 
    }

    void Start() 
    { 
        OnEnemySpawned?.Invoke(); 
    }

    void OnDestroy()
    {
        // シーン切り替え中やアプリ終了中は通知しない
        if (isQuitting || !gameObject.scene.isLoaded) 
            return;

        // 敵が Destroy された瞬間に通知
        OnEnemyDestroyed?.Invoke();
    }
}
