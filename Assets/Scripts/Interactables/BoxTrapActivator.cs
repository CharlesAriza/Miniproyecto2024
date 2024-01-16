using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BoxTrapActivator : NetworkBehaviour, IInteractable
{
    [SerializeField] private GameObject CaptureBox;
    private bool TrapUp;

    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [ServerRpc(RequireOwnership = false)]
    private void InteractServerRPC()
    {
        Debug.Log("Interacted with Capture Box");
        TrapUp = !TrapUp;
        CaptureBox.GetComponent<Animator>().SetBool("IsActive", TrapUp);
    }

    public void Interact()
    {
        InteractServerRPC();
    }
}
