using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlatformerSoundManager.Instance.PlayCoin();
            Debug.Log("Монету зібрано гравцем!");
            Destroy(gameObject);
        }
    }
}