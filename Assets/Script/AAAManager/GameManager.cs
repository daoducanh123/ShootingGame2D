using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Boss boss;

    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameWinMenu;


    [Header("Text")]
    [SerializeField] private TextMeshProUGUI enemyKilledText;

    private int numberEnemyDie = -1;


    [Header("Energy")]
    [SerializeField] private float maxEnergy = 10f;
    [SerializeField] private Image energyBar;

    [Header("Character")]
    [SerializeField] private Player player;
    
    private float currentEnergy = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        boss = FindAnyObjectByType<Boss>();
    }

    private void Start()
    {
        UpdateEnergyBar();
        UpdateKillText();
        MainMenu();

        if (boss == null) return;
        boss.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDeath += UpdateKillText;
        player.OnPlayerDeath += GameOverMenu;
    }    
    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= UpdateKillText;
        player.OnPlayerDeath -= GameOverMenu;
    }


    #region Energy Bar
    public void IncreaseCurrentEnergy(float valueTaken)
    {
        currentEnergy += valueTaken;

        currentEnergy = Mathf.Min(currentEnergy, maxEnergy);

        UpdateEnergyBar();

        if (currentEnergy >= maxEnergy)
        {
            Debug.Log("Boss spawn");

            if (boss == null) return;
            boss.gameObject.SetActive(true);
        }
    }

    private void UpdateEnergyBar()
    {
        if (energyBar == null)
        {
            return;
        }
        energyBar.fillAmount = currentEnergy / maxEnergy;
    }
    #endregion

    #region Kill Text

    private void UpdateKillText()
    {
        numberEnemyDie += 1;
        if (enemyKilledText == null)
        {
            return;
        }
            enemyKilledText.text = "Killed:" + numberEnemyDie.ToString();
    }
    #endregion

    #region Menus
    public void MainMenu()
    {
        if (mainMenu == null || gameOverMenu == null || pauseMenu == null) return;

        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameWinMenu.SetActive(false);

        Time.timeScale = 0f;
    }
    public void PauseMenu()
    {
        if (mainMenu == null || gameOverMenu == null || pauseMenu == null || gameWinMenu == null) return;

        pauseMenu.SetActive(true);
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        gameWinMenu.SetActive(false);

        Time.timeScale = 0f;
    }
    public void GameOverMenu()
    {
        if (mainMenu == null || gameOverMenu == null || pauseMenu == null || gameWinMenu == null) return;

        gameOverMenu.SetActive(true);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameWinMenu.SetActive(false);

        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        if (mainMenu == null || gameOverMenu == null || pauseMenu == null || gameWinMenu == null) return;

        gameOverMenu.SetActive(false);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameWinMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        if (mainMenu == null || gameOverMenu == null || pauseMenu == null || gameWinMenu == null) return;

        gameOverMenu.SetActive(false);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameWinMenu.SetActive(false);

        Time.timeScale = 1f;
    }

    public void GameWinMenu()
    {
        if (mainMenu == null || gameOverMenu == null || pauseMenu == null || gameWinMenu == null) return;

        gameWinMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);

        Time.timeScale = 0f;
    }

    #endregion
}