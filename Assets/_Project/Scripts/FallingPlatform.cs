using UnityEngine;
using KBCore.Refs;

public class FallingPlatform : ValidatedMonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 previousPosition;
    [SerializeField, Self] Rigidbody rb;

    public bool isFalling = false;

    [SerializeField] private float fallDelay = 2f;
    [SerializeField] private float respawnDelay = 2f;

    [HideInInspector]
    public Vector3 platformVelocity; // Вектор скорости платформы

    void Start()
    {
        rb.isKinematic = true;

        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        // Рассчитываем скорость платформы
        Vector3 currentPlatformPosition = transform.position;
        platformVelocity = (currentPlatformPosition - previousPosition) / Time.fixedDeltaTime;

        // Обновление позиции
        previousPosition = transform.position;
    }

    public void StartFallProcess()
    {
        //Debug.Log("StartFallProcess вызван");

        if (isFalling)
        {
            //Debug.LogWarning("Платформа уже падает!");
            return;
        }

        //Debug.Log("Игрок активировал падение платформы.");
        Invoke(nameof(StartFalling), fallDelay);
        isFalling = true; // Проверяем, что это состояние изменяется
        //Debug.Log("isFalling: " + isFalling);
    }


    private void StartFalling()
    {
        //Debug.Log("Платформа начинает падать.");
        rb.isKinematic = false;
        Invoke(nameof(Respawn), respawnDelay);
    }

    private void Respawn()
    {
        //Debug.Log("Платформа восстанавливается.");
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        transform.position = initialPosition;
        transform.rotation = initialRotation;
        isFalling = false;
        //Debug.Log("Платформа восстановлена.");
    }

}
