using UnityEngine;

[RequireComponent(typeof(EnemyDeathNotifier))] // ⭐ これ追加
public class EnemyShooter : MonoBehaviour
{
    [Header("弾のプレハブ")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("何秒ごとに撃つか")]
    [SerializeField] private float fireInterval = 2f;

    [Header("何秒ごとに向きを更新するか")]
    [SerializeField] private float rotateInterval = 0.1f;

    [Header("発射位置オフセット距離")]
    [SerializeField] private float shootOffset = 1.2f;

    [SerializeField] private GameObject muzzleFlashPrefab;

    private float fireTimer;
    private float rotateTimer;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            (Vector2)transform.position + direction * shootOffset;

        GameObject bullet = Instantiate(
            bulletPrefab,
            spawnPos,
            Quaternion.identity
        );

        if (muzzleFlashPrefab != null)
        {
            Instantiate(
                muzzleFlashPrefab,
                spawnPos,
                transform.rotation
            );
        }

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
            bulletScript.SetOwner(gameObject);
        }
    }
}