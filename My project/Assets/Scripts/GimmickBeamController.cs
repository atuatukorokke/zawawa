using UnityEngine;
using UnityEngine.UIElements;

public class GimmickBeamController : MonoBehaviour
{
    public GimmickBeamGenerator beamGenerator;

    [Header("伸縮設定")]
    public float stretchSpeed;
    public float maxScaleY;
    public float minScaleY;
    public float lastPositionX;
    public float lowPositionY;


    private bool stretching = true;
    private bool skipDelta = false;


    void Start()
    {
        transform.localScale = new Vector3(1f, minScaleY, 1f);
    }

    

    void Update()
    {
        Vector3 scale = transform.localScale;
        float oldHeight = scale.y;

        if (stretching)
        {
            // 伸びる
            scale.y += stretchSpeed * Time.deltaTime;

            // 最大に達したら縮むフェーズへ
            if (scale.y >= maxScaleY)
            {
                stretching = false;
            }
        }
        else
        {
            // スケールをリセット
            scale.y = minScaleY;
            // 左に1移動
            transform.position = new Vector3(transform.position.x - 1f, lowPositionY, transform.position.z);

            

            // 再び伸びるフェーズへ
            stretching = true;

            skipDelta = true;  // ★ このフレームは位置補正しない
        }

        // 下端固定で上に伸びるように位置補正
        if (!skipDelta)
        {
            float delta = (scale.y - oldHeight) / 2f;
            transform.position += new Vector3(0, delta, 0);
        }

        skipDelta = false; // 次のフレームからは通常処理


        transform.localScale = scale;

        if (lastPositionX > transform.position.x)
        {
            Destroy(gameObject);
            beamGenerator.BeamRevive();
        }

        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 🟦 Shield（盾）に当たったらビームが止まる
        if (collision.collider.CompareTag("Shield"))
        {
            Vector3 scale = transform.localScale;
            scale.y = minScaleY;
            transform.localScale = scale;

            transform.position = new Vector3(transform.position.x - 1f, lowPositionY, transform.position.z);

            stretching = true;

            skipDelta = true;  // ★ このフレームは位置補正しない
        }


        if (collision.collider.CompareTag("Player"))
        {
            PlayerMove player =
                collision.collider.GetComponent<PlayerMove>();

            if (player != null)
            {
                player.TakeDamage(1);
            }

            Destroy(gameObject);
            return;
        }
    }
}
