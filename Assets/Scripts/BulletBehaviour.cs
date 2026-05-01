using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Handles player bullet movement, enemy collision, and cleanup when off-screen.
/// </summary>

public class BulletBehaviour : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;


    private Rigidbody2D rb;

    void Start()
    {
        // Sets the bullet's speed value on initialization
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    // Triggers on collision
    void OnTriggerEnter2D(Collider2D other)
    {
        // Checks if the other object has the 'Enemy' tag
        if (other.CompareTag("Enemy")) 
        {
            Destroy(other.gameObject); // Destroys enemy
            Destroy(gameObject);       // Destroys bullet
        }
    }


    // Called when object is no longer visible on any camera
    void OnBecameInvisible()
    {
        // Destroys bullet
        Destroy(gameObject);
    }
}