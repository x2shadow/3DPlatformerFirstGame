using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class JumpingPlatform : MonoBehaviour
    {
        private Vector3 previousPosition;

        [HideInInspector]
        public Vector3 platformVelocity; // Вектор скорости платформы
        
        void FixedUpdate()
        {
            // Рассчитываем скорость платформы
            Vector3 currentPlatformPosition = transform.position;
            platformVelocity = (currentPlatformPosition - previousPosition) / Time.fixedDeltaTime;

            // Обновление позиции
            previousPosition = transform.position;
        }
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
