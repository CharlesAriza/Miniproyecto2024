using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderMultiplayer : NetworkBehaviour
{
    private static SceneLoaderMultiplayer instance;
    public static SceneLoaderMultiplayer Instance => instance;

    public List<string> scenesPendingLoad;
    public List<string> scenesPendingUnload;

    public bool loadingSceneInProgress = false;

    public float timeOutForLoadingSceneInProgress = 30f;
    public float timer_timeourLoadingSceneInProgress = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.SceneManager.OnSceneEvent += SceneManager_OnSceneEvent;
        }

        base.OnNetworkSpawn();
    }

    private void SceneManager_OnSceneEvent(SceneEvent sceneEvent)
    {
        var clientOrServer = sceneEvent.ClientId == NetworkManager.ServerClientId ? "server" : "client";
        switch (sceneEvent.SceneEventType)
        {
            case SceneEventType.LoadEventCompleted:
            case SceneEventType.UnloadEventCompleted:
                {
                    loadingSceneInProgress = false;
                    timer_timeourLoadingSceneInProgress = 0f;
                }
                break;
        }
        Debug.Log("Scene Loader Event:" + sceneEvent.SceneEventType);
    }

    private void Update()
    {
        if (timer_timeourLoadingSceneInProgress > timeOutForLoadingSceneInProgress)
        {
            loadingSceneInProgress = false;      //timeout to avoid events problems
        }

        if (loadingSceneInProgress)
        {
            timer_timeourLoadingSceneInProgress += Time.deltaTime; 
        }
        else
        {
            if (scenesPendingUnload != null && scenesPendingUnload.Count > 0)
            {
                loadingSceneInProgress = true;
                Debug.Log("Descargando escena: " + scenesPendingUnload[0]);
                NetworkManager.Singleton.SceneManager.UnloadScene(SceneManager.GetSceneByName(scenesPendingUnload[0]));
                scenesPendingUnload.RemoveAt(0);
            }
            else if (scenesPendingLoad != null && scenesPendingLoad.Count > 0)
            {
                loadingSceneInProgress = true;
                Debug.Log("Cargando escena: " + scenesPendingLoad[0]);
                NetworkManager.Singleton.SceneManager.LoadScene(scenesPendingLoad[0], LoadSceneMode.Additive);
                scenesPendingLoad.RemoveAt(0);
            }
           
        }
    }
    public override void OnDestroy()
    {
        base.OnDestroy();

        if (IsServer)
        {
            NetworkManager.SceneManager.OnSceneEvent -= SceneManager_OnSceneEvent;
        }
    }

    public void LoadScene(string sceneToLoad)
    {
        if (!scenesPendingLoad.Contains(sceneToLoad))
        {
            scenesPendingLoad.Add(sceneToLoad);
        }
        else
        {
            Debug.Log("scene already pending to be loaded");
        }
    }
    public void UnloadScene(string sceneToUnLoad)
    {
        if (!scenesPendingUnload.Contains(sceneToUnLoad))
        {
            scenesPendingUnload.Add(sceneToUnLoad);
        }
        else
        {
            Debug.Log("scene already pending to be loaded");
        }
    }
   
}

