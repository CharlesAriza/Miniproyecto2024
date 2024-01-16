using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WinScreen : NetworkBehaviour
{
    public GameObject winScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Cambia "Player" por la etiqueta del objeto que activará el trigger
        {
            OnWinGameClientRPC();
        }
    }


    [ServerRpc(RequireOwnership = false)]
    public void OnWinGameServerRPC()
    {
        OnWinGameClientRPC();
    }

    [ClientRpc]

    public void OnWinGameClientRPC()
    {
        
        
            Cursor.visible = true; // Muestra el cursor
            Cursor.lockState = CursorLockMode.None;
            winScreen.SetActive(true);
        

    }


}
