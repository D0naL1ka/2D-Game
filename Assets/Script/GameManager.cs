using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Elements")]
    [SerializeField] private Slider energySlider;
    [SerializeField] private RawImage[] heartImages;
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("GameOver Canvas")]
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TextMeshProUGUI gameOverText;

    [Header("Victory Settings")]
    [SerializeField] private Transform finishTransform;
    [SerializeField] private float finishDistance = 1.5f;
    [SerializeField] private int totalCoinsRequired = 11;

    [Header("Settings")]
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float energyDrainRate = 10f;

    // Тимчасові посилання на кнопки (знаходимо після кожного завантаження)
    private Button restartButton;
    private Button closeButton;

    private Transform playerTransform;
    private bool victoryChecked = false;
    private int currentLives = 5;
    private int currentCoins = 0;
    private float currentEnergy;

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
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        ResetGameState();
        ReinitializeAfterLoad();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetGameState();
        ReinitializeAfterLoad();
        Time.timeScale = 1f;
    }

    private void ReinitializeAfterLoad()
    {
        FindAllReferences();
        SetupGameOverButtons();
        UpdateUI();
        HideGameOverCanvas();
    }

    private void ResetGameState()
    {
        currentLives = 5;
        currentCoins = 0;
        currentEnergy = maxEnergy;
        victoryChecked = false;
        playerTransform = null;
    }

    private void HideGameOverCanvas()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    private void FindAllReferences()
    {
        FindPlayer();
        FindFinish();
        FindUIElements();
        FindGameOverCanvasAndButtons();
    }

    private void FindPlayer()
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null) playerTransform = player.transform;
    }

    private void FindFinish()
    {
        finishTransform = GameObject.FindWithTag("Finish")?.transform
                       ?? GameObject.Find("Finish")?.transform;
    }

    private void FindUIElements()
    {
        energySlider = GameObject.Find("EnergySlider")?.GetComponent<Slider>();
        coinText = GameObject.Find("CoinText")?.GetComponent<TextMeshProUGUI>();

        var heartsParent = GameObject.Find("Hearts");
        if (heartsParent != null)
        {
            heartImages = heartsParent.GetComponentsInChildren<RawImage>(true);
        }
    }

    private void FindGameOverCanvasAndButtons()
    {
        if (gameOverCanvas == null)
        {
            gameOverCanvas = GameObject.FindWithTag("GameOverCanvas");
        }

        if (gameOverCanvas == null)
        {
            Debug.LogError("GameOverCanvas не знайдено!");
            return;
        }

        gameOverText = gameOverCanvas.GetComponentInChildren<TextMeshProUGUI>(true);
        restartButton = gameOverCanvas.transform.Find("RestartButton")?.GetComponent<Button>();
        closeButton = gameOverCanvas.transform.Find("CloseButton")?.GetComponent<Button>();
    }

    private void SetupGameOverButtons()
    {
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartGame);
        }

        if (closeButton != null)
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(QuitGame);   // або ClosePanel, якщо потрібно просто сховати
        }
    }

    private void Update()
    {
        if (playerTransform == null) FindPlayer();

        if (!victoryChecked && playerTransform != null && finishTransform != null &&
            currentCoins >= totalCoinsRequired &&
            Vector2.Distance(playerTransform.position, finishTransform.position) < finishDistance)
        {
            victoryChecked = true;
            GameOver("Victory");
        }
    }

    public void AddCoin()
    {
        currentCoins++;
        UpdateUI();
    }

    public void LoseLife()
    {
        currentLives--;
        currentEnergy = maxEnergy;
        UpdateUI();
        if (currentLives <= 0) GameOver("Defeat");
    }

    public void DrainEnergy(float deltaTime)
    {
        currentEnergy -= energyDrainRate * deltaTime;
        currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
        UpdateUI();
        if (currentEnergy <= 0f) LoseLife();
    }

    private void UpdateUI()
    {
        if (energySlider != null)
            energySlider.value = currentEnergy / maxEnergy;

        if (heartImages != null)
            for (int i = 0; i < heartImages.Length; i++)
                if (heartImages[i] != null)
                    heartImages[i].gameObject.SetActive(i < currentLives);

        if (coinText != null)
            coinText.text = $"{currentCoins}/{totalCoinsRequired}";
    }

    private void GameOver(string result)
    {
        Time.timeScale = 0f;

        if (gameOverCanvas == null)
        {
            Debug.LogError("GameOverCanvas == null — неможливо показати панель");
            return;
        }

        if (gameOverText != null)
            gameOverText.text = result == "Victory" ? "Victory!" : "Game Over!";

        gameOverCanvas.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}