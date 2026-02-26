using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("出現させる敵オブジェクト（Prefabではなく実体）")]
    public GameObject enemyObject;

    [Header("警告アイコンのプレハブ or オブジェクト")]
    public GameObject warningPrefab;

    [Header("警告を表示する時間")]
    public float warningTime = 1.5f;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    System.Collections.IEnumerator SpawnEnemy()
    {
        // 警告アイコンを出す
        GameObject warning = Instantiate(warningPrefab, transform.position, Quaternion.identity);

        // 警告時間だけ待つ
        yield return new WaitForSeconds(warningTime);

        // 警告を消す
        Destroy(warning);

        // 敵を出現させる（アクティブ化）
        enemyObject.SetActive(true);
    }
}
