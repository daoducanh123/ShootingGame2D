using UnityEngine;

public class EnemyBullet : Bullet
{

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("Player Hit By BulletEnemy");
            player.TakeDamage(bulletDamage);
        }
        else Debug.Log("Player null hit");
    }

    public void EnemyBulletMovement(Vector3 direction)
    {
        rb.linearVelocity = direction * bulletSpeed;
    }
}
