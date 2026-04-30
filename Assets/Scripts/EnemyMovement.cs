using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyMovement handles all of the movement specifc state and behaviour for the enemy.
/// </summary>
public class EnemyMovement : MonoBehaviour {

    // enemyAcceleration indicates how fast the enemy accelerates
    [SerializeField]
    private float enemyAcceleration = 5000f;

    // local references
    private Rigidbody2D ourRigidbody;

    void Start() {
        // populate ourRigidbody
        ourRigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// MoveEnemy takes a direction as a parameter, and applies a force in this provided direction
    /// to ourRigidbody, based on the enemyAcceleration variables and the delta time.
    /// </summary>
    /// <param name="horizontalInput">A direction vector, expected to be a unit vector (magnitude of 1).</param>
    public void MoveEnemy(Vector2 direction) {
        //calculate our force to add
        Vector2 forceToAdd = direction * enemyAcceleration * Time.deltaTime;
        // apply forceToAdd to ourRigidbody
        ourRigidbody.AddForce(forceToAdd);
    }
}
