using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("弾のプレハブ")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("何秒ごとに撃つか")]
    [SerializeField] private float fireInterval = 2f;

    [Header("何秒ごとに向きを更新するか")]
    [SerializeField] private float rotateInterval = 0.1f;

    [Header("弾のスピード")]
    [SerializeField] private float bulletSpeed = 5f;

    private float fireTimer;
    private float rotateTimer;

    private GameObject player;

    void Start()
    {
        // 毎回Findしないように一度だけ取得（軽量化）
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null) return;

        fireTimer += Time.deltaTime;
        rotateTimer += Time.deltaTime;

        // ⭐ 向きだけ0.1秒ごとに更新
        if (rotateTimer >= rotateInterval)
        {
            RotateToPlayer();
            rotateTimer = 0f;
        }

        // ⭐ 弾は2秒ごとに発射
        if (fireTimer >= fireInterval)
        {
            Shoot();
            fireTimer = 0f;
        }
    }

    void RotateToPlayer()
    {
        Vector2 direction =
            (player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        Vector2 direction =
            (player.transform.position - transform.position).normalized;

        Vector2 spawnPos =
            (Vector2)transform.position + direction * 1.2f;

        GameObject bullet = Instantiate(
            bulletPrefab,
            spawnPos,
            Quaternion.identity
        );

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetOwner(gameObject);
        }
    }
}