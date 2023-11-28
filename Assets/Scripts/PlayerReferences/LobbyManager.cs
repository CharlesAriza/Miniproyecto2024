using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine.UI;

public class LobbyManager : NetworkBehaviour
{
    public Button startGame;
    private void Start()
    {
        if(IsServer)
        {
            startGame.interactable = true;
        }
        else           
        {
            startGame.interactable = false;
        }
    }
    public void GameLoad()
    {
        if (IsServer)
        {
            Debug.Log("Cargando juego...");
            SceneLoader.Instance.LoadScenes(new string[] { SceneLoader.Instance.GameScene }, true, true);
        }
    }

}