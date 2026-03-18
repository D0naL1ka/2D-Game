using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [Header("Y Clamp (prevent out of frustum)")]
    [SerializeField] private float minCameraY = -10f;
    [SerializeField] private float maxCameraY = 10f;

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;

            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minCameraY, maxCameraY);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}