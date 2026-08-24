using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnEnemies;
    [SerializeField] private Transform[] spawnPos;
    [SerializeField] private float spawnTimeInterval = 2f;
    
    private void Start()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (Player.Instance != null)
        {
            yield return new WaitForSeconds(spawnTimeInterval);
            GameObject enemyToSpawn = spawnEnemies[Random.Range(0, spawnEnemies.Length)];
            Transform posToSpawn = spawnPos[Random.Range(0, spawnPos.Length)];

            Instantiate(enemyToSpawn, posToSpawn.position, Quaternion.identity);
        }
        Debug.Log("Cannot Spawn Enemies -> player dead");
    }
}
