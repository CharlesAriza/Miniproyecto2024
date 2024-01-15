using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class BulletProjectile : NetworkBehaviour
{
    public GameObject vfxPrefab; // Asigna el VFX en el inspector
    private Rigidbody bulletRigidbody;
    public float bulletDamage = 10f; // Daño de la bala
    float speed = 40f;


    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    //private void Start()
    //{
        //bulletRigidbody.velocity = transform.forward * speed;
    //}

    public void InitBullet(Vector3 aimDirection)
    {
        bulletRigidbody.velocity = aimDirection.normalized * speed;
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
                // Causa daño al enemigo utilizando el valor de bulletDamage
                enemyHealth.TakeDamageServerRPC(bulletDamage);
            }        
            
        }

        //Instanciamos el VFX cuando toca con un collider.
        if (vfxPrefab != null)
        {
            bulletVFXClientRPC();
        }
        Destroy(gameObject);

    }


    //sincronizar VFX balas
    [ServerRpc(RequireOwnership = false)]
    public void bulletVFXServerRPC()

    {
       bulletVFXClientRPC();
    }

    [ClientRpc]
    void bulletVFXClientRPC()
    {
        Instantiate(vfxPrefab, transform.position, transform.rotation);

    }
}