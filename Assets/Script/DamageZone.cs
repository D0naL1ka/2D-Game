// Scripts/DamageZone.cs
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FallRespawn.Instance.RespawnToCheckpoint();
            Debug.Log("Гравець у зоні небезпеки! Respawn на чекпоінт.");
        }
    }
}