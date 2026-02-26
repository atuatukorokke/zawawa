using UnityEngine;
using System;

public class EnemyDeathNotifier : MonoBehaviour
{
    public static event Action OnEnemyDestroyed;

    void OnDestroy()
    {
        // 敵が Destroy された瞬間に通知
        OnEnemyDestroyed?.Invoke();
    }
}
