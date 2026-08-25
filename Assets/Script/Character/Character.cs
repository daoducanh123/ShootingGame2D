using UnityEngine;
using UnityEngine.UI;
public abstract class Character : MonoBehaviour, ICharacter
{
    // ImageType: Filled, Fill Method: Horizontal, Fill Origin: Left, Fill Amount: 1
    [Header("Health")]
    [SerializeField] protected float maxHealth = 100f; protected float currentHealth;
    [SerializeField] protected Image hpBar;  
    [SerializeField] protected float moveSpeed = 5f;

    [Header("Debuff")]
    [SerializeField] protected float slowCooldown = 0.5f;
    [SerializeField] protected float frozenCoolDown = 1.5f;
    [SerializeField] protected float fireCoolDown = 4.0f;

    #region State
    protected bool isDead = false;
    protected bool isSlowed = false;
    protected float slowTimer = 0f;
    protected bool isFrozen = false;
    protected float frozenTimer = 0f;
    protected bool isFired = false;
    protected float fireTimer = 0f;
    #endregion

    #region Debuff
    protected float damagePerSecond = 0f;
    #endregion

    #region Defaultstats
    protected float defaultAnimatorSpeed;
    protected Color defaultColor;
    protected float defaultSpeed;
    #endregion

    #region References
    protected GameManager gameManager;
    protected Animator animator;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    #endregion

    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    protected virtual void Start()
    {
        TakeDefaultStats();
    }

    protected virtual void Update()
    {
        UpdateHpBar();
        CheckFrozen();
        CheckSlow();
        CheckFire();
    }

    #region TakeAndSetDefault
    protected void TakeDefaultStats()
    {
        defaultAnimatorSpeed = animator.speed;
        defaultColor = spriteRenderer.color;
        defaultSpeed = moveSpeed;
        currentHealth = maxHealth;
    }
    public void SetDefaultAnimatorSpeed()
    {
        animator.speed = defaultAnimatorSpeed;
    }
    public void SetDefaultColor()
    {
        spriteRenderer.color = defaultColor;
    }
    public void SetDefaultMoveSpeed()
    {
        moveSpeed = defaultSpeed;
    }
    #endregion

    #region HealthSystem
    public void UpdateHpBar()
    {
        if (hpBar == null) return;
        
        hpBar.fillAmount = currentHealth / maxHealth;
    }

    public void Healing(float healValue)
    {
        currentHealth += healValue;
        currentHealth = Mathf.Min(maxHealth, currentHealth);
    }
    #endregion

    #region Combat
    public virtual void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()  // biến isDead tránh frozen fire explosive bullet gọi takedamage -> die -> addEnemyKilled cộng dồn
    {
        if (isDead) return;

        isDead = true;
        Destroy(gameObject);
    }
    #endregion

    #region Debuff
    // ================= SlowState ============================

    public void CheckSlow()
    {
        if (!isSlowed) return;

        slowTimer += Time.deltaTime;
        if (slowTimer >= slowCooldown)
        {
            isSlowed = false;
            SetDefaultMoveSpeed();
        }
    }

    public void SlowState(float slowValue)
    {
        isSlowed = true; 
        slowTimer = 0f;
        moveSpeed = Mathf.Max(defaultSpeed - slowValue, 3f);
    }


    // ================= Frozen State ==================
    public void CheckFrozen()
    {
        if (!isFrozen) return;
        frozenTimer += Time.deltaTime;
        if (frozenTimer >= frozenCoolDown)
        {
            isFrozen = false;
            SetDefaultMoveSpeed();  SetDefaultColor(); SetDefaultAnimatorSpeed();
        }
    }
    public void FrozenState()
    {
        isFrozen = true;
        frozenTimer = 0f;

        spriteRenderer.color = Color.lightBlue; 
        animator.speed = 0f;
        moveSpeed = 0;
    }

    // ================= Fire State ==================
    public void CheckFire()
    {
        if (!isFired) return;

        fireTimer += Time.deltaTime;
        currentHealth -= damagePerSecond;
        currentHealth = Mathf.Max(currentHealth, 0);

        if(currentHealth <= 0)
        {
            Die();
        }

        if (fireTimer >= fireCoolDown)
        {
            isFired = false;
            SetDefaultColor();
        }
    }
    public void FireState(float fireDamagePersecond, GameObject fireEffect)
    {
        isFired = true; fireTimer = 0f;
        spriteRenderer.color = Color.red;
        damagePerSecond = fireDamagePersecond;


        GameObject fire2 = Instantiate (fireEffect, transform.position, Quaternion.identity);
        fire2.transform.SetParent(transform, true);
        fire2.transform.localPosition = Vector3.zero;

        Destroy(fire2, fireCoolDown);
    }
    #endregion

}
