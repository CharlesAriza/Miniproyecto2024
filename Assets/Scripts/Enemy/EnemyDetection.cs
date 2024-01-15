using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDetection : NetworkBehaviour
{
    public float detectionDistance = 2f; // Distancia mínima para atacar
    [SerializeField] private GameObject[] players;
    [SerializeField] GameObject TargetPlayer;
    private EnemyMovement enemyMovement;


    
    void Start()
    {
        //if (!IsOwner) { return; }
        
        //    player = GameObject.FindGameObjectWithTag("Player").transform;
        //    enemyMovement = GetComponent<EnemyMovement>();
        
    }

    private void Update()
    {
        if (!IsOwner) { return; }

        players = GameObject.FindGameObjectsWithTag("Player");
        

        if (players != null && players.Length > 0) 
       
        { 
            TargetPlayer = players[0];
            float BestDistanceToPlayer = Vector3.Distance(this.transform.position, TargetPlayer.transform.position);
            for (int i = 0; i < players.Length; i++)
            {
               if (i == 0) { continue; }
                if (Vector3.Distance(this.transform.position, players[i].transform.position) < BestDistanceToPlayer)
                {
                    BestDistanceToPlayer = Vector3.Distance(this.transform.position, players[i].transform.position);
                    TargetPlayer = players[i];
                }
            }
        }

        if (TargetPlayer != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, TargetPlayer.transform.position);

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
            DestroyEnemyServerRPC();
        }
    }


    //Sincronizar la muerte del enemigo por colision.
    [ClientRpc]
    private void DestroyEnemyClientRPC()
    {
        Destroy(gameObject);
    }


    [ServerRpc(RequireOwnership = false)]
    private void DestroyEnemyServerRPC()
    {
        DestroyEnemyClientRPC();
    }

}
