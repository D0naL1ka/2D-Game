using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool oneTimeUse = true;
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (oneTimeUse && activated) return;
        activated = true;
        PlatformerSoundManager.Instance.PlayCheckpoint();
        FallRespawn.Instance.SetCheckpoint(transform.position + Vector3.up * 1f);
    }
}