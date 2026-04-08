using UnityEngine;

public class EnemyMovementAndRotation : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float acceleration = 75f;
    [SerializeField]
    private float initialVelocity = 2f;

    [Header("Rotation")]
    [SerializeField]
    private float maximumSpinSpeed = 200f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Initial downward movement
        rb.velocity = Vector2.down * initialVelocity;

        // Random spin
        rb.angularVelocity = Random.Range(-maximumSpinSpeed, maximumSpinSpeed);
    }

    void Update()
    {
        // Apply continuous downward acceleration
        Vector2 forceToAdd = Vector2.down * acceleration * Time.deltaTime;
        rb.AddForce(forceToAdd);
    }
}