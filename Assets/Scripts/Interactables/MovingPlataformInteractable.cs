using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MovingPlataformInteractable : NetworkBehaviour, IInteractable
{
    [SerializeField] private GameObject plataformPanel;
    private bool plataformMove;

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
        Debug.Log("Interacted with Moving Plataform");
        plataformMove = !plataformMove;
        plataformPanel.GetComponent<Animator>().SetBool("IsActive", plataformMove);
    }



    public void Interact()
    {
        InteractServerRPC();
    }


}
