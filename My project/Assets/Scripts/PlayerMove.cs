using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("最大HP")]
    [SerializeField] private int maxHP = 5;

    [Header("回転の速さ")]
    [SerializeField] private float rotateSpeed = 180f;

    [Header("ゲームオーバーパネル")]
    [SerializeField] private GameObject gameOverPanel; // ←追加

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

        // ゲーム開始時は非表示
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (currentHP <= 0) return; // 死亡後は操作不可

        moveInput = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) moveInput.y = 1;
        if (Input.GetKey(KeyCode.S)) moveInput.y = -1;
        if (Input.GetKey(KeyCode.D)) moveInput.x = 1;
        if (Input.GetKey(KeyCode.A)) moveInput.x = -1;

        moveInput = moveInput.normalized;

        float rotateInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) rotateInput = 1;
        if (Input.GetKey(KeyCode.RightArrow)) rotateInput = -1;

        transform.Rotate(Vector3.forward * rotateInput * rotateSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (currentHP <= 0)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log("ダメージ！ 残りHP: " + currentHP);

        if (currentHP <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("プレイヤー死亡");

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // 必要なら時間停止
        Time.timeScale = 0f;
    }
}