using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("最大HP")]
    [SerializeField] private int maxHP = 5;

    [Header("回転力")]
    [SerializeField] float rotateSpeed = 180f;

    private int currentHP; // プレイヤーのHP
    private Rigidbody2D rb; // Rigidbody2Dコンポーネントへの参照
    private Vector2 moveInput; // プレイヤーの移動入力を格納する変数

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; 
    }
    // 🔥 StartでHPを初期化
    void Start()
    {
        currentHP = maxHP;
    }
    // 🔥 Updateで移動入力を取得
    void Update()
    {
        // ここではWASDキーで移動入力を取得
        moveInput = Vector2.zero;
        // 矢印キーで回転するためにインプットキーコードに変更
        if (Input.GetKey(KeyCode.W)) moveInput.y = 1;
        if (Input.GetKey(KeyCode.S)) moveInput.y = -1;
        if (Input.GetKey(KeyCode.D)) moveInput.x = 1;
        if (Input.GetKey(KeyCode.A)) moveInput.x = -1;
        moveInput = moveInput.normalized;

        // ---------------- 回転処理 ---------------
        float rotateInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) rotateInput = 1;
        if (Input.GetKey(KeyCode.RightArrow)) rotateInput = -1;

        transform.Rotate(Vector3.forward * rotateInput * rotateSpeed * Time.deltaTime);
    }


    // 🔥 FixedUpdateで物理演算を使って移動
    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
    /// <summary>
    /// プレイヤーが衝突したときの処理
    /// </summary>
    /// <param name="other"></param>
    // 🔥 Bulletタグに当たったらダメージ
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("衝突した");
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }
    /// <summary>
    /// プレイヤーがダメージを受ける処理
    /// </summary>
    /// <param name="damage"></param>
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