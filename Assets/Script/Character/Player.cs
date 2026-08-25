using UnityEngine;
using UnityEngine.XR;

public class Player : Character
{
    private Vector3 input;
    private enum PlayerState { Running, Idle };
    private PlayerState currentState;

    public static Player Instance { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }

    protected override void Update()
    {   
        base.Update();
        ReadInput();
        UpdateState();
        HandleState();
    }

    void FixedUpdate()
    {
        PlayerMovement();    
    }

    #region Input & Movement
    private void ReadInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameManager == null) return;
            gameManager.PauseMenu();
        }
    }

    private void PlayerMovement()
    {
        if (rb == null) return;
     
        rb.linearVelocity = input.normalized * moveSpeed; 
                                                            
        if (input.x < 0)
        {
            spriteRenderer.flipX = true; 
        }
        else if (input.x > 0) 
        {
            spriteRenderer.flipX = false; 
        }
    }
    #endregion

    #region Player State
    private void HandleState()
    {
        switch (currentState)
        {
            case PlayerState.Idle:
                HandleIdleState();
                break;
            case PlayerState.Running:
                HandleRunningState();
                break;
        }
    }

    private void UpdateState()
    {
        switch (currentState)
        {
            case PlayerState.Idle:
                if (input != Vector3.zero)
                {
                    currentState = PlayerState.Running;
                }
                break;
            case PlayerState.Running:
                if (input == Vector3.zero)
                {
                    currentState = PlayerState.Idle;
                }
                break;
        }
    }


    private void HandleIdleState()
    {
        animator.SetBool("isIdle", true);
        animator.SetBool("isRunning", false);
    }
    private void HandleRunningState()
    {
        animator.SetBool("isRunning", true);
        animator.SetBool("isIdle", false);
    }
    #endregion

    protected override void Die()
    {
        base.Die();
        if (gameManager == null) return;
        gameManager.GameOverMenu();
    }
}
