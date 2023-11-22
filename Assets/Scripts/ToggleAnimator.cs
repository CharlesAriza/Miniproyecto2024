using UnityEngine;

public class ToggleAnimator : MonoBehaviour
{
    private Animator playerAnimator;

    public float delayTime = 2f; // Tiempo de espera antes de activar el Animator

    void Start()
    {
        // Obt�n la referencia al componente Animator al inicio
        playerAnimator = GetComponent<Animator>();

        // Verifica si se encontr� el componente Animator
        if (playerAnimator == null)
        {
            Debug.LogError("El objeto no tiene un componente Animator.");
        }

        // Desactiva el componente Animator al inicio
        DeactivateAnimator();

        // Luego de un tiempo, activa el componente Animator
        Invoke("ActivateAnimator", delayTime);
    }

    void ActivateAnimator()
    {
        // Verifica si el componente Animator est� presente
        if (playerAnimator != null)
        {
            // Activa el componente Animator
            playerAnimator.enabled = true;

            // Si deseas realizar alguna acci�n adicional al activar el Animator, agr�galo aqu�.
        }
    }

    void DeactivateAnimator()
    {
        // Verifica si el componente Animator est� presente
        if (playerAnimator != null)
        {
            // Desactiva el componente Animator
            playerAnimator.enabled = false;

            // Si deseas realizar alguna acci�n adicional al desactivar el Animator, agr�galo aqu�.
        }
    }
}