using UnityEngine;
using System.Collections.Generic;

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
        if (other.CompareTag("Enemy")) 
        {
            Destroy(other.gameObject); // kill enemy
            Destroy(gameObject);       // destroy bullet
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}