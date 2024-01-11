using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDetection : NetworkBehaviour
{
    public float detectionDistance = 2f; // Distancia mínima para atacar
    private Transform player;
    private EnemyMovement enemyMovement;

    
    void Start()
    {
       
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionDistance)
            {
                enemyMovement.EnableMovement();
            }
            else
            {
                enemyMovement.DisableMovement();
            }
        }
    }

    // Manejar el trigger con el jugador
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Causar 10 puntos de daño al jugador
                playerHealth.TakeDamage(10f);
            }

            // Destruir el enemigo
            Destroy(gameObject);
        }
    }
}
