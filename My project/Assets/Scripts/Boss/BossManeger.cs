using UnityEngine;

public class BossManeger : MonoBehaviour
{

    // 時間
    private float timer;
    //砲身の表示用
    private GameObject BeamBarrel;
    //ビームの弾
    [SerializeField] private GameObject BeamPrefab;
    //弾速
    [SerializeField] private float bulletSpeed = 5f;
    //発射位置
    [SerializeField] private Transform firePoint;
    [SerializeField] private float FiringInterval = 0.5f;
    //リキャスト
    [SerializeField] private float fireInterval = 3.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0f;
        //BeamBarrel.gameObject.SetActive(true); // 開始時非表示
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
            Invoke("Shoot", FiringInterval);
            //CancelInvoke();

            timer = 0f; // カウントのリセット
            //fireInterval = Random.Range(10, 30);
            //BeamBarrel.gameObject.SetActive(false); //砲身の非表示

            //Debug.Log("asd");
        }
    }

    void Shoot() 
    {
        //BeamBarrel.gameObject.SetActive(true); //砲身の表示
        
            if (BeamPrefab == null)
            {
                Debug.Log("BeamPrefabが設定されていません。");
                return;
            }
        if (firePoint == null)
        {
            Debug.Log("firePoint が設定されていません。");
            return;
        }

        Debug.Log("Beam");
            // 弾を生成
            GameObject bullet = Instantiate(BeamPrefab, firePoint.position, firePoint.rotation);
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
