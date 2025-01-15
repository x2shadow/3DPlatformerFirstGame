using UnityEngine;

namespace Platformer
{
    public class PlatformChecker : MonoBehaviour
    {
        [SerializeField] float platformDistance = 0.18f;

        public bool IsOnPlatform;

        void FixedUpdate()
        {
            Ray ray = new Ray(transform.position, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, platformDistance))
            {
                // Проверяем тег объекта, с которым столкнулся луч
                IsOnPlatform = hit.collider.CompareTag("MovingPlatform");
            }
        }
    }
}
