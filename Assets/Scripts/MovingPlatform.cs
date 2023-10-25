using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveDistance = 1.0f; // La distancia que la plataforma se moverá hacia arriba y hacia abajo.
    public float moveSpeed = 1.0f; // La velocidad a la que la plataforma se mueve.

    private Vector3 startPosition;
    private bool movingUp = true;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calcula la nueva posición de la plataforma.
        Vector3 newPosition = transform.position;

        if (movingUp)
        {
            newPosition.y += moveSpeed * Time.deltaTime;
            if (newPosition.y >= startPosition.y + moveDistance)
            {
                movingUp = false;
            }
        }
        else
        {
            newPosition.y -= moveSpeed * Time.deltaTime;
            if (newPosition.y <= startPosition.y)
            {
                movingUp = true;
            }
        }

        // Actualiza la posición de la plataforma.
        transform.position = newPosition;
    }
}
