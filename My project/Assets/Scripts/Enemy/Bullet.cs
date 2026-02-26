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
            Reflect();
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

    void Reflect()
    {
        if (!isReflected)
        {
            isReflected = true;

            Vector2 newDir = -rb.linearVelocity.normalized;
            speed = Mathf.Min(speed + speedUpAmount, maxSpeed);
            rb.linearVelocity = newDir * speed;

            // 🔥 色を変える
            sr.color = Color.cyan; // 好きな色に変えてOK
        }
    }
}