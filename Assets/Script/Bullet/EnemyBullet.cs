using UnityEngine;

public class EnemyBullet : Bullet
{

    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.CompareTag("Player")) return;
       if (Player.Instance == null) return;
 
       Debug.Log("Player Hit By BulletEnemy");
       Player.Instance.TakeDamage(bulletDamage);
    }

    public void EnemyBulletMovement(Vector3 direction)
    {
        rb.linearVelocity = direction * bulletSpeed;
    }
}
