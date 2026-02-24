using UnityEngine;

// 敵のテスト用スクリプト
// 敵が左右に行ったり来たりする動きを作る
public class Enemytest : MonoBehaviour
{
    [Header("移動の速さ")]
    // 数値を大きくすると、左右の動きが速くなる
    [SerializeField] private float moveSpeed = 2f;

    [Header("移動する幅")]
    // 数値を大きくすると、より大きく左右に動く
    [SerializeField] private float moveRange = 3f;

    // ゲーム開始時の位置を覚えておくための変数
    private Vector3 startPos;

    void Start()
    {
        // 最初の位置を保存しておく
        // この位置を基準に左右へ動く
        startPos = transform.position;
    }

    void Update()
    {
        // Time.time は「ゲームが始まってからの時間（秒）」
        // それを使ってなめらかな動きを作る

        // Mathf.Sin は -1 〜 1 の間を行ったり来たりする値を出す
        // それに moveRange をかけることで移動幅を調整している
        float x = Mathf.Sin(Time.time * moveSpeed) * moveRange;

        // 最初の位置 + 左右の動き で新しい位置を決める
        transform.position = startPos + new Vector3(x, 0, 0);
    }
}