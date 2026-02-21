using UnityEngine;

public class PlayerBullet : Bullet
{
    protected PlayerBulletPool playerBulletPool;
    protected Player player;
    protected override void Awake()
    {
        base.Awake();
        playerBulletPool = PlayerBulletPool.Instance;
        player = FindAnyObjectByType<Player>();
    }

    // =============== Bullet Movement =================
    public void BulletMovement( )
    {
        if (rb != null && playerBulletPool != null)
        { 
            rb.linearVelocity = transform.right * bulletSpeed; // di chuyển theo hướng của viên đạn, * Time.deltaTime để di chuyển mượt hơn
            Invoke(nameof(ReturnBulletLifeTime), bulletLifetime); // gọi hàm Deactivate sau bulletLifetime giây để trả viên đạn về == Thay vì
            //Destroy(gameObject, bulletLifetime);

        }
    }


    // =============== Deactivate Bullet =================
    private void ReturnBulletLifeTime()
    {
        CancelInvoke(); // / Huỷ các Invoke cũ vd đạn A hit enemy return rồi mà B vừa bắn chưa kịp bay hết lifetime của B đã bị lifetime của A hết làm cho return 
        playerBulletPool.ReturnBullet(gameObject);
    }


    // ================ Blood Effect && Slow Effect ======================
    [SerializeField] private GameObject bloodPrefab;
    [SerializeField] private float slowValue = 0.8f;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy Enemy = collision.GetComponent<Enemy>();
        if (Enemy != null) // collision.CompareTag("Enemy") ko càn
        {
            Enemy.TakeDamage(bulletDamage);
            Enemy.SlowState(slowValue);
            if (player != null)
            {
                Debug.Log("Hit heal for player");
                player.Healing(2);
            }

            GameObject blood = Instantiate(bloodPrefab, collision.transform.position, Quaternion.identity); // Code nayf mà để dưới là base nó return pool là ko giòn đau
            Destroy(blood, 0.3f);
            playerBulletPool.ReturnBullet(gameObject);
        }
        if (collision.CompareTag("Tile"))
        {
            playerBulletPool.ReturnBullet(gameObject);

        }

    }
}
