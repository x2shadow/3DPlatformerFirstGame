using System;
using System.Collections;
using KBCore.Refs;
using UnityEngine;

namespace Platformer
{
    public class CameraManager : ValidatedMonoBehaviour
    {
        [Header("References")]
        [SerializeField, Anywhere] InputReader input;
        [SerializeField] Transform playerTransform; // Ссылка на игрока
        [SerializeField] Camera mainCamera;

        [Header("Setting")]
        [SerializeField] Vector3 offset = new Vector3(0f, 8f, -10f); // Смещение камеры относительно игрока
        [SerializeField] float followSpeed = 5000f; // Скорость следования камеры
        [SerializeField, Range(0.5f, 10f)] float speedMultiplier = 3f;

        [Header("Y Clamp")]
        [SerializeField] float minY = 5f;
        [SerializeField] float maxY = 85f;

        //bool isRMBPressed = true;
        bool cameraMovementLock;

        Vector2 currentRotation; // Хранение текущей ориентации камеры

        void Start()
        {
            // Lock the cursor to the center of the screen and hide it
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Инициализация текущей ориентации камеры
            //currentRotation = new Vector2(mainCamera.transform.eulerAngles.y, mainCamera.transform.eulerAngles.x);
            currentRotation = new Vector2(180f, mainCamera.transform.eulerAngles.x);
        }

        void LateUpdate()
        {
            FollowPlayerWithRotation();
        }

        void FollowPlayer()
        {
            if (playerTransform == null) return;

            // Рассчитываем желаемую позицию камеры
            Vector3 targetPosition = playerTransform.position + offset;

            // Плавно перемещаем камеру к этой позиции
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, followSpeed * Time.deltaTime);
            //mainCamera.transform.position = targetPosition;


            // Если нужно, чтобы камера всегда смотрела на игрока:
            mainCamera.transform.LookAt(playerTransform);
        }

        void FollowPlayerWithRotation()
        {
            if (playerTransform == null) return;

            // Вычисляем радиус на основе вертикального угла
            float baseRadius = offset.magnitude; // Базовый радиус (изначальное расстояние)
            float verticalFactor = Mathf.InverseLerp(minY, maxY, currentRotation.y); // Нормализуем угол
            float dynamicRadius = Mathf.Lerp(baseRadius * 0.5f, baseRadius, verticalFactor); // Радиус уменьшается, когда камера опускается

            // Конвертируем углы в позицию камеры в мировых координатах
            float horizontalAngle = currentRotation.x; // Горизонтальный угол
            float verticalAngle = currentRotation.y;   // Вертикальный угол

            Vector3 offsetPosition = new Vector3(
                dynamicRadius * Mathf.Cos(verticalAngle * Mathf.Deg2Rad) * Mathf.Sin(horizontalAngle * Mathf.Deg2Rad),
                dynamicRadius * Mathf.Sin(verticalAngle * Mathf.Deg2Rad),
                dynamicRadius * Mathf.Cos(verticalAngle * Mathf.Deg2Rad) * Mathf.Cos(horizontalAngle * Mathf.Deg2Rad)
            );

            // Устанавливаем новую позицию камеры
            mainCamera.transform.position = Vector3.Lerp(
                mainCamera.transform.position,
                playerTransform.position + offsetPosition,
                followSpeed * Time.deltaTime
            );

            // Направляем камеру на игрока
            mainCamera.transform.LookAt(playerTransform.position);
        }

        void OnEnable()
        {
            input.Look += OnLook;
            //input.EnableMouseControlCamera  += OnEnableMouseControlCamera;
            //input.DisableMouseControlCamera += OnDisableMouseControlCamera;
        }

        void OnDisable()
        {
            input.Look -= OnLook;
            //input.EnableMouseControlCamera -= OnEnableMouseControlCamera;
            //input.DisableMouseControlCamera -= OnDisableMouseControlCamera;
        }

        void OnLook(Vector2 cameraMovement, bool isDeviceMouse)
        {
            if (cameraMovementLock) return;

            //if (isDeviceMouse && !isRMBPressed) return;

            if (PauseManager.Instance.isPaused) return;
            
            // If the device is mouse use fixedDeltaTime, otherwise use deltaTime
            float deviceMultiplier = isDeviceMouse ? Time.fixedDeltaTime :  Time.deltaTime; //Time.fixedDeltaTime for all devices?

            // Изменяем текущую ориентацию камеры
            currentRotation.x += cameraMovement.x * speedMultiplier * deviceMultiplier;
            currentRotation.y -= cameraMovement.y * speedMultiplier * deviceMultiplier;

            // Ограничиваем вращение по вертикали
            currentRotation.y = Mathf.Clamp(currentRotation.y, minY, maxY); // Вертикальный угол
        }

        public void OnEnableMouseControlCamera()
        {
            //isRMBPressed = true;

            // Lock the cursor to the center of the screen and hide it
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            StartCoroutine(DisableMouseForFrame());
        }

        IEnumerator DisableMouseForFrame()
        {
            cameraMovementLock = true;
            yield return new WaitForEndOfFrame();
            cameraMovementLock = false;
        }

        public void OnDisableMouseControlCamera()
        {
            //isRMBPressed = false;

            // Lock the cursor to the center of the screen and hide it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
