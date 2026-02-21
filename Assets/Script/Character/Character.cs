using UnityEngine;
using UnityEngine.UI;
public abstract class Character : MonoBehaviour, ICharacter
{
    // ============== Initialize Variables ==============
    // ImageType: Filled, Fill Method: Horizontal, Fill Origin: Left, Fill Amount: 1
    [SerializeField] protected float maxHealth = 100f; protected float currentHealth;
    [SerializeField] protected Image hpBar;  
    [SerializeField] protected float moveSpeed = 5f;

    protected float damagePerSecond = 0f;
    protected bool isDead = false;

    protected GameManager gameManager;
    protected Animator animator;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;


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


    // ================= TakeSetDefaultStats =================
    protected float defaultAnimatorSpeed;
    protected Color defaultColor;
    protected float defaultSpeed;
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
    // ================ Health Management =================
    public void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHealth / maxHealth;
        }
    }

    public void Healing(float healValue)
    {
        currentHealth += healValue;
        currentHealth = Mathf.Min(maxHealth, currentHealth);
    }

    // ================ Take Damage  ====================== 
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

    // ================= SlowState ============================
    [SerializeField] protected float slowCooldown = 0.5f;
    protected float slowTimer = 0f;
    protected bool isSlowed = false;

    public void CheckSlow()
    {
        if (isSlowed)
        {
            slowTimer += Time.deltaTime;

            if (slowTimer >= slowCooldown)
            {
                isSlowed = false;
                SetDefaultMoveSpeed();
            }
        }
        else return;

    }
    public void SlowState(float slowValue)
    {
        Debug.Log("Slow");
        isSlowed = true; 
        slowTimer = 0f;

        moveSpeed = Mathf.Max(defaultSpeed - slowValue, 3f);
        Debug.Log("Slowed");
    }


    // ================= Frozen State ==================
    [SerializeField] protected float frozenCoolDown = 1.5f;
    protected bool isFrozen = false;
    protected float frozenTimer = 0f;

    public void CheckFrozen()
    {
        if (isFrozen)
        {
            frozenTimer += Time.deltaTime;
            if (frozenTimer >= frozenCoolDown)
            {
                isFrozen = false;
                SetDefaultMoveSpeed();  SetDefaultColor(); SetDefaultAnimatorSpeed();
            }
        }
        else return;
    }
    public void FrozenState()
    {
        Debug.Log("Frozen");
        isFrozen = true;
        frozenTimer = 0f;

        spriteRenderer.color = Color.lightBlue; 
        animator.speed = 0f;
        moveSpeed = 0;
    }

    // ================= Fire State ==================
    [SerializeField] protected float fireCoolDown = 4.0f;  
    protected bool isFired = false;
    protected float fireTimer = 0f;
    

    public void CheckFire()
    {
        if (isFired)
        {
            Debug.Log("Fire");

            fireTimer += Time.deltaTime;
            currentHealth -= damagePerSecond;
            currentHealth = Mathf.Max(currentHealth, 0);
            if(currentHealth <= 0)
            {
                isDead = true;
                Die();
            }

            if (fireTimer >= fireCoolDown)
            {
                isFired = false;
                SetDefaultColor();
            }
        }
        else return;
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


    // ================ Die =========================
    protected virtual void Die()  // biến isDead tránh frozen fire explosive bullet gọi takedamage -> die -> addEnemyKilled cộng dồn
    {
        if (!isDead)
        {
            isDead = true;
            Destroy(gameObject);
        }
        else return;
    }    

}
