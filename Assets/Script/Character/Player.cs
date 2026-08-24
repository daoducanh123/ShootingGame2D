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
    }

    void FixedUpdate()
    {
        PlayerMovement();    
    }

    #region Input & Movement
    private void ReadInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
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
    private void UpdateState()
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

    private void ChangeState(PlayerState playerState)
    {
        if (currentState == playerState) return;
        else
        {
            currentState = playerState;
        }
    }
    private void HandleIdleState()
    {
        animator.SetBool("isIdle", true);
        animator.SetBool("isRunning", false);

        if (input != Vector3.zero)
        {
            ChangeState(PlayerState.Running);
        }

    }
    private void HandleRunningState()
    {
        animator.SetBool("isRunning", true);
        animator.SetBool("isIdle", false);
        if (input == Vector3.zero)
        {
            ChangeState(PlayerState.Idle);
        }
    }
    #endregion

}
