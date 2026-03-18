using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Налаштування руху")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private Vector2 moveOffset = new Vector2(5f, 0f);

    private Rigidbody2D rb;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private bool movingToTarget = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0f;
            rb.freezeRotation = true;
        }

        startPosition = transform.position;
        targetPosition = startPosition + moveOffset;
    }

    void FixedUpdate()
    {
        Vector2 currentPos = transform.position;
        Vector2 target = movingToTarget ? targetPosition : startPosition;

        if (Vector2.Distance(currentPos, target) < 0.05f)
        {
            movingToTarget = !movingToTarget;
            return;
        }

        Vector2 newPos = Vector2.MoveTowards(currentPos, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }
}