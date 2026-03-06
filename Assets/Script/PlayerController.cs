using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Компоненти")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform groundCheck;

    [Header("Рух та стрибок")]
    [SerializeField] private float runSpeed = 40f;
    [SerializeField] private float jumpForce = 400f;
    [Range(0, .3f)][SerializeField] private float movementSmoothing = .05f;
    [SerializeField] private bool airControl = false;

    [Header("Перевірка землі")]
    [SerializeField] private LayerMask whatIsGround;
    private const float groundedRadius = 0.2f;
    private bool isGrounded;
    private bool wasGrounded;

    [Header("Події")]
    public UnityEvent OnLandEvent;

    // Внутрішні змінні
    private Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    private bool facingRight = true;

    // Input System
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    private float horizontalMove = 0f;
    private bool jumpPressedThisFrame = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Ініціалізація Input System
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            moveAction = playerInput.actions["Move"];
            jumpAction = playerInput.actions["Jump"];
        }

        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }

    private void Update()
    {
        if (moveAction != null)
        {
            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            horizontalMove = moveInput.x * runSpeed;
        }

        // Анімація швидкості
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }

        // Стрибок
        if (jumpAction != null && jumpAction.WasPressedThisFrame())
        {
            jumpPressedThisFrame = true;
            if (animator != null)
            {
                animator.SetBool("isJumping", true);
            }
        }
    }

    private void FixedUpdate()
    {
        // Перевірка землі
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
                    {
                        animator.SetBool("isJumping", false);
                    }
                }
            }
        }

        // Рух
        if (isGrounded || airControl)
        {
            Vector3 targetVelocity = new Vector2(horizontalMove * 10f, rb.linearVelocity.y);
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, movementSmoothing);

            // Поворот персонажа
            if (horizontalMove > 0 && !facingRight) Flip();
            else if (horizontalMove < 0 && facingRight) Flip();
        }

        // Стрибок
        if (isGrounded && jumpPressedThisFrame)
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpPressedThisFrame = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Підтримка рухомої платформи
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }

    // Для зручності в інспекторі (опціонально)
    public bool IsGrounded => isGrounded;
}