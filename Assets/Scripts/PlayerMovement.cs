using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem;
using Cinemachine;


[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;


    [Header("Dash Variables")]
    public float dashingVelocity = 14f;
    public float dashingTime = 0.3f;
    public float dashingCooldown = 0.5f;
    public float maxDashVerticalVelocity = 5f;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;


    [Header ("References")]
    Animator myAnimator;
    Vector2 moveInput;
    Rigidbody2D Rb;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    float gravityAtStart;

    [Header ("CameraShake")]
    public float mag = 0.3f;
    public float tims = 0.4f;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityAtStart = Rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();       
        FlipSprite();

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
    Vector2 velocity = dashingDir.normalized * dashingVelocity;
    if (velocity.y > maxDashVerticalVelocity)
    {
        velocity.y = maxDashVerticalVelocity;
    }
    Rb.velocity = velocity;
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
            Rb.velocity = new Vector2(Rb.velocity.x, jumpSpeed);
            myAnimator.SetTrigger("IsJumping");
        } 
    }

    public void Run()
    {
        Vector2 PlayerVelocity = new Vector2 (moveInput.x * runSpeed, Rb.velocity.y);
        Rb.velocity = PlayerVelocity;

        bool isMyManJumping = Mathf.Abs(Rb.velocity.y) > Mathf.Epsilon;  

        bool isMyManRunning = Mathf.Abs(Rb.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", isMyManRunning); 
    }

    void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(Rb.velocity.x) > Mathf.Epsilon;

        if (playerHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(Rb.velocity.x), 1f);
        }
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