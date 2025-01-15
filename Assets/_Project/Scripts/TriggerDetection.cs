using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class TriggerDetection : MonoBehaviour
    {
        [SerializeField] PlatformCollisionHandler platformCollisionHandler;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("На платформе");
                platformCollisionHandler.IsOnPlatform = true;
                platformCollisionHandler.movingPlatform = gameObject.GetComponentInParent<MovingPlatform>();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Сошел с платформы");
                platformCollisionHandler.IsOnPlatform = false;
                platformCollisionHandler.movingPlatform = null;
            }
        }
    }
}
