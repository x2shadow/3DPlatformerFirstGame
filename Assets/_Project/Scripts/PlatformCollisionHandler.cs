using System;
using UnityEngine;

namespace Platformer
{
    public class PlatformCollisionHandler : MonoBehaviour
    {
        public MovingPlatform movingPlatform;
        public FallingPlatform fallingPlatform;
        public JumpingPlatform jumpingPlatform;
        public RotatingPlatform rotatingPlatform;


        public bool IsOnPlatform;
        
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MovingPlatform"))
            {
                //Debug.Log("На платформе");
                IsOnPlatform = true;
                movingPlatform = other.gameObject.GetComponentInParent<MovingPlatform>();
            }

            if (other.CompareTag("FallingPlatform"))
            {
                IsOnPlatform = true;
                fallingPlatform = other.gameObject.GetComponentInParent<FallingPlatform>();
                fallingPlatform.StartFallProcess();
            }

            if (other.CompareTag("JumpingPlatform"))
            {
                IsOnPlatform = true;
                GetComponent<PlayerController>().StartJumpingPlatformBoost();
                jumpingPlatform = other.gameObject.GetComponentInParent<JumpingPlatform>();
            }

            if (other.CompareTag("RotatingPlatform"))
            {
                IsOnPlatform = true;
                rotatingPlatform = other.gameObject.GetComponentInParent<RotatingPlatform>();
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

            if (other.CompareTag("FallingPlatform"))
            {
                IsOnPlatform = false;
                fallingPlatform = null;
            }

            if (other.CompareTag("JumpingPlatform"))
            {
                IsOnPlatform = false;
                jumpingPlatform = null;
            }

            if (other.CompareTag("RotatingPlatform"))
            {
                IsOnPlatform = false;
                rotatingPlatform = null;
            }
        }
    }
}
