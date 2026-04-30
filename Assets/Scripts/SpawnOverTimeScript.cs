using UnityEngine;

public class SpawnOverTimeScript : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject enemySpaceshipPrefab;

    [Header("Spawn Timing")]
    [SerializeField] private float minSpawnDelay = 2f;
    [SerializeField] private float maxSpawnDelay = 5f;

    [Header("Spawn Chance")]
    [SerializeField][Range(0f, 1f)] private float enemySpawnChance = 0.1f; // 10%

    private Renderer ourRenderer;

    void Start()
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

    void Spawn()
    {
        float x1 = transform.position.x - ourRenderer.bounds.size.x / 2;
        float x2 = transform.position.x + ourRenderer.bounds.size.x / 2;

        Vector2 spawnPoint = new Vector2(Random.Range(x1, x2), transform.position.y);

        // Decide what to spawn
        GameObject prefabToSpawn = Random.value <= enemySpawnChance
            ? enemySpaceshipPrefab
            : asteroidPrefab;

        Instantiate(prefabToSpawn, spawnPoint, Quaternion.identity);
    }
}