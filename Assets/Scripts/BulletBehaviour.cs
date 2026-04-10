using UnityEngine;
using System.Collections.Generic;

public class BulletBehaviour : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float initialVelocity = 5f;

    [Header("Collision")]
    [SerializeField] private List<string> destroyTags;
    [SerializeField] private bool useWhitelist = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * initialVelocity;
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector2.up * acceleration * Time.fixedDeltaTime);
    }

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