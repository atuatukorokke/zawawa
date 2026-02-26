using UnityEngine;
using UnityEngine.UIElements;

public class GimmickBeamController : MonoBehaviour
{
    public GimmickBeamGenerator generator;

    [Header("伸縮設定")]
    public float stretchSpeed;
    public float maxScaleY;
    public float minScaleY;
    public float lastPositionX;
    public float lowPositionY;


    private bool stretching = true;

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
        }

        // 下端固定で上に伸びるように位置補正
        float delta = (scale.y - oldHeight) / 4f;
        transform.position += new Vector3(0, delta, 0);

        transform.localScale = scale;

        if (lastPositionX > transform.position.x)
        {
            generator.Revive();
            Destroy(gameObject);
            
        }

        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 🟦 Shield（盾）に当たったらビームが止まる
        if (collision.collider.CompareTag("Shield"))
        {
            // スケールをリセット
            Vector3 scale = transform.localScale;
            scale.y = minScaleY;
            transform.localScale = scale;
            // 左に1移動
            transform.position = new Vector3(transform.position.x - 1f, lowPositionY, transform.position.z);

            
            

            //現在のビームを消して次のビーム生成
            //generator.Revive();
            //Destroy(gameObject);
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
