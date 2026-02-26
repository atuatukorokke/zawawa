using UnityEngine;

// このスクリプトは「弾」の動きを管理するもの
// ・まっすぐ飛ぶ
// ・盾に当たると加速する
// ・プレイヤーに当たるとダメージ
// ・敵に当たると敵を壊す
// ・撃った敵が消えたら弾も消える
public class Bullet : MonoBehaviour
{
    [Header("弾の基本スピード")]
    // 弾の初期速度
    [SerializeField] private float speed = 8f;

    [Header("盾に当たったときの加速量")]
    // 盾に当たるたびにどれだけ速くなるか
    [SerializeField] private float speedUpAmount = 2f;

    [Header("スピードの上限")]
    // 速くなりすぎないように制限
    [SerializeField] private float maxSpeed = 20f;

    // Rigidbody2D（物理で動かすために使う）
    private Rigidbody2D rb;

    // 最初に飛ぶ方向を覚えておく変数
    private Vector2 moveDirection;

    // この弾を撃ったオブジェクト（Enemyなど）
    // 撃った人が消えたら弾も消すために使う
    private GameObject owner;

    void Awake()
    {
        // Rigidbody2Dを取得
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 撃った人（Enemy）をセットする
    /// </summary>
    public void SetOwner(GameObject shooter)
    {
        owner = shooter;
    }

    /// <summary>
    /// 弾の飛ぶ方向を設定する
    /// </summary>
    public void SetDirection(Vector2 direction)
    {
        // 方向を1の長さにそろえる（normalized）
        moveDirection = direction.normalized;

        // 実際に速度を設定
        rb.linearVelocity = moveDirection * speed;

        // 見た目も進行方向に向ける
        RotateToDirection();
    }

    void Update()
    {
        // 撃った人が消えていたら弾も消える
        if (owner == null)
        {
            Destroy(gameObject);
            return;
        }

        // 常に進行方向に向きを合わせる
        if (rb.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(
                rb.linearVelocity.y,
                rb.linearVelocity.x
            ) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    /// <summary>
    /// 最初の方向に見た目を合わせる処理
    /// </summary>
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

    /// <summary>
    /// 何かにぶつかったときの処理
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        //// 🟦 Shield（盾）に当たったら加速する
        //if (collision.collider.CompareTag("Shield"))
        //{
        //    SpeedUp();
        //    return; // 他の処理はしない
        //}

        // 🔴 Playerに当たったらダメージ
        if (collision.collider.CompareTag("Player"))
        {
            PlayerMove player =
                collision.collider.GetComponent<PlayerMove>();

            if (player != null)
            {
                player.TakeDamage(1);
            }

            //Destroy(gameObject);
            return;
        }

        // 🟣 Enemyに当たったら敵を破壊
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.collider.gameObject);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// スピードを上げる処理
    /// </summary>
    void SpeedUp()
    {
        // 今のスピードに加速量を足す
        // ただし上限を超えないようにする
        speed = Mathf.Min(speed + speedUpAmount, maxSpeed);

        // 向きを変えずに速度だけ上げる
        rb.linearVelocity =
            rb.linearVelocity.normalized * speed;
    }
}