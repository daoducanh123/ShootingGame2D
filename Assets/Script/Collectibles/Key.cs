using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private Boss boss;

    private void OnEnable()
    {
        boss.OnBossDeath += SpawnKey;
    }

    private void OnDisable()
    {
        boss.OnBossDeath -= SpawnKey;
    }

    private void SpawnKey()
    {
        Instantiate( gameObject, boss.transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance == null) return;

        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.GameWinMenu();

            Debug.Log("Win!");

            Destroy(gameObject);
        }
    }
}