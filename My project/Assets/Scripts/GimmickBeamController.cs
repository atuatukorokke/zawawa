using UnityEngine;

public class GimmickBeamController : MonoBehaviour
{
    public GimmickBeamGenerator generator;

    [Header("伸縮設定")]
    public float stretchSpeed;
    public float maxScaleY;
    public float minScaleY;
    public float lastPositionX;

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
            // 左に1移動
            transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);

            // スケールをリセット
            scale.y = minScaleY;

            // 再び伸びるフェーズへ
            stretching = true;
        }

        // 下端固定で上に伸びるように位置補正
        float delta = (scale.y - oldHeight) / 2f;
        transform.position += new Vector3(0, delta, 0);

        transform.localScale = scale;

        if (lastPositionX > transform.position.x)
        {
            generator.Revive();
            Destroy(gameObject);
            
        }
    }

    
}
