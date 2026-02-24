using UnityEngine;

// このスクリプトは
// プレイヤーの「移動」「回転」「HP管理」をするもの
public class PlayerMove : MonoBehaviour
{
    [Header("移動速度")]
    // プレイヤーがどれくらいの速さで動くか
    [SerializeField] private float moveSpeed = 5f;

    [Header("最大HP")]
    // プレイヤーの最大体力
    [SerializeField] private int maxHP = 5;

    [Header("回転の速さ")]
    // 左右キーでどれくらい速く回転するか
    [SerializeField] private float rotateSpeed = 180f;

    // 現在のHP（ゲーム中に変化する）
    private int currentHP;

    // Rigidbody2D（物理で動かすために使う）
    private Rigidbody2D rb;

    // 入力された移動方向を入れる変数
    private Vector2 moveInput;

    void Awake()
    {
        // Rigidbody2Dを取得する
        rb = GetComponent<Rigidbody2D>();

        // 重力を0にする（上から落ちないようにする）
        rb.gravityScale = 0f;
    }

    void Start()
    {
        // ゲーム開始時にHPを最大値にする
        currentHP = maxHP;
    }

    void Update()
    {
        // ---- 移動入力の取得 ----

        // 毎フレームいったん0にする
        moveInput = Vector2.zero;

        // WASDキーで上下左右に移動
        if (Input.GetKey(KeyCode.W)) moveInput.y = 1;
        if (Input.GetKey(KeyCode.S)) moveInput.y = -1;
        if (Input.GetKey(KeyCode.D)) moveInput.x = 1;
        if (Input.GetKey(KeyCode.A)) moveInput.x = -1;

        // 斜め移動が速くなりすぎないように長さを1にそろえる
        moveInput = moveInput.normalized;

        // ---- 回転処理 ----

        float rotateInput = 0f;

        // 左矢印で左回転
        if (Input.GetKey(KeyCode.LeftArrow)) rotateInput = 1;

        // 右矢印で右回転
        if (Input.GetKey(KeyCode.RightArrow)) rotateInput = -1;

        // 実際に回転させる
        // Vector3.forward は「Z軸方向」
        transform.Rotate(Vector3.forward * rotateInput * rotateSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // 物理演算のタイミングで移動させる
        // 入力方向 × スピード = 実際の移動速度
        rb.linearVelocity = moveInput * moveSpeed;
    }

    /// <summary>
    /// プレイヤーがダメージを受ける処理
    /// 他のスクリプト（弾など）から呼ばれる
    /// </summary>
    public void TakeDamage(int damage)
    {
        // HPを減らす
        currentHP -= damage;

        Debug.Log("ダメージ！ 残りHP: " + currentHP);

        // HPが0以下になったら死亡
        if (currentHP <= 0)
        {
            Debug.Log("プレイヤー死亡");
            Destroy(gameObject);
        }
    }
}