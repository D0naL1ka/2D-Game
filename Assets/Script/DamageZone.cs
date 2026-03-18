using UnityEngine;



public class DamageZone : MonoBehaviour

{

    [Header("Damage Settings")]

    [SerializeField] private float energyDrainPerSecond = 10f;



    private void OnTriggerStay2D(Collider2D other)

    {

        if (other.CompareTag("Player"))

        {

            if (GameManager.Instance != null)

            {
                GameManager.Instance.DrainEnergy(energyDrainPerSecond * Time.deltaTime);

            }

        }

    }



    private void OnTriggerEnter2D(Collider2D other)

    {

        if (other.CompareTag("Player"))

        {
            PlatformerSoundManager.Instance.PlayDamage();

            Debug.Log("Гравець увійшов у DamageZone");

            // FallRespawn.Instance.RespawnToLastCheckpoint();

        }

    }



    private void OnTriggerExit2D(Collider2D other)

    {

        if (other.CompareTag("Player"))

        {

            Debug.Log("Гравець вийшов з DamageZone");

        }

    }

}