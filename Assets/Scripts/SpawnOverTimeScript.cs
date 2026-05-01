using UnityEngine;

public class SpawnOverTimeScript : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject enemySpaceshipPrefab;
    [SerializeField] private GameObject pickupPrefab;

    [Header("Spawn Timing")]
    [SerializeField] private float minSpawnDelay = 2f;
    [SerializeField] private float maxSpawnDelay = 5f;

    [Header("Spawn Chances")]
    [SerializeField][Range(0f, 1f)] private float pickupSpawnChance = 0.05f; // 5%
    [SerializeField][Range(0f, 1f)] private float enemySpawnChance = 0.25f;   // 25%

    private Renderer ourRenderer;

    private void Start()
    {
        ourRenderer = GetComponent<Renderer>();

        if (ourRenderer != null)
        {
            ourRenderer.enabled = false;
        }

        StartCoroutine(SpawnLoop());
    }

    private System.Collections.IEnumerator SpawnLoop()
    {
        while (true)
        {
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);

            Spawn();
        }
    }

    private void Spawn()
    {
        if (ourRenderer == null)
        {
            Debug.LogError("SpawnOverTimeScript needs a Renderer on the spawner object.");
            return;
        }

        float x1 = transform.position.x - ourRenderer.bounds.size.x / 2;
        float x2 = transform.position.x + ourRenderer.bounds.size.x / 2;

        Vector2 spawnPoint = new Vector2(Random.Range(x1, x2), transform.position.y);

        GameObject prefabToSpawn = ChoosePrefabToSpawn();

        if (prefabToSpawn == null)
        {
            Debug.LogError("No prefab assigned to spawn.");
            return;
        }

        Instantiate(prefabToSpawn, spawnPoint, Quaternion.identity);
    }

    private GameObject ChoosePrefabToSpawn()
    {
        if (pickupPrefab != null && Random.value <= pickupSpawnChance)
        {
            return pickupPrefab;
        }

        if (enemySpaceshipPrefab != null && Random.value <= enemySpawnChance)
        {
            return enemySpaceshipPrefab;
        }

        return asteroidPrefab;
    }
}