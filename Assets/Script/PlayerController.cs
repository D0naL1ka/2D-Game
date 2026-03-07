using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform groundCheck;

    [Header("Movement")]
    [SerializeField] private float runSpeed = 40f;
    [Range(0, .3f)][SerializeField] private float movementSmoothing = .05f;
    [SerializeField] private bool airControl = true;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 3f;             // normal jump height in meters

    [Header("Ground Check")]
    [SerializeField] private LayerMask whatIsGround;
    private const float groundedRadius = 0.2f;
    private bool isGrounded;
    private bool wasGrounded;

    [Header("Events")]
    public UnityEvent OnLandEvent;

    private Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    private bool facingRight = true;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    private float horizontalMove = 0f;
    private bool jumpPressedThisFrame = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            moveAction = playerInput.actions["Move"];
            jumpAction = playerInput.actions["Jump"];
        }

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void Update()
    {
        if (moveAction != null)
        {
            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            horizontalMove = moveInput.x * runSpeed;
        }

        if (animator != null)
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (jumpAction != null && jumpAction.WasPressedThisFrame())
        {
            jumpPressedThisFrame = true;
            if (animator != null)
                animator.SetBool("isJumping", true);
        }
    }

    private void FixedUpdate()
    {
        wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent?.Invoke();
                    if (animator != null)
                        animator.SetBool("isJumping", false);
                }
            }
        }

        if (isGrounded || airControl)
        {
            Vector3 targetVelocity = new Vector2(horizontalMove * 10f, rb.linearVelocity.y);
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, movementSmoothing);

            if (horizontalMove > 0 && !facingRight) Flip();
            else if (horizontalMove < 0 && facingRight) Flip();
        }

        if (isGrounded && jumpPressedThisFrame)
        {
            isGrounded = false;
            PerformJump(jumpHeight);
            jumpPressedThisFrame = false;
        }
    }

    private void PerformJump(float height)
    {
        float gravity = Mathf.Abs(Physics2D.gravity.y);
        float jumpVelocity = Mathf.Sqrt(2 * gravity * height);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.transform);
        }

        // BouncePlatform
        if (collision.gameObject.TryGetComponent<BouncePlatform>(out var bounce))
        {
            if (rb.linearVelocity.y <= 0.1f)
            {
                PerformJump(bounce.BounceJumpHeight);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public bool IsGrounded => isGrounded;
}