using UnityEngine;

// プレイヤーに向かって弾を撃つ敵（追尾なし）
public class EnemyShooter : MonoBehaviour
{
    [Header("弾のプレハブ")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("何秒ごとに撃つか")]
    [SerializeField] private float fireInterval = 2f;

    [Header("弾のスピード")]
    [SerializeField] private float bulletSpeed = 5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        // 🔥 Playerタグのオブジェクトを取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null) return; // プレイヤーがいなければ撃たない

        // 🔥 プレイヤー方向を計算
        Vector2 direction =
            (player.transform.position - transform.position).normalized;

        // 少し前にずらして生成
        Vector2 spawnPos =
            (Vector2)transform.position + direction * 1.2f;

        GameObject bullet = Instantiate(
            bulletPrefab,
            spawnPos,
            Quaternion.identity
        );

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;

        // 所有者登録（必要なら）
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetOwner(gameObject);
        }
    }
}