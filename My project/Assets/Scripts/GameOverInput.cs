using UnityEngine;

public class GameOverInput : MonoBehaviour
{
    [SerializeField] private RetryManager retryManager;
    [SerializeField] private StartManager startManager; // スタートへ戻る処理用

    void Update()
    {
        // パネルが表示中だけ反応させたいなら
        if (!gameObject.activeInHierarchy) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            retryManager.Retry();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            startManager.GoToStart();
        }
    }
}