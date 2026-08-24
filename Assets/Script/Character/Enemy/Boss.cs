using UnityEngine;
using System.Collections;


public class Boss : Enemy
{
    [Header("Boss Skills")]
    [SerializeField] private float intervalSkillsTime = 4f;
    [SerializeField] private float numberOfBullets = 8f;
    [SerializeField] private float teleportRadius = 8f;

    [Header("Prefabs")]
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private GameObject miniEnemyPrefab;


    protected override void Start()
    {
        base.Start();
        StartCoroutine(IBossShooting());
    }

    private IEnumerator IBossShooting()
    {
        while (Player.Instance != null) 
        {
            yield return new WaitForSeconds(intervalSkillsTime);
            int skill = Random.Range(0,4);
            switch (skill)
            {
                case 0:
                    ShootingCircle(); break;
                case 1:
                    Teleport(); break;
                case 2:
                    SpawnMiniEnemy(); break;
            }

        }
    }

    private void ShootingCircle()
    {
        if (enemyBulletPrefab == null) return;

        float angleStep = 360f/numberOfBullets;
        for (int i = 0; i < numberOfBullets; ++i) 
        {
            float angle = angleStep * i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
            GameObject enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            EnemyBullet enemyBulletCS = enemyBullet.GetComponent<EnemyBullet>();

            if (enemyBulletCS == null) return;
            enemyBulletCS.EnemyBulletMovement(direction);
    
            Destroy(enemyBullet, 8f);
        }

    }
    private void Teleport()
    {
        if (Player.Instance == null) return;

        Vector2 teleportOffset =  Random.insideUnitCircle * teleportRadius; 
        Vector2 playerPos = Player.Instance.transform.position;
        transform.position = playerPos + teleportOffset;    

    }
    private void SpawnMiniEnemy()
    {
        if (miniEnemyPrefab == null) return;
        GameObject miniEnemy = Instantiate(miniEnemyPrefab, transform.position, transform.rotation);
    }
}
