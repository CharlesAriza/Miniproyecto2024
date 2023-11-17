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
    }

    void OnDestroy()
    {
        NetworkManager.Singleton.OnServerStarted -= ServerHasStarted;
    }


    private void ServerHasStarted()
    {
        SceneLoader.Instance.LoadScenes(new string[] { }, true, false);
        SceneLoader.Instance.LoadScenes(new string[] { SceneLoader.Instance.LobbyScene }, false, true);
        //SceneLoader.Instance.LoadScenes(new string[] { SceneLoader.Instance.GameScene }, true, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
