using UnityEngine;

public class ExplosionEnemy : Enemy
{
    [Header("Prefabs")]
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject firePrefab;

    [Header("Fire Stats")]
    [SerializeField] private float numberOfFires = 8f;
    [SerializeField] private float fireCircleDuration = 4f;
    [SerializeField] private float fireRadius = 3f;


    private void CreateExplosion()
    {
        if (explosionPrefab == null) return;

        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
    }

    private void CreateFire()
    {
        if (firePrefab == null) return;

        float angleStep = 360f / numberOfFires;
        for (int i = 0; i < numberOfFires; ++i)
        {
            float angle = angleStep * i * Mathf.Deg2Rad;

            Vector3 fireDirection = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
            Vector3 spawnPos = transform.position + fireDirection * fireRadius;

            GameObject fire  = Instantiate(firePrefab, spawnPos, Quaternion.identity);
            Destroy(fire, fireCircleDuration);
        }
    }

    protected override void Die()
    {
        if (isDead) return;
        base.Die(); 
        CreateExplosion();
        CreateFire();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CreateExplosion();
            CreateFire();
            base.Die();
        }
    }

}
