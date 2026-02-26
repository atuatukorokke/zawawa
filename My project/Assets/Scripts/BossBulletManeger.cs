using UnityEngine;

public class BossBulletManeger : MonoBehaviour
{
    // 時間
    private float timer;
    //弾
    [SerializeField] private GameObject Bullet;
    //弾速
    [SerializeField] private float bulletSpeed = 5f;
    //発射位置
    [SerializeField] private Transform firePoint;
    //リキャスト
    [SerializeField] private float fireInterval = 1.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 時間を数える
        timer += Time.deltaTime;
        //Debug.Log(timer);
        if (timer >= fireInterval)
        {
            Debug.Log("Shoot");
            if (Bullet == null || firePoint == null)
            {
                Debug.Log("bulletPrefab または firePoint が設定されていません。");
                return;
            }
            timer = 0;
            shoot();
            // 弾を生成
            
        }
    }

    private void shoot()
    {
            GameObject bullet = Instantiate(Bullet, firePoint.position, this.transform.rotation);
            // Rigidbody2D を取得して速度を設定
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // firePoint の向き（右方向）に速度を与える
                rb.linearVelocity = firePoint.right * bulletSpeed;
            }
            else if (rb == null) Debug.Log("No Rigidbody");
    }
}
