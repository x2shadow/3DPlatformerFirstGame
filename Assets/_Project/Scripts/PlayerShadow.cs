using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform player; // Ссылка на трансформ игрока
    [SerializeField] GameObject shadowObject; // Объект тени
    [SerializeField] float shadowHeightOffset  = 0.01f; // Высота тени от земли
    [SerializeField] LayerMask groundLayer; // Слой, по которому будет определяться поверхность
    [SerializeField] float raycastMaxDistance = 50f;

    [Header("Settings")]
    [SerializeField] float shadowSize = 0.55f; // Размер тени
    [SerializeField] float sphereRadius = 0.4f; // Радиус луча
    [SerializeField] float shadowScaleSpeed = 0.4f;  
    [SerializeField] private float lerpSpeed = 30f;

    void FixedUpdate()
    {
        SpherecastCheck();
    }

    void SpherecastCheck()
    {
        if (player != null && shadowObject != null)
        {
            if (Physics.SphereCast(player.position + Vector3.up, sphereRadius, Vector3.down, out RaycastHit hit, raycastMaxDistance, groundLayer))
            {
                Vector3 shadowPosition = new Vector3(player.position.x, hit.point.y + shadowHeightOffset, player.position.z);

                // Проверяем, есть ли компонент MovingPlatform на объекте
                MovingPlatform movingPlatform = hit.collider.GetComponent<MovingPlatform>();
                if (movingPlatform != null)
                {
                    // Если игрок на платформе, привязываем тень к платформе
                    Vector3 platformPosition = movingPlatform.transform.position;
                    Vector3 playerOffset = player.position - platformPosition;
                    
                    shadowPosition = platformPosition + new Vector3(playerOffset.x, 0.3f, playerOffset.z);
                }
                
                // Линейная интерполяция для осей X и Z
                Vector3 smoothPosition = new Vector3(
                    Mathf.Lerp(shadowObject.transform.position.x, shadowPosition.x, lerpSpeed * Time.deltaTime),
                    shadowPosition.y,
                    Mathf.Lerp(shadowObject.transform.position.z, shadowPosition.z, lerpSpeed * Time.deltaTime)
                );

                // Применяем сглаженную позицию к тени
                shadowObject.transform.position = smoothPosition;

                // Изменение размера тени в зависимости от расстояния до поверхности
                float distanceToGround = hit.distance;
                shadowObject.transform.localScale = Vector3.one * (shadowSize / Mathf.Max(distanceToGround * shadowScaleSpeed, 1f));
            }
        }
    }
}
