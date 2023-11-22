using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;

public class LobbyManager : NetworkBehaviour
{
    // Start is called before the first frame update

    public void GameLoad()
    {
        if (IsServer)
        {
            Debug.Log("Cargando juego...");
            SceneLoader.Instance.LoadScenes(new string[] { SceneLoader.Instance.GameScene }, true, true);
        }
    }

}