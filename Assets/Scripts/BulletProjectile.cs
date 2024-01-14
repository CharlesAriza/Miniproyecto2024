using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class BulletProjectile : NetworkBehaviour
{
    public GameObject vfxPrefab; // Asigna el VFX en el inspector
    private Rigidbody bulletRigidbody;
    public float bulletDamage = 10f; // Da�o de la bala


    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 40f;
        bulletRigidbody.velocity = transform.forward * speed;
    }


    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que colisionamos es un enemigo
        if (other.CompareTag("Enemy") || other.CompareTag("Trap"))
        {
            // Intenta obtener el componente EnemyHealth
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // Causa da�o al enemigo utilizando el valor de bulletDamage
                enemyHealth.TakeDamageServerRPC(bulletDamage);
            }        
            
        }

        //Instanciamos el VFX cuando toca con un collider.
        if (vfxPrefab != null)
        {
            Instantiate(vfxPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}