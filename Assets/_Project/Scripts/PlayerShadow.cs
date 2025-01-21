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

    void RaycastCheck()
    {
        if (player != null && shadowObject != null)
        {
            // Выполняем лучевой каст вниз от позиции игрока
            if (Physics.Raycast(player.position + Vector3.up, Vector3.down, out RaycastHit hit, raycastMaxDistance, groundLayer))
            {
                // Устанавливаем позицию тени на уровень пересечения с поверхностью
                Vector3 shadowPosition = new Vector3(player.position.x, hit.point.y + shadowHeightOffset, player.position.z);
                shadowObject.transform.position = shadowPosition;

                // Изменение размера тени в зависимости от расстояния до поверхности
                float distanceToGround = hit.distance;
                shadowObject.transform.localScale = Vector3.one * (shadowSize / Mathf.Max(distanceToGround * shadowScaleSpeed, 1f));
            }
        }
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
                    shadowPosition = new Vector3(shadowPosition.x, movingPlatform.gameObject.transform.position.y + 0.055f, shadowPosition.z);
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
