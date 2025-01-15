using System;
using DG.Tweening;
using UnityEngine;

namespace Platformer
{
    public class PlatformMover : MonoBehaviour
    {
        [SerializeField] Vector3 moveTo = Vector3.zero;
        [SerializeField] float moveTime = 1f;
        [SerializeField] Ease ease = Ease.InOutQuad;

        Vector3 startPosition;

        public Vector3 platformVelocity; // Скорость платформы
        Vector3 lastPlatformPosition; // Позиция платформы в предыдущем кадре

        void FixedUpdate()
        {
            // Рассчитываем скорость платформы
            Vector3 currentPlatformPosition = transform.position;
            platformVelocity = (currentPlatformPosition - lastPlatformPosition) / Time.fixedDeltaTime;
            lastPlatformPosition = currentPlatformPosition;
        }

        void Start()
        {
            startPosition = transform.position;
            Move();
        }

        void Move()
        {
            transform.DOMove(startPosition + moveTo, moveTime)
                .SetEase(ease)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}
