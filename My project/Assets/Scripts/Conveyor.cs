using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField] private Vector2 moveDirection = Vector2.right;
    [SerializeField] private float moveSpeed = 2f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Enemy")) return;

        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        rb.linearVelocity = moveDirection.normalized * moveSpeed;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Enemy")) return;

        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        rb.linearVelocity = Vector2.zero;
    }
}