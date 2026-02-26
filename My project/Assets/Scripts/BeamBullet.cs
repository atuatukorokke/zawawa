using UnityEngine;

public class BeamBullet : MonoBehaviour
{
    //時間
    private float timer;
    [SerializeField]private float DeleteTime = 6.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Rigidbody2D rb = this.GetComponent<Rigidbody2D>();  // rigidbodyを取得
        //Vector3 force = new Vector3(0.0f, 0.0f, 1.0f);    // 力を設定
        //rb.AddForce(force);  // 力を加える

    }

    // Update is called once per frame
    void Update()
    {
        // 時間を数える
        timer += Time.deltaTime;
        if (timer > DeleteTime) 
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        //if(collision.gameObject.CompareTag())
        if (collision.gameObject.name == "Boss") 
        {
            Destroy(gameObject);
        }
        
    }
}

