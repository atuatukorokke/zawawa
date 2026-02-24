using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("最大HP")]
    [SerializeField] private int maxHP = 5;

    private int currentHP;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
    // 🔥 Bulletタグに当たったらダメージ
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("衝突した");
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }
    // 🔥 外部から呼ばれるダメージ処理
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log("ダメージ！ 残りHP: " + currentHP);

        if (currentHP <= 0)
        {
            Debug.Log("プレイヤー死亡");
            Destroy(gameObject);
        }
    }
    

}