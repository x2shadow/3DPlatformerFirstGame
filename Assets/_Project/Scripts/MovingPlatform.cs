using Unity.Mathematics;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float speed = 2f;

    private Vector3 direction;
    private Vector3 previousPosition;

    public Vector3 platformVelocity; // Скорость платформы

    private void Start()
    {
        startPosition = transform.position;
        previousPosition = startPosition;
        direction = (endPosition - startPosition).normalized;
    }

    void FixedUpdate()
    {
        // Движение платформы
        float distance = Vector3.Distance(startPosition, endPosition);
        transform.position = startPosition + direction * Mathf.PingPong(Time.time * speed, distance);

            // Рассчитываем скорость платформы
            Vector3 currentPlatformPosition = transform.position;
            platformVelocity = (currentPlatformPosition - previousPosition) / Time.fixedDeltaTime;

        // Обновление позиции
        previousPosition = transform.position;
    }

    public Vector3 GetPlatformMovement()
    {
        return transform.position - previousPosition;
    }
}
