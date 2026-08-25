using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    private Boss boss;


    [Header("Text")]
    [SerializeField] private TextMeshProUGUI enemyKilledText;

    private int numberEnemyDie = 0;


    [Header("Energy")]
    [SerializeField] private float maxEnergy = 10f;
    [SerializeField] private Image energyBar;

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
        UpdateEnemyKilledText();

        if (boss == null) return;
        boss.gameObject.SetActive(false);
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
        energyBar.fillAmount = (float)currentEnergy / (float)maxEnergy;
    }
    #endregion

    #region Kill Text
    public void EnemyKilled(int num)
    {
        numberEnemyDie += num;

        UpdateEnemyKilledText();
    }


    private void UpdateEnemyKilledText()
    {
        if (enemyKilledText == null)
        {
            return;
        }
            enemyKilledText.text = "Killed:" + numberEnemyDie.ToString();
    }
    # endregion
}