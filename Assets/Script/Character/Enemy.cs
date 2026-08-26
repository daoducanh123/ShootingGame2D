using System.Diagnostics.Tracing;
using UnityEngine;
using System;
public abstract class Enemy : Character
{
    [SerializeField] protected float damageEnemyDealt = 4f;
    Vector3 movementDirection = Vector3.zero;
    public static event Action OnEnemyDeath;

    protected override void Awake()
    {
        base.Awake();
    }


    protected override void Update()
    {
        base.Update();
        if (Player.Instance != null) { // ko check luc player die la null
            movementDirection = Player.Instance.transform.position - transform.position;
        }
    }

    protected virtual void FixedUpdate()
    {
        EnemyMovement();
    }

    #region EnemyMovement
    protected void EnemyMovement()
    {
        // neu ko check null thi se bi loi khi enemy spawn ma player chua spawn
        if (Player.Instance == null) return;
     
        rb.linearVelocity = movementDirection.normalized * moveSpeed;

        if (transform.position.x <= Player.Instance.transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
    #endregion
    
    #region Combat
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) return;
            spriteRenderer.color = Color.green;

            if (Player.Instance == null) return;
            Player.Instance.TakeDamage(damageEnemyDealt);
        }
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) return;
            spriteRenderer.color = Color.green;

            if (Player.Instance == null) return;
            Player.Instance.TakeDamage(damageEnemyDealt * Time.deltaTime);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Player.Instance == null) return;
            Player.Instance.SetDefaultColor();
        }
    }

    protected override void Die()
    {
        if (isDead) return;
        base.Die();
        OnEnemyDeath?.Invoke();
    }
    #endregion

}
