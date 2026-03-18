using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BouncePlatform : MonoBehaviour
{
    [Header("Bounce Settings")]
    [SerializeField] private float bounceJumpHeight = 4f;
    [SerializeField] private float friction = 0.3f;

    public float BounceJumpHeight => bounceJumpHeight;

    void Start()
    {
        var col = GetComponent<Collider2D>();
        var mat = new PhysicsMaterial2D("BounceMaterial")
        {
            friction = friction,
            bounciness = 0f,
            frictionCombine = PhysicsMaterialCombine2D.Minimum,
            bounceCombine = PhysicsMaterialCombine2D.Minimum
        };
        col.sharedMaterial = mat;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            PlatformerSoundManager.Instance.PlayBounce();
    }
}