
using UnityEngine;

public class ChildToPlataform : MonoBehaviour
{

    private bool isOnMovingPlatform = false;
    private Vector3 lastPlatformPosition;

    private void Update()
    {
        if (isOnMovingPlatform)
        {
            // Apply the movement of the platform to the character.
            Vector3 platformMovement = transform.parent.position - lastPlatformPosition;
            transform.position += platformMovement;

            // Update the last platform position.
            lastPlatformPosition = transform.parent.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            isOnMovingPlatform = true;
            lastPlatformPosition = other.transform.position;
            transform.parent = other.transform; // Attach character to the platform.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            isOnMovingPlatform = false;
            transform.parent = null; // Detach character from the platform.
        }
    }

}
