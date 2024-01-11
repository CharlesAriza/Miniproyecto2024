using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DesactivateTrapInteractable : NetworkBehaviour, IInteractable
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

    [ServerRpc(RequireOwnership =false)]
    private void InteractServerRPC()
    {
        Debug.Log("Interacted with TrapDisabler");
        trapActive = !trapActive;
        fireTrap.GetComponent<Animator>().SetBool("IsActive", trapActive);
    }

    public void Interact()
    {
        InteractServerRPC();
    }
}
