using UnityEngine;

public class ExplosionEnemy : Enemy
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private float numberOfFires = 8f;
    [SerializeField] private float fireCircleDuration = 4f;
    private float fireRadius = 3f;


    // ================ Create Explosion =============
    private void CreateExplosion()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
    }
    // ================ Create Fire ====================

    private void CreateFire()
    {
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

    // =============== Die Explosion ================

    protected override void Die()
    {
        CreateExplosion();
        CreateFire();
        base.Die(); // die để dưới
    }

    // ================ Die Impact On Player ====================
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            CreateExplosion();
            CreateFire();
            base.Die();
        }
    }

}
