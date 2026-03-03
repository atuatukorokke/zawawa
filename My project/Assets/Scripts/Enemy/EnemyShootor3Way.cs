using UnityEngine;

[RequireComponent(typeof(EnemyDeathNotifier))] // ⭐ これ追加
public class EnemyShooter3Way : MonoBehaviour
{
    [Header("弾のプレハブ")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("発射間隔（秒）")]
    [SerializeField] private float fireInterval = 2f;

    [Header("向き更新間隔")]
    [SerializeField] private float rotateInterval = 0.1f;

    [Header("弾のスピード")]
    [SerializeField] private float bulletSpeed = 5f;

    [Header("発射位置オフセット")]
    [SerializeField] private float spawnOffset = 1.2f;

    [Header("3Wayの角度")]
    [SerializeField] private float angleOffset = 45f;

    private float fireTimer;
    private float rotateTimer;

    private Transform player;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        fireTimer += Time.deltaTime;
        rotateTimer += Time.deltaTime;

        if (rotateTimer >= rotateInterval)
        {
            RotateToPlayer();
            rotateTimer = 0f;
        }

        if (fireTimer >= fireInterval)
        {
            Shoot3Way();
            fireTimer = 0f;
        }
    }

    void RotateToPlayer()
    {
        Vector2 direction =
            (player.position - transform.position).normalized;

        float angle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler(0, 0, angle);
    }

    void Shoot3Way()
    {
        Vector2 baseDir =
            (player.position - transform.position).normalized;

        float[] angles = { 0f, angleOffset, -angleOffset };

        foreach (float angle in angles)
        {
            Vector2 dir = Rotate(baseDir, angle);

            Vector2 spawnPos =
                (Vector2)transform.position + dir * spawnOffset;

            GameObject bullet = Instantiate(
                bulletPrefab,
                spawnPos,
                Quaternion.identity
            );

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = dir * bulletSpeed;
            }

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetOwner(gameObject);
            }
        }
    }

    Vector2 Rotate(Vector2 v, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }
}