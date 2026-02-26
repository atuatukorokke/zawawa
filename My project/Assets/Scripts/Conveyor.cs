using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField] private Vector2 moveDirection = Vector2.right;
    [SerializeField] private float moveSpeed = 2f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") &&
            !collision.CompareTag("Player")) return;

        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        Vector2 beltVel = moveDirection.normalized * moveSpeed;

        if (collision.CompareTag("Player"))
        {
            PlayerMove player = collision.GetComponent<PlayerMove>();
            player.conveyorVelocity = beltVel;
        }
        else
        {
            rb.linearVelocity = beltVel;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMove>().conveyorVelocity = Vector2.zero;
        }
    }
}