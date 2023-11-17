using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
interface IInteractable
{
    public void Interact();
}
public class Interactor : MonoBehaviour
{
    public Transform InteracterSource;
    public float InteractRange;

    public TextMeshProUGUI interactText; // Referencia al objeto de texto en la UI
    private bool canInteract = false;


    private void Start()
    {
        InteracterSource = PlayerHelperInicializator.Singleton.GetComponent<UnityEngine.Transform>();
        interactText =  PlayerHelperInicializator.Singleton.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            // Realiza la interacción con el objeto
            Ray r = new Ray(InteracterSource.position, InteracterSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractableObject"))
        {
            interactText.text = "Press E to Interact";
            interactText.gameObject.SetActive(true);
            canInteract = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractableObject"))
        {
            interactText.gameObject.SetActive(false);
            canInteract = false;
        }
    }
}

