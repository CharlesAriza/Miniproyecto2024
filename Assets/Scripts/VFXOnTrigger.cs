using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXOnTrigger : MonoBehaviour
{
    public GameObject vfxPrefab; // Asigna el VFX en el Inspector
    public float vfxDuration = 2.0f; // Duración del VFX

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            PlayVFX();
        }
    }

    private void PlayVFX()
    {
        if (vfxPrefab != null)
        {
            GameObject vfxInstance = Instantiate(vfxPrefab, transform.position, transform.rotation);
            Destroy(vfxInstance, vfxDuration); // Destruye el VFX después de la duración especificada
        }
    }
}
