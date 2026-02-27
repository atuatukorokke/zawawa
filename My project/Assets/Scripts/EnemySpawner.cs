using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // シーン上の敵（非アクティブ）
    public GameObject warningObject;   // 警告アイコン（シーン上の実体）
    public float warningTime = 1.5f;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    System.Collections.IEnumerator SpawnEnemy()
    {
        // 警告アイコンを表示
        warningObject.SetActive(true);

        // 警告時間だけ待つ
        yield return new WaitForSeconds(warningTime);

        // 敵を出現させる
        enemyPrefab.SetActive(true);

        // 敵が出たら警告を消す
        warningObject.SetActive(false);
    }
}
