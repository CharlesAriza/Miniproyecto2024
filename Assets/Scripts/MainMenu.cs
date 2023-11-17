using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlayOfflineGame()
    {
        SceneLoader.Instance.LoadScenes(new string[] { SceneLoader.Instance.GameScene },true,false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayOnlineGameAsHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void PlayOnlineGameAsClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}