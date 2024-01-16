using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : NetworkBehaviour
{
    public float movementSpeed = 5f;
    private Vector3 player;
    private bool canMove = false;
    public NavMeshAgent agent;

    void Start()
    {
        
    }

    //public override void OnNetworkSpawn()
    //{
    //    player = GameObject.FindGameObjectWithTag("Player").transform;
    //}


    void Update()
    {
        ////Añadimos un offset para que vaya al pecho del jugador.
        //// new Vector3(player.position.x, player.position.y + 1f, player.position.z);
        //Vector3 target = player.position + Vector3.up;
        if (canMove && player != null)
        {
            // Mueve el objeto hacia el jugador
            //transform.position =  Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
            agent.destination = player;
        }
        else
        {
            agent.destination = transform.position;
        }
    }

    public void EnableMovement(Vector3 position)
    {
        player = position;
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }
}
