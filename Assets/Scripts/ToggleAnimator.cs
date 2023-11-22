using UnityEngine;

public class ToggleAnimator : MonoBehaviour
{
    private Animator playerAnimator;

    public float delayTime = 2f; // Tiempo de espera antes de activar el Animator

    void Start()
    {
        // Obtén la referencia al componente Animator al inicio
        playerAnimator = GetComponent<Animator>();

        // Verifica si se encontró el componente Animator
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
        // Verifica si el componente Animator está presente
        if (playerAnimator != null)
        {
            // Activa el componente Animator
            playerAnimator.enabled = true;

            // Si deseas realizar alguna acción adicional al activar el Animator, agrégalo aquí.
        }
    }

    void DeactivateAnimator()
    {
        // Verifica si el componente Animator está presente
        if (playerAnimator != null)
        {
            // Desactiva el componente Animator
            playerAnimator.enabled = false;

            // Si deseas realizar alguna acción adicional al desactivar el Animator, agrégalo aquí.
        }
    }
}