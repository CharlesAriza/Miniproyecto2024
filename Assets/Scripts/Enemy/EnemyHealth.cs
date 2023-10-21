using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    
    

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
        // Realiza cualquier acci�n que desees cuando el enemigo muera, como reproducir una animaci�n o efectos de part�culas.
        Destroy(gameObject); // Destruye el objeto enemigo.
    }
}
