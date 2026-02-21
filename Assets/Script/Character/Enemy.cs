using UnityEngine;

public abstract class Enemy : Character
{
    protected Player player;
    [SerializeField] protected float damageEnemyDealt = 4f;


    protected override void Awake()
    {
        base.Awake();
        player = FindAnyObjectByType<Player>();
    }


    protected override void Update()
    {
        base.Update();
        if (player != null) { // ko check luc player die la null
            movementDirection = player.transform.position - transform.position;
        }

    }

    protected virtual void FixedUpdate()
    {
        EnemyMovement();
    }


    // ================ ENEMY MOVEMENT  =================
    Vector3 movementDirection = Vector3.zero;
    protected void EnemyMovement()
    {
        // neu ko check null thi se bi loi khi enemy spawn ma player chua spawn
        if (player != null)
        {
            rb.linearVelocity = movementDirection.normalized * moveSpeed;

            if (transform.position.x <= player.transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    // ================ Killed Text =========================

    protected override void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Destroy(gameObject);
            gameManager.EnemyKilled(1);
        }
        else return;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.green;
            player.TakeDamage(damageEnemyDealt);
        }
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.green;
            player.TakeDamage(damageEnemyDealt * Time.deltaTime);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.SetDefaultColor();
        }
    }
 
}
