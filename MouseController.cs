using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MouseController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    Rigidbody2D rb;
    Vector2 input;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input = Vector2.ClampMagnitude(input, 1f); // no diagonal boost
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * moveSpeed * Time.fixedDeltaTime);
    }

    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Cat"))
            GameManager.Instance.GameOver();
    }
}
