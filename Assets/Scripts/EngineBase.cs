using UnityEngine;

/// <summary>
/// EngineBase handles shared Rigidbody2D acceleration movement.
/// PlayerMovement and EnemyMovement use this instead of duplicating movement code.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EngineBase : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float acceleration = 5000f;

    private Rigidbody2D ourRigidbody;

    private void Awake()
    {
        ourRigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Applies force in the provided direction.
    /// </summary>
    public void Accelerate(Vector2 direction)
    {
        if (direction.magnitude == 0)
        {
            return;
        }

        Vector2 normalisedDirection = direction.normalized;
        Vector2 forceToAdd = normalisedDirection * acceleration * Time.deltaTime;

        ourRigidbody.AddForce(forceToAdd);
    }
}