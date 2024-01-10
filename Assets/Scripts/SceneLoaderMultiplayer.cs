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
    public List<string> scenesPendingUnLoad;

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
            if (scenesPendingUnLoad != null && scenesPendingUnLoad.Count > 0)
            {
                loadingSceneInProgress = true;
                NetworkManager.Singleton.SceneManager.LoadScene(scenesPendingUnLoad[0], LoadSceneMode.Additive);
                scenesPendingUnLoad.RemoveAt(0);

            }
            else if (scenesPendingUnLoad != null && scenesPendingUnLoad.Count > 0)
            {
                loadingSceneInProgress = true;
                NetworkManager.Singleton.SceneManager.UnloadScene(SceneManager.GetSceneByName(scenesPendingUnLoad[0]));
                scenesPendingUnLoad.RemoveAt(0);
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
        if (!scenesPendingUnLoad.Contains(sceneToLoad))
        {
            scenesPendingUnLoad.Add(sceneToLoad);
        }
        else
        {
            Debug.Log("scene already pending to be loaded");
        }
    }
    public void UnloadScene(string sceneToUnLoad)
    {
        if (!scenesPendingUnLoad.Contains(sceneToUnLoad))
        {
            scenesPendingUnLoad.Add(sceneToUnLoad);
        }
        else
        {
            Debug.Log("scene already pending to be loaded");
        }
    }
   
}

