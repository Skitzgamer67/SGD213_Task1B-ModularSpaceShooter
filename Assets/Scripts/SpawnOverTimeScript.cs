using System.Collections;
using UnityEngine;

public class SpawnOverTimeScript : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject enemySpaceshipPrefab;
    [SerializeField] private GameObject bossPrefab;

    [Header("Pickup Prefab")]
    [SerializeField] private GameObject pickupPrefab;

    [Header("Enemy Spawn Timing")]
    [SerializeField] private float minEnemySpawnDelay = 2f;
    [SerializeField] private float maxEnemySpawnDelay = 5f;

    [Header("Pickup Spawn Timing")]
    [SerializeField] private float minPickupSpawnDelay = 30f;
    [SerializeField] private float maxPickupSpawnDelay = 60f;

    [Header("Enemy Spawn Chances")]
    [SerializeField][Range(0f, 1f)] private float bossSpawnChance = 0.05f;  // 5%
    [SerializeField][Range(0f, 1f)] private float enemySpawnChance = 0.25f; // 25%

    private Renderer ourRenderer;

    private void Start()
    {
        ourRenderer = GetComponent<Renderer>();

        if (ourRenderer != null)
        {
            ourRenderer.enabled = false;
        }

        StartCoroutine(EnemySpawnLoop());
        StartCoroutine(PickupSpawnLoop());
    }

    private IEnumerator EnemySpawnLoop()
    {
        while (true)
        {
            float delay = Random.Range(minEnemySpawnDelay, maxEnemySpawnDelay);
            yield return new WaitForSeconds(delay);

            SpawnEnemyType();
        }
    }

    private IEnumerator PickupSpawnLoop()
    {
        while (true)
        {
            float delay = Random.Range(minPickupSpawnDelay, maxPickupSpawnDelay);
            yield return new WaitForSeconds(delay);

            SpawnPickup();
        }
    }

    private void SpawnEnemyType()
    {
        GameObject prefabToSpawn = ChooseEnemyPrefabToSpawn();

        if (prefabToSpawn == null)
        {
            Debug.LogError("No enemy prefab assigned to spawn.");
            return;
        }

        Instantiate(prefabToSpawn, GetRandomSpawnPoint(), Quaternion.identity);
    }

    private void SpawnPickup()
    {
        if (pickupPrefab == null)
        {
            Debug.LogError("No pickup prefab assigned.");
            return;
        }

        Instantiate(pickupPrefab, GetRandomSpawnPoint(), Quaternion.identity);
    }

    private GameObject ChooseEnemyPrefabToSpawn()
    {
        float randomValue = Random.value;

        if (bossPrefab != null && randomValue <= bossSpawnChance)
        {
            return bossPrefab;
        }

        if (enemySpaceshipPrefab != null && randomValue <= bossSpawnChance + enemySpawnChance)
        {
            return enemySpaceshipPrefab;
        }

        return asteroidPrefab;
    }

    private Vector2 GetRandomSpawnPoint()
    {
        if (ourRenderer == null)
        {
            Debug.LogError("SpawnOverTimeScript needs a Renderer on the spawner object.");
            return transform.position;
        }

        float x1 = transform.position.x - ourRenderer.bounds.size.x / 2;
        float x2 = transform.position.x + ourRenderer.bounds.size.x / 2;

        return new Vector2(Random.Range(x1, x2), transform.position.y);
    }
}