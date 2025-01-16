using Unity.Mathematics;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 endPosition;

    [SerializeField] Transform target;

    public float speed = 2f;

    private Vector3 direction;
    private Vector3 previousPosition;

    [HideInInspector]
    public Vector3 platformVelocity; // Вектор скорости платформы

    private void Start()
    {
        startPosition = transform.position;
        endPosition   = target.position;

        //Отключаем конечное положение
        target.gameObject.SetActive(false);

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

    void OnDrawGizmos()
    {
        if (target != null)
        {
            // Установить цвет Gizmos для платформы
            Gizmos.color = Color.cyan;

            // Если игра не запущена, используем позицию target
            Vector3 positionToDraw = Application.isPlaying ? endPosition : target.position;

            Gizmos.DrawWireCube(positionToDraw, target.localScale);
        }
    }
}
