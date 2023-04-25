using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;


    [Header("Dash Variables")]
    public float dashingVelocity = 14f;
    public float dashingTime = 0.3f;

    public float dashingCooldown = 0.5f;

    public float distanceBetweenImages;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;

    Animator myAnimator;
    Vector2 moveInput;
    Rigidbody2D myrigidbody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    float gravityAtStart;

    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityAtStart = myrigidbody.gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        Run();       
        FlipSprite();
        ClimbLadder();

        var dashInput = Input.GetButtonDown("Dash");

        if (dashInput && canDash)
        {
            isDashing = true;
            canDash = false;

            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, 0f);
            }
            StartCoroutine(StopDashing());

        }

        if (isDashing)
        {
            myrigidbody.velocity = dashingDir.normalized * dashingVelocity;
            return;
        }
        
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {   
            return; 
        }

        if (value.isPressed)
        {
            myrigidbody.velocity += new Vector2(1f, jumpSpeed);
            myAnimator.SetTrigger("IsJumping");
        } 
    }

    public void Run()
    {
        Vector2 PlayerVelocity = new Vector2 (moveInput.x * runSpeed, myrigidbody.velocity.y);
        myrigidbody.velocity = PlayerVelocity;

        bool isMyManJumping = Mathf.Abs(myrigidbody.velocity.y) > Mathf.Epsilon;  

        bool isMyManRunning = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", isMyManRunning); 
    }

    void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myrigidbody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myrigidbody.gravityScale = gravityAtStart;
            return; 
        }
        
        Vector2 climbVelocity = new Vector2 (myrigidbody.velocity.x, moveInput.y * climbSpeed);
        myrigidbody.velocity = climbVelocity; 

        myrigidbody.gravityScale = 0f; 
    }
    
    void JumpCheck()
    {
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("IsJumping", false);
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    
}