using UnityEngine;

// このスクリプトは
// 敵が「円を描くように」ぐるぐる動く動きを作るもの
public class Enemytest2 : MonoBehaviour
{
    [Header("円の大きさ（半径）")]
    // 円の大きさを決める
    // 数値が大きいほど大きな円を描く
    [SerializeField] private float radius = 3f;

    [Header("回転の速さ")]
    // どれくらいの速さで回るか
    // 数値が大きいほど速く回る
    [SerializeField] private float rotateSpeed = 2f;

    // 円の中心になる位置を保存する変数
    private Vector3 center;

    void Start()
    {
        // ゲーム開始時の「今いる場所」を中心として保存する
        // これを基準に円運動する
        center = transform.position;
    }

    void Update()
    {
        // Time.time は「ゲームが始まってからの時間（秒）」
        // それに回転速度をかけることで回転の速さを調整している

        // CosはX方向の動き
        float x = Mathf.Cos(Time.time * rotateSpeed) * radius;

        // SinはY方向の動き
        float y = Mathf.Sin(Time.time * rotateSpeed) * radius;

        // 中心 + (x, y) で円の位置を作る
        transform.position = center + new Vector3(x, y, 0);
    }
}