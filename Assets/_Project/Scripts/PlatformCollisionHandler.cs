using System;
using UnityEngine;

namespace Platformer
{
    public class PlatformCollisionHandler : MonoBehaviour
    {
        public PlatformMover platformMover;
        public MovingPlatform movingPlatform;
        public bool IsOnPlatform;
        
        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                // If the contact normal is pointing up, we've collided with the top of the platform
                ContactPoint contact = other.GetContact(0);
                if (contact.normal.y < 0.5f) return;
                
                
            }            
        }
        
        void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                //platformMover = null;
                //movingPlatform = null;
            }            
        }
    }
}
