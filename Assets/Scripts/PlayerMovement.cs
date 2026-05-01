using UnityEngine;

/// <summary>
/// PlayerMovement handles player-specific movement requests,
/// then passes the actual movement work to EngineBase.
/// </summary>
[RequireComponent(typeof(EngineBase))]
public class PlayerMovement : MonoBehaviour
{
    private EngineBase engine;

    private void Awake()
    {
        engine = GetComponent<EngineBase>();
    }

    /// <summary>
    /// Moves the player horizontally using raw input.
    /// </summary>
    public void MovePlayer(float horizontalInput)
    {
        if (horizontalInput == 0)
        {
            return;
        }

        engine.Accelerate(Vector2.right * horizontalInput);
    }

    /// <summary>
    /// Moves the player in the provided direction.
    /// </summary>
    public void MovePlayer(Vector2 direction)
    {
        engine.Accelerate(direction);
    }
}