using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("弾の基本スピード")]
    [SerializeField] private float speed = 8f;

    [Header("盾に当たったときの加速量")]
    [SerializeField] private float speedUpAmount = 2f;

    [Header("スピードの上限")]
    [SerializeField] private float maxSpeed = 20f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private GameObject owner;
    private SpriteRenderer sr;

    // ★ 追加：反射したかどうか
    public bool isReflected = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetOwner(GameObject shooter)
    {
        owner = shooter;
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
        rb.linearVelocity = moveDirection * speed;
        RotateToDirection();
    }

    void Update()
    {
        if (owner == null)
        {
            Destroy(gameObject);
            return;
        }

        if (rb.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(
                rb.linearVelocity.y,
                rb.linearVelocity.x
            ) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void RotateToDirection()
    {
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(
                moveDirection.y,
                moveDirection.x
            ) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 🛡 Shieldに当たったら反射
        if (collision.collider.CompareTag("Shield"))
        {
            Reflect(collision);
            return;
        }

        // 🔴 Playerに当たったらダメージ
        if (collision.collider.CompareTag("Player"))
        {
            PlayerMove player =
                collision.collider.GetComponent<PlayerMove>();

            if (player != null)
            {
                player.TakeDamage(1);
            }

            return;
        }

        // 💥 Enemyに当たったら（反射済みのみ）
        if (collision.collider.CompareTag("Enemy"))
        {
            if (isReflected)
            {
                Debug.Log("反射弾で敵を破壊！");
                Destroy(collision.collider.gameObject);
                Destroy(gameObject);
            }
        }
    }

    void Reflect(Collision2D collision)
    {
        if (!isReflected)
        {
            isReflected = true;

            // 衝突面の法線を取得
            Vector2 normal = collision.contacts[0].normal;

            // 入射ベクトルから正しく反射方向を計算
            Vector2 newDir = Vector2.Reflect(
                rb.linearVelocity.normalized,
                normal
            );

            // スピードだけ管理（プレイヤー速度は使わない）
            speed = Mathf.Min(speed + speedUpAmount, maxSpeed);

            rb.linearVelocity = newDir * speed;

            sr.color = Color.cyan;
        }
    }
}