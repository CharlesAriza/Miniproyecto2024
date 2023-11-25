using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : NetworkBehaviour
{

    public TMP_InputField IP;
    public TMP_InputField portText;
    private ushort port;
    public void PlayOfflineGame()
    {
        SceneLoader.Instance.LoadScenes(new string[] { SceneLoader.Instance.GameScene },true,false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayOnlineGameAsHost()
    {
        CambiarIP();
        NetworkManager.Singleton.StartHost();
    }

    public void PlayOnlineGameAsClient()
    {
        CambiarIP();
        NetworkManager.Singleton.StartClient();
    }

    //Cambia el unity transport, el IP y la Adress
    public void CambiarIP()
    {
        ushort.TryParse(portText.text, out port);
        NetworkManager.GetComponent<UnityTransport>().ConnectionData.Address = IP.text;
        NetworkManager.GetComponent<UnityTransport>().ConnectionData.Port = port;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}