using UnityEngine;

public class BossHP : MonoBehaviour
{
    [SerializeField] private float BossHels = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BossHels <= 0 ) 
        {
            //自分殺し
            Debug.Log("Boss Destroy");
            Destroy(gameObject);
        }
        if (Input.GetKey(KeyCode.F)) Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))BossHels =- 1;
        if (collision.gameObject) BossHels -= 1;
        Debug.Log(BossHels);
    }
}
