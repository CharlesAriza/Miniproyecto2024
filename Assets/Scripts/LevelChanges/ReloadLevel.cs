using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ReloadLevel : NetworkBehaviour
{
    public string CurrentLevel;
    public string NextLevel;

    GameObject[] thirdPersonShooterControllers;

    [ServerRpc(RequireOwnership = false)]
    public void DespawnPlayerServerRPC()
    {

        thirdPersonShooterControllers = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < thirdPersonShooterControllers.Length; i++)
        {
            Debug.Log("DespawnCompletado");
            //thirdPersonShooterControllers[i].GetComponent<NetworkObject>().Despawn();
            Destroy(thirdPersonShooterControllers[i].gameObject);

        }
        LevelChangeServerRPC();
    }

    [ServerRpc(RequireOwnership = false)]
    public void LevelChangeServerRPC()
    {

        SceneLoaderMultiplayer.Instance.UnloadScene(CurrentLevel);
        SceneLoaderMultiplayer.Instance.LoadScene(NextLevel);
    }
}
