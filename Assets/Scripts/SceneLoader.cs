using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;
    public static SceneLoader Instance => instance;
    [SerializeField] private string staticScene = "StaticScene";
    [SerializeField] private string mainMenuScene = "MainMenuScene";
    [SerializeField] private string gameScene = "GameScene";
    [SerializeField] private string lobbyScene = "Lobby";
    [SerializeField] private string onlineBaseScene = "OnlineBaseScene";
    public string LobbyScene => lobbyScene;

    public string MainMenuScene => mainMenuScene;
    public string GameScene => gameScene;
    public string OnlineBaseScene => onlineBaseScene;

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

    private void Start()
    {
        LoadScenes(new string[] { mainMenuScene }, true, false);
    }

    public void LoadScenes(string[] scenesNames, bool removeOtherScenes, bool onlineLoad)
    {
        StartCoroutine(LoadScenesCorrutine(scenesNames, removeOtherScenes, onlineLoad));    
    }

    public IEnumerator LoadScenesCorrutine (string[] scenesNames, bool removeOtherScenes, bool onlineLoad)
    {
        //Load All wanted Scene.
        for (int i = 0; i < scenesNames.Length; i++)
        {
            SceneEventProgressStatus loadOperation = SceneEventProgressStatus.None;

            if (IsSceneLoaded(scenesNames[i]))
            {
                Debug.LogFormat("Scene <color=yellow>" + scenesNames[i] + "</color> is Already loaded, not loading anything");
            }
            else
            {
                if (onlineLoad)
                {
                    loadOperation = NetworkManager.Singleton.SceneManager.LoadScene(scenesNames[i], LoadSceneMode.Additive);
                    Debug.Log("Load scene online ->" + loadOperation.ToString());

                }
                else
                {
                    SceneManager.LoadSceneAsync(scenesNames[i], LoadSceneMode.Additive);
                }
            }
            while(loadOperation == SceneEventProgressStatus.SceneEventInProgress || loadOperation ==  SceneEventProgressStatus.Started)
            {
                yield return null;

            }
        }

        if (removeOtherScenes)
        {
            StartCoroutine(RemoveUnwantedScenes(scenesNames, onlineLoad));
        }
    }

    [ContextMenu("Unload Lobby")]
    public void UnloadLobby()
    {
        NetworkManager.Singleton.SceneManager.UnloadScene(SceneManager.GetSceneByName(LobbyScene));
    }

    private IEnumerator RemoveUnwantedScenes(string[] wantedScenes, bool onlineLoad)
    {
        yield return null; //Needs at least 1 frame to ensure that the scene is loaded in the editor.

        List<Scene> unwantedScenes = new List<Scene>();

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            bool removeScene = true;

            for (int j = 0; j < wantedScenes.Length; j++)
            {
                if (SceneManager.GetSceneAt(i).name == wantedScenes[j])
                {
                    removeScene = false;
                }
            }
            if (removeScene)
            {
                unwantedScenes.Add(SceneManager.GetSceneAt(i));
                removeScene = false;
            }
        }

        for (int i = 0; i < unwantedScenes.Count; i++)
        {
            if (SceneManager.GetSceneAt(i).name != staticScene)
            {
                if (onlineLoad)
                {
                    //  Debug.Log("Unload scene online " + unwantedScenes[i].name);
                    SceneEventProgressStatus loadoperation = NetworkManager.Singleton.SceneManager.UnloadScene(unwantedScenes[i]);
                    Debug.Log("Unload scene online -> " + loadoperation.ToString());
                }
                else
                {
                    SceneManager.UnloadSceneAsync(unwantedScenes[i]);
                }



                //The static scene will be ignored always, not possible to unload it. -> Debug.LogFormat("Removing Scene <color=orange>" + SceneManager.GetSceneAt(i).name + "</color>");

            }
        }
    }

    public bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (sceneName == SceneManager.GetSceneAt(i).name)
            {
                return true;
            }
        }
        return false;
    }
    //[ContextMenu("Descargar MainMenu")]
    //public void UnloadMainMenu()
    //{
    //    if (NetworkManager.Singleton != null)
    //    { NetworkManager.Singleton.SceneManager.UnloadScene(GetLoadedSceneWithName(MainMenuScene)); }
    //    else
    //    {
    //        scene
    //    }
    //}
    //public Scene GetLoadedSceneWithName(string sceneName)
    //{
    //    for (int i = 0; i < SceneManager.sceneCount; i++)
    //    {
    //        if (SceneManager.GetSceneAt(i).name == sceneName)
    //        {
    //            return SceneManager.GetSceneAt(i);
    //        }
    //    }
    //    Debug.Log("Scene not found, returning first active Scene");
    //    return SceneManager.GetSceneAt(0);
    //}
}

