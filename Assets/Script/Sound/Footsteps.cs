using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private float stepInterval = 0.35f;

    private float stepTimer = 0f;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (playerController.IsGrounded &&
            Mathf.Abs(GetComponent<Rigidbody2D>().linearVelocity.x) > 0.1f)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                footstepSource.Play();
                stepTimer = 0f;
            }
        }
        else { stepTimer = 0f; }
    }
}
