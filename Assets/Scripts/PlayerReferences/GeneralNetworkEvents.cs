using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GeneralNetworkEvents : MonoBehaviour
{ 

    public void Start()
    {
        NetworkManager.Singleton.OnServerStarted += callbackOnServerStarted;
        NetworkManager.Singleton.OnClientStarted += CallbackOnClientStarted;
        NetworkManager.Singleton.OnClientStopped += callbackWhenclientsDisconects;
    }

    private void CallbackOnClientStarted()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(SceneLoader.Instance.MainMenuScene);
    }

    private void callbackWhenclientsDisconects(bool obj)
    {
        Debug.Log("Client disconnected");
        SceneLoaderMultiplayer.Instance.UnloadScene(SceneLoader.Instance.LobbyScene);
        SceneLoaderMultiplayer.Instance.UnloadScene(SceneLoader.Instance.GameScene);
    }

    private void callbackOnServerStarted()
    {
        Debug.Log("Server started");       
        SceneLoaderMultiplayer.Instance.LoadScene(SceneLoader.Instance.LobbyScene);
    }
   
    void OnDestroy()
    {
        NetworkManager.Singleton.OnServerStarted -= callbackOnServerStarted;
        NetworkManager.Singleton.OnClientStarted -= CallbackOnClientStarted;
        NetworkManager.Singleton.OnClientStopped -= callbackWhenclientsDisconects;
    }
}
