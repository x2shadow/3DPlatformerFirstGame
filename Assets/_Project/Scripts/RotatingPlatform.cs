using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class RotatingPlatform : MonoBehaviour
    {
        private Vector3 previousPosition;

        [HideInInspector]
        public Vector3 platformVelocity; // Вектор скорости платформы

        [Header("Rotation Settings")]
        public float rotationSpeedX = 0f;
        public float rotationSpeedY = 0f;
        public float rotationSpeedZ = 0f;

        void FixedUpdate()
        {
            // Рассчитываем скорость платформы
            Vector3 currentPlatformPosition = transform.position;
            platformVelocity = (currentPlatformPosition - previousPosition) / Time.fixedDeltaTime;

            // Обновление позиции
            previousPosition = transform.position;

            // Поворачиваем платформу вокруг оси Y
            transform.Rotate(rotationSpeedX * Time.fixedDeltaTime, rotationSpeedY * Time.fixedDeltaTime, rotationSpeedZ * Time.fixedDeltaTime, Space.Self);
        }
    }
}
