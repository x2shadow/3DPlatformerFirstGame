using UnityEngine;

public class PlatformInteractionHandler : MonoBehaviour
{
    private MovingPlatform currentPlatform;
    private Vector3 platformMovement;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            currentPlatform = collision.gameObject.GetComponent<MovingPlatform>();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (currentPlatform != null)
        {
            platformMovement = currentPlatform.GetPlatformMovement();
            transform.position += platformMovement;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            currentPlatform = null;
            platformMovement = Vector3.zero;
        }
    }
}
