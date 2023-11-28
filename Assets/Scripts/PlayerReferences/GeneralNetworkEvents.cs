using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GeneralNetworkEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Singleton.OnServerStarted += ServerHasStarted;
        NetworkManager.Singleton.OnClientStarted += LocalClientConnection;
    }

    private void LocalClientConnection()
    {
        SceneLoader.Instance.LoadScenes(new string[] { }, true, false);
    }

    void OnDestroy()
    {
        NetworkManager.Singleton.OnServerStarted -= ServerHasStarted;
    }


    private void ServerHasStarted()
    {
        
        SceneLoader.Instance.LoadScenes(new string[] { SceneLoader.Instance.LobbyScene, SceneLoader.Instance.OnlineBaseScene  }, false, true);
        //SceneLoader.Instance.LoadScenes(new string[] { SceneLoader.Instance.GameScene }, true, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
