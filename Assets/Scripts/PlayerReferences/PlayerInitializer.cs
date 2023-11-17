using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInitializer : NetworkBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public  void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);
        base.OnNetworkSpawn();


        if(IsServer)
        {
            Debug.Log("Instantiate Soldier");
            var intantiatedGO = Instantiate(player1Prefab);
            intantiatedGO.GetComponent<NetworkObject>().Spawn();
        }
        else
        {
            InstantiatePlayer2ServerRPC();
        }

    }

    [ServerRpc(RequireOwnership = false)]
    void InstantiatePlayer2ServerRPC()
    {
        Debug.Log("Instantiate Soldier");
        var intantiatedGO = Instantiate(player2Prefab);
        intantiatedGO.GetComponent<NetworkObject>().Spawn();
    }


    //// Start is called before the first frame update

    //   void Start()
    //    {
    //    ThirdPersonShooterController[] PlayerShooterController = FindObjectsByType<ThirdPersonShooterController>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
    //    ThirdPersonController[] PlayerController = FindObjectsByType<ThirdPersonController>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);

    //    for (int i = 0; i < PlayerShooterController.Length; i++)
    //    {
    //        PlayerShooterController[i].InitComponent();
    //    }

    //    for (int i = 0; i < PlayerController.Length; i++)
    //    {
    //        PlayerController[i].InitComponent();
    //    }

    //    }
}

