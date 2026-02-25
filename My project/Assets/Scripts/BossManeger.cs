using UnityEngine;

public class BossManeger : MonoBehaviour
{

    // 時間
    private float timer;
    private GameObject BeamBarrel;

    //リキャスト
    [SerializeField] private float fireInterval;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BeamBarrel.gameObject.SetActive(false); // 開始時非表示
    }

    // Update is called once per frame
    void Update()
    {
        // 時間を数える
        timer += Time.deltaTime;
        Debug.Log(timer);

        if (timer >= fireInterval)
        {
           
            for (float i = 0; i< 3; i++) 
            {
                this.gameObject.SetActive(true); //砲身の表示
                //Shot();
                Invoke("Shot",0.2f);
                
            }
            
            timer = 0f; // カウントのリセット
            fireInterval = Random.Range(10, 30);
            this.gameObject.SetActive(false); //砲身の非表示

            Debug.Log("asd");
        }
    }
}
