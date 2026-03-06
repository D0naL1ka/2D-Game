// Scripts/Checkpoint.cs
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

        FallRespawn.Instance.SetCheckpoint(transform.position + Vector3.up * 1f);
        Debug.Log("Чекпоінт активовано!");
    }
}