using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CatAI : MonoBehaviour
{
    [Header("Target")]
    public Transform mouse;        // drag the Mouse object here in Inspector

    [Header("Speed")]
    public float baseSpeed = 2f;   // starting speed
    public float speedGainPerSec = 0.15f; // how quickly it ramps up

    Rigidbody2D rb;
    float currentSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        // scale speed as time passes
        currentSpeed += speedGainPerSec * Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (mouse == null) return;

        Vector2 dir = ((Vector2)mouse.position - rb.position).normalized;
        rb.MovePosition(rb.position + dir * currentSpeed * Time.fixedDeltaTime);
    }

    public void AddSpeedModifier(float delta)   => currentSpeed = Mathf.Max(0f, currentSpeed + delta);
    public void MultiplySpeed(float factor)      => currentSpeed = Mathf.Max(0f, currentSpeed * factor);
    public float GetCurrentSpeed()               => currentSpeed;
}
