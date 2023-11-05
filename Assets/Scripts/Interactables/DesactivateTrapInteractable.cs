using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivateTrapInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject fireTrap;
    private bool trapActive;

    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {

        Debug.Log("Interacted with TrapDisabler");
        trapActive = !trapActive;
        fireTrap.GetComponent<Animator>().SetBool("IsActive", trapActive);

    }
}
