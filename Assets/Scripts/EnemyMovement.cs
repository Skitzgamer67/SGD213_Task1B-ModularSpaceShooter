using UnityEngine;

/// <summary>
/// EnemyMovement handles enemy-specific movement requests,
/// then passes the actual movement work to EngineBase.
/// </summary>
[RequireComponent(typeof(EngineBase))]
public class EnemyMovement : MonoBehaviour
{
    private EngineBase engine;

    private void Awake()
    {
        engine = GetComponent<EngineBase>();
    }

    /// <summary>
    /// Moves the enemy in the provided direction.
    /// </summary>
    public void MoveEnemy(Vector2 direction)
    {
        engine.Accelerate(direction);
    }
}