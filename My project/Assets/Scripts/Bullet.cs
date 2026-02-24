using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Player‚É“–‚½‚Á‚½‚ç
        PlayerMove player = other.GetComponent<PlayerMove>();

        if (player != null)
        {
            
            Destroy(gameObject);
        }
    }
}