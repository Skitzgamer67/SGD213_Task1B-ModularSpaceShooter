using UnityEngine;
using System.Collections;

/// <summary>
/// PlayerMovementScript handles all of the movement specifc state and behaviour for the player.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    // horizontalPlayerSpeed indicates how fast we accelerate Horizontally
    [SerializeField]
    private float horizontalPlayerAcceleration = 5000f;

    // local references
    private Rigidbody2D ourRigidbody;

    void Start()
    {
        // populate ourRigidbody
        ourRigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// MovePlayer takes a float representing the raw horizontal input, and applies a lateral force
    /// to ourRigidbody, based on the provided horizontal input, the horizontalPlayerAcceleration
    /// and the delta time.
    /// </summary>
    /// <param name="horizontalInput">Raw horizontal input value. Expected to be between -1 and 1. 
    /// Number outside this range increase movement speed. A value of 0 is ignored.</param>
    public void MovePlayer(float horizontalInput) {
        // a horizontalInput of 0 has no effect, as we want our ship to drift
        if (horizontalInput != 0) {
            //calculate our force to add
            Vector2 forceToAdd = Vector2.right * horizontalInput * horizontalPlayerAcceleration * Time.deltaTime;
            // apply forceToAdd to ourRigidbody
            ourRigidbody.AddForce(forceToAdd);
        }
    }

    public void MovePlayer(Vector2 direction)
    {
        // a horizontalInput of 0 has no effect, as we want our ship to drift
        if (direction.magnitude != 0)
        {
            //calculate our force to add
            Vector2 forceToAdd = direction * horizontalPlayerAcceleration * Time.deltaTime;
            // apply forceToAdd to ourRigidbody
            ourRigidbody.AddForce(forceToAdd);
        }
    }
}
