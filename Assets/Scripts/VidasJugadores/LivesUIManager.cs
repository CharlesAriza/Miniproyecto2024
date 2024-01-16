using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class LivesUIManager : NetworkBehaviour
{
    public TextMeshProUGUI textoVidasRestantes;
    public GameObject pantallaDerrota;
    private void Start()
    {
        // Asegúrate de asignar el componente TextMeshProUGUI en el Inspector de Unity
        if (textoVidasRestantes == null)
        {
            Debug.LogError("Asigna el componente TextMeshProUGUI en el Inspector.");
        }
        else
        {
            UpdateLivesUI(5);  // Inicializa el texto con el número máximo de vidas
        }
    }

    public void UpdateLivesUI(int vidasRestantes)
    {
        // Actualiza el texto o la imagen de vidas restantes en la UI
        if (textoVidasRestantes != null)
        {
            textoVidasRestantes.text = "Lives Left: " + vidasRestantes;
        }

        if (vidasRestantes == 0)
        {
            PantallaDerrotaServerRPC();
        }
    }


    [ServerRpc(RequireOwnership = false)]
    public void PantallaDerrotaServerRPC()
    {
        PantallaDerrotaClientRPC();
    }

    [ClientRpc]

    public void PantallaDerrotaClientRPC()
    {


        Cursor.visible = true; // Muestra el cursor
        Cursor.lockState = CursorLockMode.None;
        pantallaDerrota.SetActive(true);


    }

   
}
