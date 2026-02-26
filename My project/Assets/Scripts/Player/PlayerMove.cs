using UnityEngine;

// このスクリプトは
// ・プレイヤーの移動
// ・回転
// ・HP管理
// ・コンベア（床に流される動き）
// ・ゲームオーバー処理
// をまとめて管理するもの
public class PlayerMove : MonoBehaviour
{
    [Header("移動速度")]
    // プレイヤーが自分の入力で動く速さ
    [SerializeField] private float moveSpeed = 5f;

    [Header("最大HP")]
    // プレイヤーの最大体力
    [SerializeField] private int maxHP = 5;

    [Header("回転の速さ")]
    // 左右キーでどれくらい速く回転するか
    [SerializeField] private float rotateSpeed = 180f;

    // 現在のHP（ゲーム中に変化する）
    private int currentHP;

    // 物理移動用
    private Rigidbody2D rb;

    // 入力方向を保存する
    private Vector2 moveInput;

    // コンベアなどの「強制移動」用の速度
    // 外部スクリプトから変更される想定
    public Vector2 conveyorVelocity = Vector2.zero;

    [Header("コンベア中の操作倍率")]
    // コンベアに乗っている時、
    // プレイヤー入力をどれくらい弱くするか
    [SerializeField] private float conveyorInputMultiplier = 0.5f;

    void Awake()
    {
        // Rigidbody取得
        rb = GetComponent<Rigidbody2D>();

        // 重力を0にする（上から落ちないように）
        rb.gravityScale = 0f;
    }

    void Start()
    {
        // ゲーム開始時にHPを最大にする
        currentHP = maxHP;
    }

    void Update()
    {
        // HPが0以下なら操作できない
        if (currentHP <= 0) return;

        // --- 移動入力の取得 ---
        moveInput = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) moveInput.y = 1;
        if (Input.GetKey(KeyCode.S)) moveInput.y = -1;
        if (Input.GetKey(KeyCode.D)) moveInput.x = 1;
        if (Input.GetKey(KeyCode.A)) moveInput.x = -1;

        // 斜め移動が速くなりすぎないように長さを1にそろえる
        moveInput = moveInput.normalized;

        // --- 回転処理 ---
        float rotateInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) rotateInput = 1;
        if (Input.GetKey(KeyCode.RightArrow)) rotateInput = -1;

        // Z軸回転（2D用）
        transform.Rotate(Vector3.forward * rotateInput * rotateSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // 死亡中は完全停止
        if (currentHP <= 0)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // 入力による速度
        Vector2 inputVelocity = moveInput * moveSpeed;

        // コンベアに乗っている場合は入力を弱める
        if (conveyorVelocity != Vector2.zero)
        {
            inputVelocity *= conveyorInputMultiplier;
        }

        // 最終的な速度 = コンベアの流れ + プレイヤー入力
        rb.linearVelocity = conveyorVelocity + inputVelocity;
    }

    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        Debug.Log("ダメージ！ 残りHP: " + currentHP);

        if (currentHP <= 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    void GameOver()
    {
        Debug.Log("プレイヤー死亡");

        // シーン内のGameOverManagerを探す
        GameOverManager manager = FindFirstObjectByType<GameOverManager>();

        if (manager != null)
        {
            manager.ShowGameOver();
        }
        else
        {
            Debug.LogWarning("GameOverManagerが見つかりません！");
        }
    }
}