using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PickupRPC : NetworkBehaviour
{
    [ClientRpc]
    public void DestroyPickupClientRPC()
    {
        Destroy(gameObject);
    }


    [ServerRpc(RequireOwnership = false)]
    public void DestroyPickupServerRPC()
    {
        DestroyPickupClientRPC();
    }
}
