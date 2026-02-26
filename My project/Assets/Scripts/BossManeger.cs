using UnityEngine;

public class BossManeger : MonoBehaviour
{

    // 時間
    private float timer;
    //砲身
    private GameObject BeamBarrel;
    //ビームの弾
     private GameObject Bullet;
    //弾速
    [SerializeField] private float bulletSpeed = 5f;
    //発射位置
     private Transform firePoint;
    //リキャスト
    [SerializeField] private float fireInterval = 3.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0f;
        //BeamBarrel.gameObject.SetActive(false); // 開始時非表示
        //InvokeRepeating(nameof(Shoot), 3.5f, 1.0f);
        //Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        // 時間を数える
        timer += Time.deltaTime;
        //Debug.Log(timer);

        if (timer >= fireInterval)
        {
            //BeamBarrel.gameObject.SetActive(true);
            //Debug.Log("Shoot");
            Shoot();      //弾を撃つ
            Invoke("Shoot",0.5f);
            //CancelInvoke();

            timer = 0f; // カウントのリセット
            //fireInterval = Random.Range(10, 30);
            //BeamBarrel.gameObject.SetActive(false); //砲身の非表示

            Debug.Log("asd");
        }
    }

    void Shoot() 
    {
        //BeamBarrel.gameObject.SetActive(true); //砲身の表示
        
            if (Bullet == null || firePoint == null)
            {
                Debug.Log("bulletPrefab または firePoint が設定されていません。");
                return;
            }

            // 弾を生成
            GameObject bullet = Instantiate(Bullet, firePoint.position, firePoint.rotation);
            // Rigidbody2D を取得して速度を設定
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // firePoint の向き（右方向）に速度を与える
                rb.linearVelocity = firePoint.up * bulletSpeed;
            }
            else if (rb == null) Debug.Log("No Rigidbody");
        
    }
}
