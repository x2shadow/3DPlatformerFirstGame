using System;
using UnityEngine;

namespace Platformer
{
    public class PlatformCollisionHandler : MonoBehaviour
    {
        public MovingPlatform movingPlatform;
        public bool IsOnPlatform;
        
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MovingPlatform"))
            {
                //Debug.Log("На платформе");
                IsOnPlatform = true;
                movingPlatform = other.gameObject.GetComponentInParent<MovingPlatform>();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("MovingPlatform"))
            {
                //Debug.Log("Сошел с платформы");
                IsOnPlatform = false;
                movingPlatform = null;
            }
        }
    }
}
