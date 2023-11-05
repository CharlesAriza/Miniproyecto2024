using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public GameObject deathVFXPrefab; // Arrastra el prefab del VFX en el Inspector


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (deathVFXPrefab != null)
        {
            Instantiate(deathVFXPrefab, transform.position, transform.rotation);
        }
        // Realiza cualquier acci�n que desees cuando el enemigo muera, como reproducir una animaci�n o efectos de part�culas.
        gameObject.SetActive(false); // Destruye el objeto enemigo.
    }

    internal void ResetEnemy()
    {
        throw new NotImplementedException();
    }
}
