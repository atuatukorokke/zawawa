using UnityEngine;

public class BeamShot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    //ビームの弾
    private GameObject BeamPrefab;
    
    //砲身
    private GameObject BeamBarrel;

    //ビームの出てくる位置
    private Transform BeamBarrelPos;

    // 弾速
    [SerializeField] private float bulletSpeed = 5f;

    // 時間
    private float timer;
    
    //リキャスト
    private float fireInterval;

    private SpriteRenderer spriteRenderer;
    void Start()
    {

        this.gameObject.SetActive(false); // 開始時非表示

        //fireInterval = Random.Range(1, 3);
        fireInterval = 3f;

        Debug.Log("a");
    }

    // Update is called once per frame
    void Update()
    {
        // 時間を数える
        timer += Time.deltaTime;
        Debug.Log(timer);
        Debug.Log("b");


        // 規定時間になったら
        if (timer >= fireInterval)
        {
           
            for (float i = 0; i< 3; i++) 
            {
                this.gameObject.SetActive(true); //砲身の表示
                Shot();
                Invoke("Shot",0.2f);
                
            }
            
            timer = 0f; // カウントのリセット
            fireInterval = Random.Range(10, 30);
            this.gameObject.SetActive(false); //砲身の非表示

            Debug.Log("asd");
        }
    }

    void Shot()
    {

        // �e�𐶐�
        BeamPrefab = Instantiate(BeamPrefab, BeamBarrelPos.position, Quaternion.identity);

        // Rigidbody2D ���擾
        Rigidbody2D rb = BeamPrefab.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        // �����ɉ����Ĕ��˕���������
        Vector2 direction = spriteRenderer.flipX ? Vector2.left : Vector2.right;

        // 速度を与える
        rb.linearVelocity = direction * bulletSpeed;

    }
}
