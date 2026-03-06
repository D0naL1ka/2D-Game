// Scripts/FallRespawn.cs
using UnityEngine;

public class FallRespawn : MonoBehaviour
{
    public static FallRespawn Instance { get; private set; }

    [Header("Налаштування падіння")]
    [SerializeField] private float fallDeathY = -20f;

    private Transform playerTransform;
    private Rigidbody2D playerRb;

    private Vector3 startPosition;
    private Vector3 checkpointPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("FallRespawn: Не знайдено гравця з тегом 'Player'!");
            enabled = false;
            return;
        }

        playerTransform = player.transform;
        playerRb = player.GetComponent<Rigidbody2D>();

        startPosition = playerTransform.position;
        checkpointPosition = startPosition;

        Debug.Log($"FallRespawn готовий. Старт: {startPosition}");
    }

    private void Update()
    {
        // Падіння → повернення на початок
        if (playerTransform.position.y < fallDeathY)
        {
            RespawnToStart();
        }
    }

    private void RespawnToStart()
    {
        Debug.Log("Гравець впав! Повернення на початок.");
        playerTransform.position = startPosition;
        playerRb.linearVelocity = Vector2.zero;
        checkpointPosition = startPosition; // Скидаємо прогрес
    }

    public void RespawnToCheckpoint()
    {
        Debug.Log("Respawn на чекпоінт.");
        playerTransform.position = checkpointPosition;
        playerRb.linearVelocity = Vector2.zero;
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
        Debug.Log($"Чекпоінт оновлено: {position}");
    }
}