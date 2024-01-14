using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : NetworkBehaviour
{
    public string CurrentLevel;
    public string NextLevel;

    ThirdPersonShooterController[] thirdPersonShooterControllers;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DespawnPlayerServerRPC();
            LevelChangeServerRPC();
        }
    }

    [ClientRpc]
    public void DespawnPlayerClientRPC()
    {
        thirdPersonShooterControllers = FindObjectsOfType<ThirdPersonShooterController>();
        for (int i = 0; i < thirdPersonShooterControllers.Length; i++)
        {
            thirdPersonShooterControllers[i].GetComponent<NetworkObject>().Despawn();
        }
    }


    [ServerRpc(RequireOwnership = false)]
    public void DespawnPlayerServerRPC()
    {
        DespawnPlayerClientRPC();
    }

    [ServerRpc(RequireOwnership = false)]
    public void LevelChangeServerRPC()
    {
        SceneLoaderMultiplayer.Instance.UnloadScene(CurrentLevel);
        SceneLoaderMultiplayer.Instance.LoadScene(NextLevel);
    }


}
