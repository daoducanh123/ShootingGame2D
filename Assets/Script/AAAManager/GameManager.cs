using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Boss boss;

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
            boss.gameObject.SetActive(false);
    }

    //  ====================== Energy Image ===============================
    [SerializeField] private float maxEnergy = 10f;
    [SerializeField] private Image energyBar;
    private float currentEnergy = 0f;
    public void IncreaseCurrentEnergy(float valueTaken)
    {
        currentEnergy += valueTaken;
        currentEnergy = Mathf.Min(currentEnergy, maxEnergy);
        UpdateEnergyBar();
        if (currentEnergy >= maxEnergy)
        {
            Debug.Log("Boss spawn");
            boss.gameObject.SetActive(true);
        }
        
    }


    private void UpdateEnergyBar()
    {
        if (energyBar != null)
        {
            energyBar.fillAmount = currentEnergy/maxEnergy;
        }
    }


    //  ====================== EnemyKilledText ===============================

    [SerializeField] private TextMeshProUGUI enemyKilledText;
    private int numberEnemyDie = 0;
    public void EnemyKilled(int num)
    {
        numberEnemyDie += num;
        UpdateEnemyKilledText();
    }

    private void UpdateEnemyKilledText()
    {
        if (enemyKilledText != null)
        {
            enemyKilledText.text = "Killed:" + numberEnemyDie.ToString();
        }
    }



}

