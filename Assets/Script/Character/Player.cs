using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR;
using System;
public class Player : Character
{
    private Vector3 input;
    
    #region StateMachine Pattern
    private enum PlayerState { Running, Idle };
    private PlayerState currentState;
    #endregion

    #region Singleton Pattern
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
    #endregion

    #region Observer Pattern
    public event Action OnPlayerDeath;
    #endregion

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
        if(isDead) return;
        
        base.Die();
        OnPlayerDeath?.Invoke();
    }
}
