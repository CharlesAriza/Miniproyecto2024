using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DoorInteraction : NetworkBehaviour, IInteractable
{
    [SerializeField] private GameObject door;
    private bool doorOpen;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ServerRpc(RequireOwnership = false)]
    public void InteractServerRPC()
    {
        Debug.Log("Interacted with Door");
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }

    public void Interact()
    {
        InteractServerRPC();
    }
}
