using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Перевіряємо, чи торкнувся монети гравець
        if (collision.gameObject.CompareTag("Player"))
        {
            // Можна додати звук або ефект тут
            Debug.Log("Монету зібрано гравцем!");
            Destroy(gameObject);
        }
    }
}
