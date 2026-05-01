using UnityEngine;
using System.Collections;

/// <summary>
/// MoveConstantly gives an object the ability to continuously move based on the
/// specified direction, acceleration and initialVelocity variables.
/// </summary>
public class MoveConstantly : MonoBehaviour
{

    [SerializeField]
    private float acceleration = 100f;

    [SerializeField]
    private float initialVelocity = 10f;

    [SerializeField]
    // our direction to move in
    private Vector2 direction = new Vector2(0, 1);

    /// <summary>
    /// Direction provides access to the direction variable used to direct the movement of our object.
    /// It is expected that when setting the direction, the provided Vector2 is a unit vector. If not,
    /// it will be automatically normalised.
    /// </summary>
    public Vector2 Direction {
        get {
            return direction;
        }
        set {
            if (value.magnitude == 1) {
                direction = value;
            } else {
                direction = value.normalized;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Enemy")) // Checks if this script is within a gameObject with the Enemy Tag (This is so this script can also be used in the Pickup)
        {
            // Checks if the other object has the 'Player' tag
            if (other.CompareTag("Player"))
            {
                Destroy(other.gameObject); // Destroys enemy
                Destroy(gameObject);       // Destroys bullet
            }
        }
    }

    // local references
    private Rigidbody2D ourRigidbody;

    void Start()
    {
        ourRigidbody = GetComponent<Rigidbody2D>();

        ourRigidbody.velocity = direction * initialVelocity;
    }
    void OnBecameInvisible()
    {
        // Destroys bullet
        Destroy(gameObject);
    }
    void Update()
    {
        // calculate our force to add, based on our provided direction, acceleration and delta time
        Vector2 forceToAdd = direction * acceleration * Time.deltaTime;
        // add our forceToAdd to ourRigidbody
        ourRigidbody.AddForce(forceToAdd);
    }
}
