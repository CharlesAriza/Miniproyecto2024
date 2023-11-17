using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNetworkCleanStart : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            var thirdPersonController = GetComponent<ThirdPersonController>();
            var interactor = GetComponent<Interactor>();
            var thirdPersonShooter = GetComponent<ThirdPersonShooterController>();
            var characterController = GetComponent<CharacterController>();
            var playerInpur = GetComponent<PlayerInput>();
            var starterAssetsInput = GetComponent<StarterAssetsInputs>();

            if (thirdPersonController != null) Destroy(thirdPersonController);
            if (interactor != null) Destroy(interactor);
            if (playerInpur != null) Destroy(playerInpur);
            if (starterAssetsInput != null) Destroy(starterAssetsInput);
            if (thirdPersonShooter != null) Destroy(thirdPersonShooter);
            if (characterController != null) Destroy(characterController);
        }

        else
        {
            PlayerHelperInicializator.Singleton.playerCamera.Follow = transform.GetComponentInChildren<PlayerCameraRoot>().transform;
            PlayerHelperInicializator.Singleton.playerAimCamera.Follow = transform.GetComponentInChildren<PlayerCameraRoot>().transform;
        }
    }



}
