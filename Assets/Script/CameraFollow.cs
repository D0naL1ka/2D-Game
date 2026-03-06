using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // Об'єкт, за яким стежимо (гравець)
    public float smoothSpeed = 0.125f; // Швидкість згладжування
    public Vector3 offset;         // Зміщення (наприклад, 0, 0, -10)

    void FixedUpdate()
    {
        if (target != null)
        {
            // Визначаємо бажану позицію
            Vector3 desiredPosition = target.position + offset;

            // Плавно переміщуємо камеру до цієї позиції
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Оновлюємо позицію камери
            transform.position = smoothedPosition;
        }
    }
}