using UnityEngine;

public class Player : Character
{
    protected override void Awake()
    {
        base.Awake();
    }


    protected override void Update()
    {   
        base.Update();
        ReadInput();
        CheckAnimation();
    }

    void FixedUpdate()
    {
        PlayerMovement();    
    }

    // =============== Read Input Avoid Lagging  =================
    private Vector3 input;
    private void ReadInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
    }

    // ================ PLAYER MOVEMENT  =================
    private void PlayerMovement()
    {
        if (rb != null)
        {
            rb.linearVelocity = input.normalized * moveSpeed; // ko cần * Time.deltaTime vì đã dùng velocity
                                                                // rb.velocity = new Vector2(input.x * moveSpeed , input.y * moveSpeed ); // ko cần * Time.deltaTime vì đã dùng velocity && cái này ko có normalized nên sẽ nhanh hơn khi đi chéo
            if (input.x < 0)
            {
                spriteRenderer.flipX = true; 
            }
            else if (input.x > 0) 
            {
                spriteRenderer.flipX = false; 
            }

        }
     
    }

    // ==================== Player State ===================

    private void CheckAnimation()
    {
        if (input != Vector3.zero)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

    }
}
