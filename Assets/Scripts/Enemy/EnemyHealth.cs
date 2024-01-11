using System;
using Unity.Netcode;
using UnityEngine;

public class EnemyHealth : NetworkBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public GameObject deathVFXPrefab; // Arrastra el prefab del VFX en el Inspector


    void Start()
    {
        
        currentHealth = maxHealth;
    }


    [ServerRpc]

    public void TakeDamageServerRPC(float damage)
    {
        TakeDamageClientRPC(damage);
    }
    [ClientRpc]
    public void TakeDamageClientRPC(float damage)
    {
        
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            DieServerRPC();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void DieServerRPC() 
    
    {
        DieClientRPC();
    }
    [ClientRpc]
    void DieClientRPC()
    {
        
        if (deathVFXPrefab != null)
        {
            Instantiate(deathVFXPrefab, transform.position, transform.rotation);
        }
        // Realiza cualquier acción que desees cuando el enemigo muera, como reproducir una animación o efectos de partículas.
        gameObject.SetActive(false); // Destruye el objeto enemigo.
    }

    
}
