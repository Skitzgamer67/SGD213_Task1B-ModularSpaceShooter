using UnityEngine;

/* SUMMARY

MoveConstantly gives an object the ability to continuously move based on
a direction, acceleration, and initial velocity.

If the object is the asteroid prefab named "Asteroid", it can randomise its
falling speed and rotation when spawned.

*/
public class MoveConstantly : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float acceleration = 100f;
    [SerializeField] private float initialVelocity = 10f;

    [SerializeField]
    private Vector2 direction = new Vector2(0, 1);

    [Header("Asteroid Random Settings")]
    [SerializeField] private bool randomiseIfAsteroid = true;
    [SerializeField] private float minAsteroidSpeed = 3f;
    [SerializeField] private float maxAsteroidSpeed = 8f;
    [SerializeField] private float minAsteroidSpinSpeed = 50f;
    [SerializeField] private float maxAsteroidSpinSpeed = 250f;

    private Rigidbody2D ourRigidbody;
    private bool isAsteroid;

    public Vector2 Direction
    {
        get
        {
            return direction;
        }
        set
        {
            if (value.magnitude == 1)
            {
                direction = value;
            }
            else
            {
                direction = value.normalized;
            }
        }
    }

    private void Start()
    {
        ourRigidbody = GetComponent<Rigidbody2D>();

        if (ourRigidbody == null)
        {
            Debug.LogError(gameObject.name + " is missing a Rigidbody2D.");
            return;
        }

        isAsteroid = IsNamedEnemyAsteroid();

        if (isAsteroid && randomiseIfAsteroid) // If this object is the asteroid prefab, randomise its speed and spin
        {
            ApplyRandomAsteroidSettings();
        }

        ourRigidbody.velocity = direction * initialVelocity;
    }

    private void Update()
    {
        if (ourRigidbody == null)
        {
            return;
        }

        Vector2 forceToAdd = direction * acceleration * Time.deltaTime;
        ourRigidbody.AddForce(forceToAdd);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Enemy"))
        {
            if (other.CompareTag("Player"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible() // Destroy objects that leave the camera view to avoid unused objects building up
    {
        Destroy(gameObject);
    }

    /*
    Checks whether this object is the asteroid prefab called "Asteroid".
    Unity-spawned prefab instances usually become "Asteroid(Clone)", so this handles that too
    */
    private bool IsNamedEnemyAsteroid()
    {
        string cleanName = gameObject.name.Replace("(Clone)", "").Trim();
        return cleanName == "Asteroid";
    }

    /// <summary>
    /// Gives the asteroid a random speed and random clockwise/anticlockwise spin.
    /// </summary>
    private void ApplyRandomAsteroidSettings()
    {
        initialVelocity = Random.Range(minAsteroidSpeed, maxAsteroidSpeed);

        float spinSpeed = Random.Range(minAsteroidSpinSpeed, maxAsteroidSpinSpeed);

        if (Random.value < 0.5f)
        {
            spinSpeed *= -1f;
        }

        ourRigidbody.angularVelocity = spinSpeed;
    }
}