using UnityEngine;

using UnityEngine.SceneManagement;



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

        DontDestroyOnLoad(gameObject);

    }

    private void OnEnable()

    {

        SceneManager.sceneLoaded += OnSceneLoaded;

        FindPlayer();

    }

    private void OnDisable()

    {

        SceneManager.sceneLoaded -= OnSceneLoaded;

    }



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)

    {

        FindPlayer();

    }



    private void FindPlayer()

    {

        var player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)

        {

            playerTransform = player.transform;

            playerRb = player.GetComponent<Rigidbody2D>();



            if (startPosition == Vector3.zero)

            {

                startPosition = playerTransform.position;

                lastCheckpointPosition = startPosition;

            }



            Debug.Log($"FallRespawn initialized. Start position: {startPosition}");

        }

        else

        {

            Debug.LogError("FallRespawn: Player with tag 'Player' not found!");

        }

    }



    private void Update()
    {
        // Перевіряємо null перед доступом
        if (playerTransform == null)
        {
            FindPlayer();  // намагаємося знайти, якщо раптом зник
            return;
        }

        if (playerTransform.position.y < fallDeathY)
        {
            GameManager.Instance.LoseLife();  // -1 життя
            RespawnToLastCheckpoint();
        }
    }

    public void RespawnToLastCheckpoint()

    {

        Debug.Log("Player fell! Respawning to last checkpoint.");

        playerTransform.position = lastCheckpointPosition;

        playerRb.linearVelocity = Vector2.zero;

    }



    public void SetCheckpoint(Vector3 position)

    {

        lastCheckpointPosition = position;

        Debug.Log($"Checkpoint updated: {position}");

    }

}