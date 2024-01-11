using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class VFXOnTrigger : NetworkBehaviour
{
    public GameObject vfxPrefab; // Asigna el VFX en el Inspector
    public float vfxDuration = 2.0f; // Duración del VFX
    public float damage = 40f;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {        
            if (other.CompareTag("Player") && !hasTriggered)
            {
                hasTriggered = true;
                PlayVFXServerRPC();
            }
    }
    [ServerRpc(RequireOwnership =false)]
    private void PlayVFXServerRPC()
    {
        PlayVFXClientRPC();
    }

    [ClientRpc]
    private void PlayVFXClientRPC()
    {
        hasTriggered = true;

        if (vfxPrefab != null)
        {
            GameObject vfxInstance = Instantiate(vfxPrefab, transform.position, transform.rotation);
            Destroy(vfxInstance, vfxDuration); // Destruye el VFX después de la duración especificada
        }
    }
}
