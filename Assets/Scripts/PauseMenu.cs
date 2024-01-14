using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class PauseMenu : NetworkBehaviour
{
    public GameObject HostQuit;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject hideUI;


    void Start()
    {
        //Al incio llamo a la función "Resume" para que al inciar el "Level_1" y entrar en la escena no este el menu de pausa puesto.
        Resume();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        GameIsPaused = false;
        hideUI.SetActive(true);
        Cursor.visible = false; // Oculta el cursor
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro de la ventana
    }
    void Pause()
    {
        hideUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        //Si queremos pausar el juego añadiriramos la siguente linea de codigo, al ser multijugador no se pausara.
        //Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true; // Muestra el cursor
        Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
    }
    public void LoadMenu()
    {
        OnServerQuitServerRPC();
        NetworkManager.Singleton.Shutdown();
        Cleanup();
        //ATENCIÓN: Si no va a la escena "Program" se bugea! Esto es debido a que program se tiene que cargar primero que "MainMenu" para el correcto funcionamiento del juego.
        SceneManager.LoadScene("Program");
        Debug.Log("Loding menu...");
    }

    public void QuitGame()
    {
        OnServerQuitServerRPC();
        NetworkManager.Singleton.Shutdown();
        Cleanup();
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void ActivarCrosshair()
    {
        hideUI.SetActive(true);
    }

    //Limpia el Network Object que se duplica.
    void Cleanup()
    {
        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }
    }
    public void DesactivarCrosshair()
    {
        hideUI.SetActive(false);
    }

    [ServerRpc(RequireOwnership = false)]
    public void OnServerQuitServerRPC()
    {
        OnServerQuitClientRPC();
    }

    [ClientRpc]

    public void OnServerQuitClientRPC()
    {
        if(!IsServer)
        {
            Cursor.visible = true; // Muestra el cursor
            Cursor.lockState = CursorLockMode.None;
            HostQuit.SetActive(true);
        }
        
    }


}
