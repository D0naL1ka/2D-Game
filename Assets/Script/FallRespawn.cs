using UnityEngine;

public class FallRespawn : MonoBehaviour
{
    public static FallRespawn Instance { get; private set; }

    [Header("Fall Settings")]
    [SerializeField] private float fallDeathY = -20f;

    private Transform playerTransform;
    private Rigidbody2D playerRb;

    private Vector3 startPosition;
    private Vector3 lastCheckpointPosition;

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
            Debug.LogError("FallRespawn: Player with tag 'Player' not found!");
            enabled = false;
            return;
        }

        playerTransform = player.transform;
        playerRb = player.GetComponent<Rigidbody2D>();

        startPosition = playerTransform.position;
        lastCheckpointPosition = startPosition;

        Debug.Log($"FallRespawn initialized. Start position: {startPosition}");
    }

    private void Update()
    {
        if (playerTransform.position.y < fallDeathY)
        {
            RespawnToLastCheckpoint();
        }
    }

    public void RespawnToLastCheckpoint()
    {
        Debug.Log("Player fell! Respawning to last checkpoint.");
        playerTransform.position = lastCheckpointPosition;
        playerRb.linearVelocity = Vector2.zero;
    }

    //після завершення гри
    public void RespawnToStart() 
    {
        Debug.Log("Respawn to start (manual).");
        playerTransform.position = startPosition;
        playerRb.linearVelocity = Vector2.zero;
        lastCheckpointPosition = startPosition; 
    }

    public void SetCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
        Debug.Log($"Checkpoint updated: {position}");
    }
}