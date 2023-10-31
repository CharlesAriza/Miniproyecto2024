using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;
    public static SceneLoader Instance => instance;

    [SerializeField] private string staticScene = "StaticScene";
    [SerializeField] private string mainMenuScene = "MainMenuScene";
    [SerializeField] private string gameScene = "GameScene";


    public string MainMenuScene => mainMenuScene;
    public string GameScene => gameScene;

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
        LoadScenes(new string[] { mainMenuScene }, true);

    }

    public void LoadScenes(string[] scenesNames, bool removeOtherScenes)
    {
        //Load All wanted Scene.
        for (int i = 0; i < scenesNames.Length; i++)
        {
            LoadScene(scenesNames[i]);
        }

        if (removeOtherScenes)
        {
            StartCoroutine(RemoveUnwantedScenes(scenesNames));
        }
    }

    private void LoadScene(string sceneName)
    {
        if (IsSceneLoaded(sceneName))
        {
            Debug.LogFormat("Scene <color=yellow>" + sceneName + "</color> is Already loaded, not loading anything");
        }
        else
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }

    private IEnumerator RemoveUnwantedScenes(string[] wantedScenes)
    {
        yield return null; //Needs at least 1 frame to ensure that the scene is loaded in the editor.

        List<string> unwantedScenes = new List<string>();

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
                unwantedScenes.Add(SceneManager.GetSceneAt(i).name);
                removeScene = false;
            }
        }

        for (int i = 0; i < unwantedScenes.Count; i++)
        {
            if (SceneManager.GetSceneAt(i).name != staticScene)
            {
                //The static scene will be ignored always, not possible to unload it. -> Debug.LogFormat("Removing Scene <color=orange>" + SceneManager.GetSceneAt(i).name + "</color>");
                SceneManager.UnloadSceneAsync(unwantedScenes[i]);
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
}

