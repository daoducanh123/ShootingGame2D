using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    private Player player;
    [SerializeField] private GameObject[] spawnEnemies;
    [SerializeField] private Transform[] spawnPos;
    [SerializeField] private float spawnTimeInterval = 5f;
    
    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (player != null)
        {

            yield return new WaitForSeconds(spawnTimeInterval);
            GameObject enemyToSpawn = spawnEnemies[Random.Range(0, spawnEnemies.Length)];
            Transform posToSpawn = spawnPos[Random.Range(0, spawnPos.Length)];

            Instantiate(enemyToSpawn, posToSpawn.position, Quaternion.identity);
        }
        Debug.Log("Cannot Spawn Enemies -> player dead");
    }
}
