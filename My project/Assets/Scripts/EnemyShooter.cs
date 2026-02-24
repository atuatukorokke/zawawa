using UnityEngine;

// このスクリプトは
// 「一定時間ごとに弾を撃つ敵」を作るもの
public class EnemyShooter : MonoBehaviour
{
    [Header("弾のプレハブ")]
    // Projectビューにある弾をここにセットする
    [SerializeField] private GameObject bulletPrefab;

    [Header("何秒ごとに撃つか")]
    // 例：2にすると2秒に1回撃つ
    [SerializeField] private float fireInterval = 2f;

    [Header("弾のスピード")]
    // 弾が飛ぶ速さ
    [SerializeField] private float bulletSpeed = 5f;

    [Header("発射角度（度数）")]
    // 0 = 右
    // 90 = 上
    // 180 = 左
    // -90 = 下
    [SerializeField] private float fireAngle = 0f;

    // 時間を数えるための変数
    private float timer;

    void Update()
    {
        // 毎フレーム時間を足していく
        timer += Time.deltaTime;

        // 指定した時間を超えたら弾を撃つ
        if (timer >= fireInterval)
        {
            Shoot();
            timer = 0f; // タイマーをリセット
        }
    }

    /// <summary>
    /// 弾を発射する処理
    /// </summary>
    void Shoot()
    {
        // 度（°）をラジアンに変換
        // SinやCosはラジアンを使うため
        float rad = fireAngle * Mathf.Deg2Rad;

        // 角度から「方向ベクトル」を作る
        // Cos → X方向
        // Sin → Y方向
        Vector2 direction = new Vector2(
            Mathf.Cos(rad),
            Mathf.Sin(rad)
        );

        // 敵の中心から少し前にずらして弾を生成
        // これをしないと自分に当たることがある
        Vector2 spawnPos =
            (Vector2)transform.position + direction * 0.6f;

        // 弾を生成する（Instantiate = 複製して出す）
        GameObject bullet = Instantiate(
            bulletPrefab,
            spawnPos,
            Quaternion.identity
        );

        // Rigidbody2Dを取得して速度を設定
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // 方向 × スピード = 実際の移動速度
        rb.linearVelocity = direction.normalized * bulletSpeed;

        // この弾を撃ったのは「自分」だと登録する
        // （撃った敵が消えたら弾も消すため）
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetOwner(gameObject);
    }
}